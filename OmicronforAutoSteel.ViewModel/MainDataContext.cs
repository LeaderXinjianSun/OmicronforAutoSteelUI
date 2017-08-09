using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BingLibrary.hjb;
using BingLibrary.hjb.Intercepts;
using System.ComponentModel.Composition;
using OmicronforAutoSteel.Model;
using ViewROI;
using HalconDotNet;
using System.Collections.ObjectModel;

namespace OmicronforAutoSteel.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        #region 属性
        public virtual string MsgText { set; get; } = "";
        public virtual bool EpsonStatusAuto { set; get; } = false;
        public virtual bool EpsonStatusWarning { set; get; } = false;
        public virtual bool EpsonStatusSError { set; get; } = false;
        public virtual bool EpsonStatusSafeGuard { set; get; } = false;
        public virtual bool EpsonStatusEStop { set; get; } = false;
        public virtual bool EpsonStatusError { set; get; } = false;
        public virtual bool EpsonStatusPaused { set; get; } = false;
        public virtual bool EpsonStatusRunning { set; get; } = false;
        public virtual bool EpsonStatusReady { set; get; } = false;
        public virtual bool EpsonStatusAuto1 { set; get; } = false;
        public virtual bool EpsonStatusWarning1 { set; get; } = false;
        public virtual bool EpsonStatusSError1 { set; get; } = false;
        public virtual bool EpsonStatusSafeGuard1 { set; get; } = false;
        public virtual bool EpsonStatusEStop1 { set; get; } = false;
        public virtual bool EpsonStatusError1 { set; get; } = false;
        public virtual bool EpsonStatusPaused1 { set; get; } = false;
        public virtual bool EpsonStatusRunning1 { set; get; } = false;
        public virtual bool EpsonStatusReady1 { set; get; } = false;
        public virtual string HomePageVisibility { set; get; } = "Visible";
        public virtual string ParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string CameraPageVisibility { set; get; } = "Collapsed";
        public virtual string EpsonIp { set; get; }
        public virtual int EpsonTestSendPort { set; get; }
        public virtual int EpsonTestReceivePort { set; get; }
        public virtual int EpsonMsgReceivePort { set; get; }
        public virtual int EpsonRemoteControlPort { set; get; }
        public virtual string EpsonIp1 { set; get; }
        public virtual int EpsonTestSendPort1 { set; get; }
        public virtual int EpsonTestReceivePort1{ set; get; }
        public virtual int EpsonMsgReceivePort1 { set; get; }
        public virtual int EpsonRemoteControlPort1 { set; get; }
        public virtual bool TestSendPortStatus { set; get; } = false;
        public virtual bool TestRevPortStatus { set; get; } = false;
        public virtual bool MsgRevPortStatus { set; get; } = false;
        public virtual bool CtrlPortStatus { set; get; } = false;
        public virtual bool TestSendPortStatus1 { set; get; } = false;
        public virtual bool TestRevPortStatus1 { set; get; } = false;
        public virtual bool MsgRevPortStatus1 { set; get; } = false;
        public virtual bool CtrlPortStatus1 { set; get; } = false;
        public virtual string DeltaAS300Ip { set; get; }
        public virtual bool DeltaAS300PortStatus { set; get; }
        public virtual int DeltaAS300Port { set; get; }
        public virtual string AOISoftwareIp { set; get; }
        public virtual bool AOISoftwarePortStatus { set; get; }
        public virtual int AOISoftwarePort { set; get; }
        public virtual HImage hImage { set; get; }
        public virtual ObservableCollection<HObject> hObjectList { set; get; }
        public virtual ObservableCollection<ROI> ROIList { set; get; } = new ObservableCollection<ROI>();
        public virtual int ActiveIndex { set; get; }
        public virtual bool Repaint { set; get; }
        public virtual bool PLCConnect { set; get; }
        public virtual bool Robot1Connect{ set; get; }
        public virtual bool Robot2Connect { set; get; }
        public virtual bool AOICameraConnect { set; get; }
        public virtual bool IsRobotReady { set; get; }
        #endregion
        #region 变量
        private string MessageStr = "";
        private Robot1 robot1;
        private Robot2 robot2;
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private Camera camera = new Camera();
        private DeltaAS300 deltaAS300;
        private AOICCD aOICCD;
        private dialog mydialog = new dialog();
        #endregion
        #region 构造函数
        public MainDataContext()
        {
            //MsgText = AddMessage("开始");
            robot1 = new Robot1();
            robot2 = new Robot2();
            robot1.ModelPrint += ModelPrintEventProcess;
            robot2.ModelPrint += ModelPrintEventProcess;
            robot1.RobotPrint += Robot1PrintEventProcess;
            robot2.RobotPrint += Robot2PrintEventProcess;
            robot1.EpsonStatusUpdate += Robot1StatusUpdateProcess;
            robot2.EpsonStatusUpdate += Robot2StatusUpdateProcess;
            camera.ImageChanged += CameraImageChangeedProcess;
            deltaAS300 = new DeltaAS300();
            aOICCD = new AOICCD();
            deltaAS300.tcpListenerServer.SeverPrint += ModelPrintEventProcess;
            aOICCD.ModelPrint += ModelPrintEventProcess;
            deltaAS300.tcpsevertest();
            ReadParameter();
            UpdateUI();
            CameraInit();
            PLCRun();
        }
        #endregion
        #region 测试Function
        public void FunctionTest()
        {
            //MsgText = AddMessage("打印测试");
            camera.Action();
        }
        #endregion
        #region 功能与函数
        public void CameraAction()
        {
            camera.Action();
        }
        private void CameraInit()
        {
            if (camera.OpenDevice())
            {
                MsgText = AddMessage("USB相机初始化成功");
            }
            else
            {
                MsgText = AddMessage("打开相机失败");
            }
        }
        private void CameraImageChangeedProcess()
        {
            hImage = camera.himage;
        }
        private void AOIActioncallback(string s)
        {
            try
            {
                for (int i = 2; i < 27; i++)
                {
                    deltaAS300.YY[i] = false;
                }
                string[] strs = s.Split(';');
                for (int i = 1; i < strs.Length; i++)
                {
                    deltaAS300.YY[int.Parse(strs[i]) + 1] = true;
                }
                deltaAS300.YY[1] = true;
            }
            catch (Exception ex)
            {
                Log.Default.Error("MainDataContext.AOIActioncallback", ex.Message);
            }
        }
        private async void PLCRun()
        {
            bool XX0 = false, XX1 = false;
            while (true)
            {
                await Task.Delay(100);
                if (XX0 != deltaAS300.XX[0])
                {
                    XX0 = deltaAS300.XX[0];
                    if (XX0)
                    {
                        deltaAS300.YY[0] = false;
                        //deltaAS300.YY[28] = await camera.Action();
                        await Task.Delay(1000);
                        deltaAS300.YY[0] = true;
                    }
                }
                if (XX1 != deltaAS300.XX[1])
                {
                    XX1 = deltaAS300.XX[1];
                    if (XX1)
                    {
                        deltaAS300.YY[1] = false;
                        //await Task.Delay(1000);
                        //AOIActioncallback("");
                        aOICCD.Action(AOIActioncallback);
                    }
                }
            }
        }
        /// <summary>
        /// 画面选择
        /// </summary>
        /// <param name="p"></param>
        public void ChoosePage(object p)
        {
            string _swp = p.ToString();
            switch (_swp)
            {
                case "0":
                    HomePageVisibility = "Visible";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Collapsed";
                    break;
                case "1":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Visible";
                    CameraPageVisibility = "Collapsed";
                    break;
                case "2":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Visible";
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 机械手操作
        /// </summary>
        /// <param name="p"></param>
        public async void EpsonOpetate(object p)
        {
            string s = p.ToString();
            switch (s)
            {
                case "1":
                    robot1.StartOperate();
                    robot2.StartOperate();
                    break;
                case "2":
                    robot1.PauseOperate();
                    robot2.PauseOperate();
                    break;
                case "3":
                    robot1.ContinueOperate();
                    robot2.ContinueOperate();
                    break;
                case "4":
                    mydialog.changeaccent("red");
                    var r = await mydialog.showconfirm("确定进行机械手重启操作吗？");
                    if (r)
                    {
                        robot1.ReStartOperate();
                        robot2.ReStartOperate();
                    }
                    mydialog.changeaccent("Cobalt");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 界面保存按钮
        /// </summary>
        public void SaveParameter()
        {
            WriteParameter();
        }
        private async void UpdateUI()
        {
            while (true)
            {
                await Task.Delay(100);
                TestSendPortStatus = robot1.TestSentNet.tcpConnected;
                TestRevPortStatus = robot1.TestReceiveNet.tcpConnected;
                MsgRevPortStatus = robot1.MsgReceiveNet.tcpConnected;
                CtrlPortStatus = robot1.CtrlNet.tcpConnected;
                TestSendPortStatus1 = robot2.TestSentNet.tcpConnected;
                TestRevPortStatus1 = robot2.TestReceiveNet.tcpConnected;
                MsgRevPortStatus1 = robot2.MsgReceiveNet.tcpConnected;
                CtrlPortStatus1 = robot2.CtrlNet.tcpConnected;
                DeltaAS300PortStatus = deltaAS300.tcpListenerServer.ServerStatus;
                AOISoftwarePortStatus = aOICCD.AOISentNet.tcpConnected;
                PLCConnect = DeltaAS300PortStatus;
                Robot1Connect = TestSendPortStatus && TestRevPortStatus && MsgRevPortStatus && CtrlPortStatus;
                Robot2Connect = TestSendPortStatus1 && TestRevPortStatus1 && MsgRevPortStatus1 && CtrlPortStatus1;
                AOICameraConnect = AOISoftwarePortStatus;
                IsRobotReady = Robot1Connect && Robot2Connect;
            }
 
        }
        private void ModelPrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
        }
        private void Robot1PrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
        }
        private void Robot2PrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
        }
        private void Robot1StatusUpdateProcess(string str)
        {
            EpsonStatusAuto = str[2] == '1';
            EpsonStatusWarning = str[3] == '1';
            EpsonStatusSError = str[4] == '1';
            EpsonStatusSafeGuard = str[5] == '1';
            EpsonStatusEStop = str[6] == '1';
            EpsonStatusError = str[7] == '1';
            EpsonStatusPaused = str[8] == '1';
            EpsonStatusRunning = str[9] == '1';
            EpsonStatusReady = str[10] == '1';
        }
        private void Robot2StatusUpdateProcess(string str)
        {
            EpsonStatusAuto1 = str[2] == '1';
            EpsonStatusWarning1 = str[3] == '1';
            EpsonStatusSError1 = str[4] == '1';
            EpsonStatusSafeGuard1 = str[5] == '1';
            EpsonStatusEStop1 = str[6] == '1';
            EpsonStatusError1 = str[7] == '1';
            EpsonStatusPaused1 = str[8] == '1';
            EpsonStatusRunning1 = str[9] == '1';
            EpsonStatusReady1 = str[10] == '1';
        }
        /// <summary>
        /// 写参数到INI
        /// </summary>
        private void WriteParameter()
        {
            try
            {
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonIp", EpsonIp);
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestSendPort", EpsonTestSendPort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestReceivePort", EpsonTestReceivePort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", EpsonMsgReceivePort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", EpsonRemoteControlPort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonIp1", EpsonIp1);
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestSendPort1", EpsonTestSendPort1.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestReceivePort1", EpsonTestReceivePort1.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonMsgReceivePort1", EpsonMsgReceivePort1.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonRemoteControlPort1", EpsonRemoteControlPort1.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Delta", "DeltaAS300Ip", DeltaAS300Ip);
                Inifile.INIWriteValue(iniParameterPath, "Delta", "DeltaAS300Port", DeltaAS300Port.ToString());
                Inifile.INIWriteValue(iniParameterPath, "AOI", "AOISoftwareIp", AOISoftwareIp);
                Inifile.INIWriteValue(iniParameterPath, "AOI", "AOISoftwarePort", AOISoftwarePort.ToString());
                MsgText = AddMessage("保存参数完成");
            }
            catch (Exception ex)
            {
                Log.Default.Error("WriteParameter", ex.Message);
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
                EpsonIp1 = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp1", "192.168.1.21");
                EpsonTestSendPort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort1", "2000"));
                EpsonTestReceivePort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort1", "2001"));
                EpsonMsgReceivePort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort1", "2002"));
                EpsonRemoteControlPort1 = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort1", "5000"));
                DeltaAS300Ip = Inifile.INIGetStringValue(iniParameterPath, "Delta", "DeltaAS300Ip", "192.168.1.100");
                DeltaAS300Port = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Delta", "DeltaAS300Port", "9000"));
                AOISoftwareIp = Inifile.INIGetStringValue(iniParameterPath, "AOI", "AOISoftwareIp", "192.168.1.100");
                AOISoftwarePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "AOI", "AOISoftwarePort", "8010"));
                MsgText = AddMessage("读取参数完成");
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadParameter", ex.Message);
            }
        }
        /// <summary>
        /// 打印窗口字符处理函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string AddMessage(string str)
        {
            string[] s = MessageStr.Split('\n');
            if (s.Length > 1000)
            {
                MessageStr = "";
            }
            MessageStr += "\n"+ System.DateTime.Now.ToString() + " " + str ;
            return MessageStr;
        }
        #endregion




    }
    class VMManager
    {
        [Export(MEF.Contracts.Data)]
        [ExportMetadata(MEF.Key, "md")]
        MainDataContext md = MainDataContext.New<MainDataContext>();
    }
}
