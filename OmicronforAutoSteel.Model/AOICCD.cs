using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;

namespace OmicronforAutoSteel.Model
{
    public class AOICCD
    {
        public virtual string AOISoftwareIp { set; get; }
        public virtual int AOISoftwarePort { set; get; }

        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        public TcpIpClient AOISentNet = new TcpIpClient();
        public readonly AsyncLock m_lock1 = new AsyncLock();
        public delegate void PrintEventHandler(string ModelMessageStr);
        public event PrintEventHandler ModelPrint;
        public AOICCD()
        {
            ReadParameter();
            checkAOISentNet();
        }
        /// <summary>
        /// 从INI读参数
        /// </summary>
        private void ReadParameter()
        {
            try
            {
                AOISoftwareIp = Inifile.INIGetStringValue(iniParameterPath, "AOI", "AOISoftwareIp", "192.168.1.100");
                AOISoftwarePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "AOI", "AOISoftwarePort", "8010"));
            }
            catch (Exception ex)
            {
                Log.Default.Error("AOICCD.ReadParameter", ex.Message);
            }
        }
        public async void checkAOISentNet()
        {
            while (true)
            {
                AOISentNet.IsOnline();
                await Task.Delay(400);
                if (!AOISentNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    AOISentNet.IsOnline();
                    if (!AOISentNet.tcpConnected)
                    {
                        bool r1 = await AOISentNet.Connect(AOISoftwareIp, AOISoftwarePort);
                        if (r1)
                        {
                            ModelPrint("AOISentNet连接");
                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public delegate void ActionProcessedDelegate(String SS);
        public async void Action(ActionProcessedDelegate callback)
        {
            bool success = false;
            while (!success)
            {
                if (AOISentNet.tcpConnected)
                {
                    using (var releaser = await m_lock1.LockAsync())
                    {
                        await AOISentNet.SendAsync("AOICAMERA");
                        string s = await AOISentNet.ReceiveAsync();
                        string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        try
                        {
                            s = ss[0];
                        }
                        catch 
                        {
                            s = "error";
                        }
                        if (s != "error")
                        {
                            ModelPrint("AOIRev: " + s);
                            try
                            {
                                string[] strs = s.Split(';');
                                if (strs.Length > 1 && strs[0] == "CMD")
                                {
                                    callback(s);
                                    success = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                ModelPrint("AOI收字符串出错");
                                Log.Default.Error("AOICCD.Action", ex.Message);
                            }
                        }
                        else
                        {
                            await Task.Delay(100);
                        }
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }

    }
}
