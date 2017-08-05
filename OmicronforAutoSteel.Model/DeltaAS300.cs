using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using System.Net.Sockets;
using System.Net; // for the ip address

namespace OmicronforAutoSteel.Model
{
    public class DeltaAS300
    {
        public bool[] XX { set; get; } = new bool[50];
        public bool[] YY { set; get; } = new bool[50];
        public virtual string DeltaAS300Ip { set; get; }
        public virtual int DeltaAS300Port { set; get; }
        public TcpListenerServer tcpListenerServer;
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        public DeltaAS300()
        {
            ReadParameter();
            tcpListenerServer = new TcpListenerServer(DeltaAS300Ip, DeltaAS300Port);
        }
        public async void tcpsevertest()
        {
            await ((Func<Task>)(() =>
            {
                return Task.Run(() =>
                {
                    tcpListenerServer.run(UpdateIO);
                });
            }))();
        }
        private string UpdateIO(string str)
        {
            if (str.Length == 100)
            {
                string[] ss = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length == 50)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (ss[i] == "1")
                        {
                            XX[i] = true;
                        }
                        else
                        {
                            XX[i] = false;
                        }
                    }
                }
            }
            

            string outputstr = "";
            for (int i = 0; i < 50; i++)
            {
                if (YY[i])
                {
                    outputstr += "1;";
                }
                else
                {
                    outputstr += "0;";
                }
            }
            return outputstr;
        }
        /// <summary>
        /// 从INI读参数
        /// </summary>
        private void ReadParameter()
        {
            try
            {
                DeltaAS300Ip = Inifile.INIGetStringValue(iniParameterPath, "Delta", "DeltaAS300Ip", "192.168.1.5");
                DeltaAS300Port = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Delta", "DeltaAS300Port", "9000"));            
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadParameter", ex.Message);
            }
        }
    }
    public class TcpListenerServer
    {
        TcpListener server = null;
        public bool ServerStatus = false;
        public TcpListenerServer(string ip, Int32 port)
        {
            try
            {
                Int32 MyPort = port;
                IPAddress MyLocalAddr = IPAddress.Parse(ip);
                server = new TcpListener(MyLocalAddr, MyPort);
                server.Start();
                //SeverPrint("服务器已开启，等待PLC连接");
            }
            catch (Exception e)
            {
                Log.Default.Error("DeltaAS300.TcpListenerServer", e.Message);
            }

        }
        public delegate string ProcessedDelegate(string msgstr);
        public delegate void MsgPrintDelegate(string str);
        public event MsgPrintDelegate SeverPrint;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback">收到的字符串，处理一下，再传出相应信息</param>
        public void run(ProcessedDelegate callback)
        {
            Byte[] bytes = new Byte[512];
            String data = null;
            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    SeverPrint("PLC已连接 "+client.Client.RemoteEndPoint.ToString());
                    NetworkStream stream = client.GetStream();
                    int i;
                    ServerStatus = true;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        string str = callback(data);
                        byte[] bytes1 = System.Text.Encoding.ASCII.GetBytes(str);
                        stream.Write(bytes1, 0, bytes1.Length);
                        
                    }
                    SeverPrint("PLC断开");
                    ServerStatus = false;
                }
                catch (Exception ex)
                {
                    Log.Default.Error("DeltaAS300.TcpListenerServer.run", ex.Message);
                    ServerStatus = false;
                }

            }

        }
    }
}
