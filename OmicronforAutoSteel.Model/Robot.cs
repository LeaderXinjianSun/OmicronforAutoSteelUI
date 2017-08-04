using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;

namespace OmicronforAutoSteel.Model
{
    public class Robot1
    {
        #region 属性
        public virtual string EpsonIp { set; get; }
        public virtual int EpsonTestSendPort { set; get; }
        public virtual int EpsonTestReceivePort { set; get; }
        public virtual int EpsonMsgReceivePort { set; get; }
        public virtual int EpsonRemoteControlPort { set; get; }
        #endregion
        #region 变量
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        public TcpIpClient TestSentNet = new TcpIpClient();
        public TcpIpClient TestReceiveNet = new TcpIpClient();
        public TcpIpClient MsgReceiveNet = new TcpIpClient();
        public TcpIpClient CtrlNet = new TcpIpClient();
        private bool isLogined = false;
        public readonly AsyncLock m_lock1 = new AsyncLock();
        #endregion
        #region 事件
        public delegate void PrintEventHandler(string ModelMessageStr);
        public event PrintEventHandler ModelPrint;
        public event PrintEventHandler RobotPrint;
        public event PrintEventHandler EpsonStatusUpdate;
        #endregion
        public Robot1()
        {
            ReadParameter();
            checkCtrlNet();
            checkTestSentNet();
            checkTestReceiveNet();
            checkMsgReceiveNet();
            GetStatus();
            TestRevAnalysis();
            MsgRevAnalysis();

        }
        public async void checkCtrlNet()
        {
            while (true)
            {
                CtrlNet.IsOnline();
                if (!CtrlNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    CtrlNet.IsOnline();
                    if (!CtrlNet.tcpConnected)
                    {
                        isLogined = false;
                        bool r1 = await CtrlNet.Connect(EpsonIp, EpsonRemoteControlPort);
                        if (r1)
                        {
                            ModelPrint("机械手1CtrlNet连接");
                        }
                    }
                }
                if (!isLogined && CtrlNet.tcpConnected)
                {
                    string s = "";
                    using (var releaser = await m_lock1.LockAsync())
                    {
                        await CtrlNet.SendAsync("$login,123");
                        s = await CtrlNet.ReceiveAsync();
                    }
                    if (s.Contains("#login,0"))
                        isLogined = true;
                    await Task.Delay(400);
                }
                else
                {
                    await Task.Delay(3000);
                }
            }
        }
        public async void checkTestSentNet()
        {
            while (true)
            {
                TestSentNet.IsOnline();
                await Task.Delay(400);
                if (!TestSentNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestSentNet.IsOnline();
                    if (!TestSentNet.tcpConnected)
                    {
                        bool r1 = await TestSentNet.Connect(EpsonIp, EpsonTestSendPort);
                        if (r1)
                        {
                            ModelPrint("机械手TestSentNet连接");
                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkTestReceiveNet()
        {
            while (true)
            {
                TestReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!TestReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestReceiveNet.IsOnline();
                    if (!TestReceiveNet.tcpConnected)
                    {
                        bool r1 = await TestReceiveNet.Connect(EpsonIp, EpsonTestReceivePort);
                        if (r1)
                        {
                            ModelPrint("机械手1TestReceiveNet连接");
                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkMsgReceiveNet()
        {
            while (true)
            {
                MsgReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!MsgReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    MsgReceiveNet.IsOnline();
                    if (!MsgReceiveNet.tcpConnected)
                    {
                        bool r1 = await MsgReceiveNet.Connect(EpsonIp, EpsonMsgReceivePort);
                        if (r1)
                        {
                            ModelPrint("机械手1MsgReceiveNet连接");

                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        private async void GetStatus()
        {
            string status = "";
            while (true)
            {
                if (isLogined == true)
                {
                    try
                    {
                        using (var releaser = await m_lock1.LockAsync())
                        {
                            await CtrlNet.SendAsync("$getstatus");
                            status = await CtrlNet.ReceiveAsync();
                        }
                        string[] statuss = status.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (statuss[0] == "#getstatus")
                        {
                            if (statuss[1].Length == 11)
                            {
                                EpsonStatusUpdate(statuss[1]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Default.Error("Robot1.GetStatus", ex.Message);
                    }
                }
                await Task.Delay(1000);
            }
        }
        private async void TestRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (TestReceiveNet.tcpConnected == true)
                {
                    string s = await TestReceiveNet.ReceiveAsync();

                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        TestReceiveNet.tcpConnected = false;
                        ModelPrint("机械手TestReceiveNet断开");
                    }
                    else
                    {
                        ModelPrint("TestRev: " + s);
                        try
                        {
                            string[] strs = s.Split(',');
                            switch (strs[0])
                            {
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelPrint("监听机械手1命令出错");
                            Log.Default.Error("Robot1.TestRevAnalysis", ex.Message);
                        }

                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        private async void MsgRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (MsgReceiveNet.tcpConnected == true)
                {
                    string s = await MsgReceiveNet.ReceiveAsync();
                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        MsgReceiveNet.tcpConnected = false;
                        ModelPrint("机械手1MsgReceiveNet断开");
                    }
                    else
                    {
                        RobotPrint("MsgRev: " + s);
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        /// <summary>
        /// 从INI读参数
        /// </summary>
        private void ReadParameter()
        {
            try
            {
                EpsonIp = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp", "192.168.1.20");
                EpsonTestSendPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort", "2000"));
                EpsonTestReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort", "2001"));
                EpsonMsgReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", "2002"));
                EpsonRemoteControlPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", "5000"));
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadParameter", ex.Message);
            }
        }
    }
    public class Robot2
    {
        public virtual string EpsonIp1 { set; get; }
        public virtual int EpsonTestSendPort1 { set; get; }
        public virtual int EpsonTestReceivePort1 { set; get; }
        public virtual int EpsonMsgReceivePort1 { set; get; }
        public virtual int EpsonRemoteControlPort1 { set; get; }
        #region 变量
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        public TcpIpClient TestSentNet = new TcpIpClient();
        public TcpIpClient TestReceiveNet = new TcpIpClient();
        public TcpIpClient MsgReceiveNet = new TcpIpClient();
        public TcpIpClient CtrlNet = new TcpIpClient();
        private bool isLogined = false;
        public readonly AsyncLock m_lock1 = new AsyncLock();
        #endregion
        #region 事件
        public delegate void PrintEventHandler(string ModelMessageStr);
        public event PrintEventHandler ModelPrint;
        public event PrintEventHandler RobotPrint;
        public event PrintEventHandler EpsonStatusUpdate;
        #endregion
        public Robot2()
        {
            ReadParameter();
            checkCtrlNet();
            checkTestSentNet();
            checkTestReceiveNet();
            checkMsgReceiveNet();
            GetStatus();
            TestRevAnalysis();
            MsgRevAnalysis();
        }
        public async void checkCtrlNet()
        {
            while (true)
            {
                CtrlNet.IsOnline();
                if (!CtrlNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    CtrlNet.IsOnline();
                    if (!CtrlNet.tcpConnected)
                    {
                        isLogined = false;
                        bool r1 = await CtrlNet.Connect(EpsonIp1, EpsonRemoteControlPort1);
                        if (r1)
                        {
                            ModelPrint("机械手2CtrlNet连接");
                        }
                    }
                }
                if (!isLogined && CtrlNet.tcpConnected)
                {
                    string s = "";
                    using (var releaser = await m_lock1.LockAsync())
                    {
                        await CtrlNet.SendAsync("$login,123");
                        s = await CtrlNet.ReceiveAsync();
                    }
                    if (s.Contains("#login,0"))
                        isLogined = true;
                    await Task.Delay(400);
                }
                else
                {
                    await Task.Delay(3000);
                }
            }
        }
        public async void checkTestSentNet()
        {
            while (true)
            {
                TestSentNet.IsOnline();
                await Task.Delay(400);
                if (!TestSentNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestSentNet.IsOnline();
                    if (!TestSentNet.tcpConnected)
                    {
                        bool r1 = await TestSentNet.Connect(EpsonIp1, EpsonTestSendPort1);
                        if (r1)
                        {
                            ModelPrint("机械手2TestSentNet连接");
                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkTestReceiveNet()
        {
            while (true)
            {
                TestReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!TestReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestReceiveNet.IsOnline();
                    if (!TestReceiveNet.tcpConnected)
                    {
                        bool r1 = await TestReceiveNet.Connect(EpsonIp1, EpsonTestReceivePort1);
                        if (r1)
                        {
                            ModelPrint("机械手2TestReceiveNet连接");
                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkMsgReceiveNet()
        {
            while (true)
            {
                MsgReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!MsgReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    MsgReceiveNet.IsOnline();
                    if (!MsgReceiveNet.tcpConnected)
                    {
                        bool r1 = await MsgReceiveNet.Connect(EpsonIp1, EpsonMsgReceivePort1);
                        if (r1)
                        {
                            ModelPrint("机械手2MsgReceiveNet连接");

                        }
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        private async void GetStatus()
        {
            string status = "";
            while (true)
            {
                if (isLogined == true)
                {
                    try
                    {
                        using (var releaser = await m_lock1.LockAsync())
                        {
                            await CtrlNet.SendAsync("$getstatus");
                            status = await CtrlNet.ReceiveAsync();
                        }
                        string[] statuss = status.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (statuss[0] == "#getstatus")
                        {
                            if (statuss[1].Length == 11)
                            {
                                EpsonStatusUpdate(statuss[1]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Default.Error("Robot2.GetStatus", ex.Message);
                    }
                }
                await Task.Delay(1000);
            }
        }
        private async void TestRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (TestReceiveNet.tcpConnected == true)
                {
                    string s = await TestReceiveNet.ReceiveAsync();

                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        TestReceiveNet.tcpConnected = false;
                        ModelPrint("机械手2TestReceiveNet断开");
                    }
                    else
                    {
                        ModelPrint("TestRev: " + s);
                        try
                        {
                            string[] strs = s.Split(',');
                            switch (strs[0])
                            {
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelPrint("监听机械手2命令出错");
                            Log.Default.Error("Robot1.TestRevAnalysis", ex.Message);
                        }

                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        private async void MsgRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (MsgReceiveNet.tcpConnected == true)
                {
                    string s = await MsgReceiveNet.ReceiveAsync();
                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        MsgReceiveNet.tcpConnected = false;
                        ModelPrint("机械手2MsgReceiveNet断开");
                    }
                    else
                    {
                        RobotPrint("MsgRev: " + s);
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        /// <summary>
        /// 从INI读参数
        /// </summary>
        private void ReadParameter()
        {
            try
            {
                EpsonIp1 = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp1", "192.168.1.21");
                EpsonTestSendPort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort1", "2000"));
                EpsonTestReceivePort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort1", "2001"));
                EpsonMsgReceivePort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort1", "2002"));
                EpsonRemoteControlPort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort1", "5000"));
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadParameter", ex.Message);
            }
        }
    }
}
