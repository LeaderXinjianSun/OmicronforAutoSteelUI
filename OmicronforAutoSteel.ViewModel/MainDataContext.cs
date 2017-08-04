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

        #endregion
        #region 变量
        private string MessageStr = "";
        private Robot1 robot1;
        private Robot2 robot2;
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
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
            ReadParameter();
            UpdateUI();
        }
        #endregion
        #region 测试Function
        public void FunctionTest()
        {
            //MsgText = AddMessage("打印测试");
            WriteParameter();
        }
        #endregion
        #region 功能与函数
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
                    break;
                case "1":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Visible";
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 机械手操作
        /// </summary>
        /// <param name="p"></param>
        public void EpsonOpetate(object p)
        {

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
            await Task.Delay(100);
            TestSendPortStatus = robot1.TestSentNet.tcpConnected;
            TestRevPortStatus = robot1.TestReceiveNet.tcpConnected;
            MsgRevPortStatus = robot1.MsgReceiveNet.tcpConnected;
            CtrlPortStatus = robot1.CtrlNet.tcpConnected;
            TestSendPortStatus1 = robot2.TestSentNet.tcpConnected;
            TestRevPortStatus1 = robot2.TestReceiveNet.tcpConnected;
            MsgRevPortStatus1 = robot2.MsgReceiveNet.tcpConnected;
            CtrlPortStatus1 = robot2.CtrlNet.tcpConnected;
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
