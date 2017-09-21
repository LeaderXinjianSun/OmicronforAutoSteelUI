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
        public virtual string ParameterPage1Visibility { set; get; } = "Collapsed";
        public virtual string ParameterPage2Visibility { set; get; } = "Collapsed";
        public virtual string ParameterPage3Visibility { set; get; } = "Collapsed";
        public virtual string AlarmGridVisibility { set; get; } = "Collapsed";
        public virtual string HelpPageVisibility { set; get; } = "Collapsed";
        public virtual string AlarmString1 { set; get; } = "Collapsed";
        public virtual string AlarmString2 { set; get; } = "Collapsed";
        public virtual string AlarmString3 { set; get; } = "Collapsed";
        public virtual string AlarmString4 { set; get; } = "Collapsed";
        public virtual string AlarmString5 { set; get; } = "Collapsed";
        public virtual string AlarmString6 { set; get; } = "Collapsed";
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
        public virtual bool USBCameraConnect { set; get; }
        public virtual bool IsRobotReady { set; get; }
        public virtual string CameraResult { set; get; }
        public virtual double R1XOffset1 { set; get; }
        public virtual double R1YOffset1 { set; get; }
        public virtual double R1UOffset1 { set; get; }
        public virtual double R2XOffset1 { set; get; }
        public virtual double R2YOffset1 { set; get; }
        public virtual double R2UOffset1 { set; get; }
        public virtual double R2XOffset2 { set; get; }
        public virtual double R2YOffset2 { set; get; }
        public virtual double R2UOffset2 { set; get; }
        public virtual double R2XOffset3 { set; get; }
        public virtual double R2YOffset3 { set; get; }
        public virtual double R2UOffset3 { set; get; }
        public virtual double R2XOffset4 { set; get; }
        public virtual double R2YOffset4 { set; get; }
        public virtual double R2UOffset4 { set; get; }
        public virtual double R2XOffset5 { set; get; }
        public virtual double R2YOffset5 { set; get; }
        public virtual double R2UOffset5 { set; get; }

        public virtual double R2XOffset1_2 { set; get; }
        public virtual double R2YOffset1_2 { set; get; }
        public virtual double R2UOffset1_2 { set; get; }
        public virtual double R2XOffset2_2 { set; get; }
        public virtual double R2YOffset2_2 { set; get; }
        public virtual double R2UOffset2_2 { set; get; }
        public virtual double R2XOffset3_2 { set; get; }
        public virtual double R2YOffset3_2 { set; get; }
        public virtual double R2UOffset3_2 { set; get; }
        public virtual double R2XOffset4_2 { set; get; }
        public virtual double R2YOffset4_2 { set; get; }
        public virtual double R2UOffset4_2 { set; get; }
        public virtual double R2XOffset5_2 { set; get; }
        public virtual double R2YOffset5_2 { set; get; }
        public virtual double R2UOffset5_2 { set; get; }

        public virtual double R2XOffset1_3 { set; get; }
        public virtual double R2YOffset1_3 { set; get; }
        public virtual double R2UOffset1_3 { set; get; }
        public virtual double R2XOffset2_3 { set; get; }
        public virtual double R2YOffset2_3 { set; get; }
        public virtual double R2UOffset2_3 { set; get; }
        public virtual double R2XOffset3_3 { set; get; }
        public virtual double R2YOffset3_3 { set; get; }
        public virtual double R2UOffset3_3 { set; get; }
        public virtual double R2XOffset4_3 { set; get; }
        public virtual double R2YOffset4_3 { set; get; }
        public virtual double R2UOffset4_3 { set; get; }
        public virtual double R2XOffset5_3 { set; get; }
        public virtual double R2YOffset5_3 { set; get; }
        public virtual double R2UOffset5_3 { set; get; }

        public virtual double R2XOffset1_4 { set; get; }
        public virtual double R2YOffset1_4 { set; get; }
        public virtual double R2UOffset1_4 { set; get; }
        public virtual double R2XOffset2_4 { set; get; }
        public virtual double R2YOffset2_4 { set; get; }
        public virtual double R2UOffset2_4 { set; get; }
        public virtual double R2XOffset3_4 { set; get; }
        public virtual double R2YOffset3_4 { set; get; }
        public virtual double R2UOffset3_4 { set; get; }
        public virtual double R2XOffset4_4 { set; get; }
        public virtual double R2YOffset4_4 { set; get; }
        public virtual double R2UOffset4_4 { set; get; }
        public virtual double R2XOffset5_4 { set; get; }
        public virtual double R2YOffset5_4 { set; get; }
        public virtual double R2UOffset5_4 { set; get; }

        public virtual double R2XOffset1_5 { set; get; }
        public virtual double R2YOffset1_5 { set; get; }
        public virtual double R2UOffset1_5 { set; get; }
        public virtual double R2XOffset2_5 { set; get; }
        public virtual double R2YOffset2_5 { set; get; }
        public virtual double R2UOffset2_5 { set; get; }
        public virtual double R2XOffset3_5 { set; get; }
        public virtual double R2YOffset3_5 { set; get; }
        public virtual double R2UOffset3_5 { set; get; }
        public virtual double R2XOffset4_5 { set; get; }
        public virtual double R2YOffset4_5 { set; get; }
        public virtual double R2UOffset4_5 { set; get; }
        public virtual double R2XOffset5_5 { set; get; }
        public virtual double R2YOffset5_5 { set; get; }
        public virtual double R2UOffset5_5 { set; get; }

        public virtual string AlarmTextString { set; get; }
        public virtual bool IsLogin { set; get; } = false;
        #endregion
        #region 变量
        private string MessageStr = "";
        private Robot1 robot1;
        private Robot2 robot2;
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private string iniCameraDataPath = System.Environment.CurrentDirectory + "\\CameraData.ini";
        private Camera camera = new Camera();
        //private Camera camera;
        private DeltaAS300 deltaAS300;
        private AOICCD aOICCD;
        private dialog mydialog = new dialog();
        ObservableCollection<HObject> _hObjectList = new ObservableCollection<HObject>();
        public readonly AsyncLock m_lock1 = new AsyncLock();
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
            camera.FindChanged += FindChangedProcess;
            deltaAS300 = new DeltaAS300();
            aOICCD = new AOICCD();
            deltaAS300.tcpListenerServer.SeverPrint += ModelPrintEventProcess;
            aOICCD.ModelPrint += ModelPrintEventProcess;
            deltaAS300.tcpsevertest();
            ReadParameter();
            UpdateUI();
            CameraInit();

            PLCRun();
            Runloop();

        }
        #endregion
        #region 测试Function
        public void FunctionTest()
        {
            //MsgText = AddMessage("打印测试");
            //camera.Action();
        }
        #endregion
        #region 功能与函数
        public void ShowAlarmText(string s)
        {
            AlarmTextString = s;
            AlarmGridVisibility = "Visible";
        }
        public void CloseAlarmText()
        {
            AlarmGridVisibility = "Collapsed";
        }
        public async void UpdateCameraData(object p)
        {
            string _swp = p.ToString();
            switch (_swp)
            {
                case "1":
                    Inifile.INIWriteValue(iniCameraDataPath, "R1", "R1XOffset1", R1XOffset1.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R1", "R1YOffset1", R1YOffset1.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R1", "R1UOffset1", R1UOffset1.ToString());
                    using (var releaser = await robot1.m_lock2.LockAsync())
                    {
                        if (robot1.TestSentNet.tcpConnected)
                        {
                            await robot1.TestSentNet.SendAsync("R1Offset;" + R1XOffset1.ToString() + ";" + R1YOffset1.ToString() + ";" + R1UOffset1.ToString());
                            MsgText = AddMessage("更新机械手1偏移量");
                        }
                    }
                    break;
                case "2":
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset1", R2XOffset1.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset1", R2YOffset1.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset1", R2UOffset1.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset2", R2XOffset2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset2", R2YOffset2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset2", R2UOffset2.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset3", R2XOffset3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset3", R2YOffset3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset3", R2UOffset3.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset4", R2XOffset4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset4", R2YOffset4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset4", R2UOffset4.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset5", R2XOffset5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset5", R2YOffset5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset5", R2UOffset5.ToString());

                    using (var releaser = await robot2.m_lock2.LockAsync())
                    {
                        if (robot2.TestSentNet.tcpConnected)
                        {
                            await robot2.TestSentNet.SendAsync("R2Offset1;" + R2XOffset1.ToString() + ";" + R2XOffset2.ToString() + ";" + R2XOffset3.ToString() + ";" + R2XOffset4.ToString() + ";" + R2XOffset5.ToString() + ";"
                                + R2YOffset1.ToString() + ";" + R2YOffset2.ToString() + ";" + R2YOffset3.ToString() + ";" + R2YOffset4.ToString() + ";" + R2YOffset5.ToString() + ";"
                                + R2UOffset1.ToString() + ";" + R2UOffset2.ToString() + ";" + R2UOffset3.ToString() + ";" + R2UOffset4.ToString() + ";" + R2UOffset5.ToString());
                            MsgText = AddMessage("更新机械手2_1偏移量");
                        }
                    }
                    break;
                case "3":
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset1_2", R2XOffset1_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset1_2", R2YOffset1_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset1_2", R2UOffset1_2.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset2_2", R2XOffset2_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset2_2", R2YOffset2_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset2_2", R2UOffset2_2.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset3_2", R2XOffset3_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset3_2", R2YOffset3_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset3_2", R2UOffset3_2.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset4_2", R2XOffset4_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset4_2", R2YOffset4_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset4_2", R2UOffset4_2.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset5_2", R2XOffset5_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset5_2", R2YOffset5_2.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset5_2", R2UOffset5_2.ToString());

                    using (var releaser = await robot2.m_lock2.LockAsync())
                    {
                        if (robot2.TestSentNet.tcpConnected)
                        {
                            await robot2.TestSentNet.SendAsync("R2Offset2;" + R2XOffset1_2.ToString() + ";" + R2XOffset2_2.ToString() + ";" + R2XOffset3_2.ToString() + ";" + R2XOffset4_2.ToString() + ";" + R2XOffset5_2.ToString() + ";"
                                + R2YOffset1_2.ToString() + ";" + R2YOffset2_2.ToString() + ";" + R2YOffset3_2.ToString() + ";" + R2YOffset4_2.ToString() + ";" + R2YOffset5_2.ToString() + ";"
                                + R2UOffset1_2.ToString() + ";" + R2UOffset2_2.ToString() + ";" + R2UOffset3_2.ToString() + ";" + R2UOffset4_2.ToString() + ";" + R2UOffset5_2.ToString());
                            MsgText = AddMessage("更新机械手2_2偏移量");
                        }
                    }
                    break;
                case "4":
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset1_3", R2XOffset1_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset1_3", R2YOffset1_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset1_3", R2UOffset1_3.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset2_3", R2XOffset2_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset2_3", R2YOffset2_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset2_3", R2UOffset2_3.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset3_3", R2XOffset3_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset3_3", R2YOffset3_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset3_3", R2UOffset3_3.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset4_3", R2XOffset4_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset4_3", R2YOffset4_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset4_3", R2UOffset4_3.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset5_3", R2XOffset5_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset5_3", R2YOffset5_3.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset5_3", R2UOffset5_3.ToString());

                    using (var releaser = await robot2.m_lock2.LockAsync())
                    {
                        if (robot2.TestSentNet.tcpConnected)
                        {
                            await robot2.TestSentNet.SendAsync("R2Offset3;" + R2XOffset1_3.ToString() + ";" + R2XOffset2_3.ToString() + ";" + R2XOffset3_3.ToString() + ";" + R2XOffset4_3.ToString() + ";" + R2XOffset5_3.ToString() + ";"
                                + R2YOffset1_3.ToString() + ";" + R2YOffset2_3.ToString() + ";" + R2YOffset3_3.ToString() + ";" + R2YOffset4_3.ToString() + ";" + R2YOffset5_3.ToString() + ";"
                                + R2UOffset1_3.ToString() + ";" + R2UOffset2_3.ToString() + ";" + R2UOffset3_3.ToString() + ";" + R2UOffset4_3.ToString() + ";" + R2UOffset5_3.ToString());
                            MsgText = AddMessage("更新机械手2_3偏移量");
                        }
                    }
                    break;
                case "5":
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset1_4", R2XOffset1_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset1_4", R2YOffset1_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset1_4", R2UOffset1_4.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset2_4", R2XOffset2_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset2_4", R2YOffset2_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset2_4", R2UOffset2_4.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset3_4", R2XOffset3_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset3_4", R2YOffset3_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset3_4", R2UOffset3_4.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset4_4", R2XOffset4_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset4_4", R2YOffset4_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset4_4", R2UOffset4_4.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset5_4", R2XOffset5_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset5_4", R2YOffset5_4.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset5_4", R2UOffset5_4.ToString());

                    using (var releaser = await robot2.m_lock2.LockAsync())
                    {
                        if (robot2.TestSentNet.tcpConnected)
                        {
                            await robot2.TestSentNet.SendAsync("R2Offset4;" + R2XOffset1_4.ToString() + ";" + R2XOffset2_4.ToString() + ";" + R2XOffset3_4.ToString() + ";" + R2XOffset4_4.ToString() + ";" + R2XOffset5_4.ToString() + ";"
                                + R2YOffset1_4.ToString() + ";" + R2YOffset2_4.ToString() + ";" + R2YOffset3_4.ToString() + ";" + R2YOffset4_4.ToString() + ";" + R2YOffset5_4.ToString() + ";"
                                + R2UOffset1_4.ToString() + ";" + R2UOffset2_4.ToString() + ";" + R2UOffset3_4.ToString() + ";" + R2UOffset4_4.ToString() + ";" + R2UOffset5_4.ToString());
                            MsgText = AddMessage("更新机械手2_4偏移量");
                        }
                    }
                    break;
                case "6":
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset1_5", R2XOffset1_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset1_5", R2YOffset1_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset1_5", R2UOffset1_5.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset2_5", R2XOffset2_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset2_5", R2YOffset2_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset2_5", R2UOffset2_5.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset3_5", R2XOffset3_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset3_5", R2YOffset3_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset3_5", R2UOffset3_5.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset4_5", R2XOffset4_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset4_5", R2YOffset4_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset4_5", R2UOffset4_5.ToString());

                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2XOffset5_5", R2XOffset5_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2YOffset5_5", R2YOffset5_5.ToString());
                    Inifile.INIWriteValue(iniCameraDataPath, "R2", "R2UOffset5_5", R2UOffset5_5.ToString());

                    using (var releaser = await robot2.m_lock2.LockAsync())
                    {
                        if (robot2.TestSentNet.tcpConnected)
                        {
                            await robot2.TestSentNet.SendAsync("R2Offset5;" + R2XOffset1_5.ToString() + ";" + R2XOffset2_5.ToString() + ";" + R2XOffset3_5.ToString() + ";" + R2XOffset4_5.ToString() + ";" + R2XOffset5_5.ToString() + ";"
                                + R2YOffset1_5.ToString() + ";" + R2YOffset2_5.ToString() + ";" + R2YOffset3_5.ToString() + ";" + R2YOffset4_5.ToString() + ";" + R2YOffset5_5.ToString() + ";"
                                + R2UOffset1_5.ToString() + ";" + R2UOffset2_5.ToString() + ";" + R2UOffset3_5.ToString() + ";" + R2UOffset4_5.ToString() + ";" + R2UOffset5_5.ToString());
                            MsgText = AddMessage("更新机械手2_5偏移量");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public async void CameraAction()
        {
            _hObjectList.Clear();
            hObjectList = null;
            int tt = 0;
            using (var releaser = await m_lock1.LockAsync())
            {
                tt = await camera.RunAction();
                if (tt == -1)
                {
                    _hObjectList.Clear();
                    tt = await camera.RunAction();
                }
            }
            if (_hObjectList.Count > 0)
            {
                hObjectList = _hObjectList;
            }
        }
        private void CameraInit()
        {
            if (camera.OpenDevice())
            {
                camera.CreatModel();
                if (_hObjectList.Count > 0)
                {
                    hObjectList = _hObjectList;
                }
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
        private void FindChangedProcess(HObject ht)
        {
            HObject tt = new HObject(ht); ;
            _hObjectList.Add(tt);
        }
        private void AOIActioncallback(string s)
        {
            try
            {
                for (int i = 2; i < 27; i++)
                {
                    deltaAS300.YY[i] = false;
                }
                if (s == "OK")
                {

                }
                else
                {
                    string[] strs = s.Split(';');
                    for (int i = 1; i < strs.Length; i++)
                    {
                        deltaAS300.YY[int.Parse(strs[i]) + 1] = true;
                    }
                }
  
                deltaAS300.YY[1] = true;
            }
            catch (Exception ex)
            {
                Log.Default.Error("MainDataContext.AOIActioncallback", ex.Message);
            }
        }
        private async void USBCameraPLCAction()
        {
            _hObjectList.Clear();
            hObjectList = null;
            int tt;
            await Task.Run(async () =>
            {
                using (var releaser = await m_lock1.LockAsync())
                {
                    tt = await camera.RunAction();
                    while (tt != 1 && !deltaAS300.XX[40])
                    {
                        _hObjectList.Clear();
                        tt = await camera.RunAction();
                        await Task.Delay(10);
                    }
                    if (_hObjectList.Count > 0)
                    {
                        hObjectList = _hObjectList;
                        await Task.Delay(200);
                        deltaAS300.YY[0] = true;
                    }

                }
            });
        }
        private async void PLCRun()
        {
            bool XX0 = false, XX1 = false;
            bool XX10 = false, XX11 = false, XX12 = false, XX13 = false, XX14 = false, XX15 = false, XX16 = false,XX17 = false;
            while (true)
            {
                await Task.Delay(100);
                if (XX0 != deltaAS300.XX[0])
                {
                    XX0 = deltaAS300.XX[0];
                    if (XX0)
                    {
                        deltaAS300.YY[0] = false;

                        USBCameraPLCAction();

                    }
                    else
                    {
                        deltaAS300.YY[0] = false;
                    }
                }
                if (XX1 != deltaAS300.XX[1])
                {
                    XX1 = deltaAS300.XX[1];
                    if (XX1)
                    {
                        deltaAS300.YY[1] = false;
                        await Task.Delay(1000);
                        //AOIActioncallback("");
                        aOICCD.Action(AOIActioncallback);
                    }
                    else
                    {
                        deltaAS300.YY[1] = false;
                    }
                }
                if (XX10 != deltaAS300.XX[10])
                {
                    XX10 = deltaAS300.XX[10];
                    if (XX10)
                    {
                        AlarmString1 = "Visible";
                    }
                    else
                    {
                        AlarmString1 = "Collapsed";
                    }
                }
                if (XX11 != deltaAS300.XX[11])
                {
                    XX11 = deltaAS300.XX[11];
                    if (XX11)
                    {
                        AlarmString2 = "Visible";
                    }
                    else
                    {
                        AlarmString2 = "Collapsed";
                    }
                }
                if (XX12 != deltaAS300.XX[12])
                {
                    XX12 = deltaAS300.XX[12];
                    if (XX12)
                    {
                        AlarmString3 = "Visible";
                    }
                    else
                    {
                        AlarmString3 = "Collapsed";
                    }
                }
                if (XX13 != deltaAS300.XX[13])
                {
                    XX13 = deltaAS300.XX[13];
                    if (XX13)
                    {
                        AlarmString4 = "Visible";
                    }
                    else
                    {
                        AlarmString4 = "Collapsed";
                    }
                }
                if (XX14 != deltaAS300.XX[14])
                {
                    XX14 = deltaAS300.XX[14];
                    if (XX14)
                    {
                        AlarmString5 = "Visible";
                    }
                    else
                    {
                        AlarmString5 = "Collapsed";
                    }
                }
                if (XX15 != deltaAS300.XX[15])
                {
                    XX15 = deltaAS300.XX[15];
                    if (XX15)
                    {
                        AlarmString3 = "Visible";
                    }
                    
                }
                if (XX16 != deltaAS300.XX[16])
                {
                    XX16 = deltaAS300.XX[16];
                    if (XX16)
                    {
                        AlarmString4 = "Visible";
                    }
                    
                }
                if (!XX12 && !XX15)
                {
                    AlarmString3 = "Collapsed";
                }
                if (!XX13 && !XX16)
                {
                    AlarmString4 = "Collapsed";
                }
                if (XX17 != deltaAS300.XX[17])
                {
                    XX17 = deltaAS300.XX[17];
                    if (XX17)
                    {
                        AlarmString6 = "Visible";
                    }
                    else
                    {
                        AlarmString6 = "Collapsed";
                    }
                }
            }
        }
        private async void Runloop()
        {
            while (true)
            {
                await Task.Delay(1000);
                using (var releaser = await m_lock1.LockAsync())
                {
                    _hObjectList.Clear();

                    hObjectList = null;

                    
                    int r = await camera.RunAction();
                    CameraResult = r == 1 ? "OK" : "NG";
                    if (_hObjectList.Count > 0)
                    {
                        hObjectList = _hObjectList;
                    }
                }
            }
        }
        #region PLC命令按钮
        public void PCBLoadMouseDown()
        {
            deltaAS300.YY[41] = true;
        }
        public void PCBLoadMouseUp()
        {
            deltaAS300.YY[41] = false;
        }
        public void MoLoadMouseDown()
        {
            deltaAS300.YY[42] = true;
        }
        public void MoLoadMouseUp()
        {
            deltaAS300.YY[42] = false;
        }
        public void GPLoadMouseDown()
        {
            deltaAS300.YY[43] = true;
        }
        public void GPLoadMouseUp()
        {
            deltaAS300.YY[43] = false;
        }
        public void GPUnLoadMouseDown()
        {
            deltaAS300.YY[44] = true;
        }
        public void GPUnLoadMouseUp()
        {
            deltaAS300.YY[44] = false;
        }
        public void PCBUnLoadMouseDown()
        {
            deltaAS300.YY[45] = true;
        }
        public void PCBUnLoadMouseUp()
        {
            deltaAS300.YY[45] = false;
        }
        public void GPDownMouseDown()
        {
            deltaAS300.YY[46] = true;
        }
        public void GPDownMouseUp()
        {
            deltaAS300.YY[46] = false;
        }
        #endregion
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
                    ParameterPage1Visibility = "Collapsed";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Collapsed";
                    IsLogin = false;
                    break;
                case "1":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Visible";
                    CameraPageVisibility = "Collapsed";
                    ParameterPage1Visibility = "Collapsed";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Collapsed";
                    break;
                case "2":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Visible";
                    ParameterPage1Visibility = "Collapsed";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Collapsed";
                    IsLogin = false;
                    break;
                case "3":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Collapsed";
                    ParameterPage1Visibility = "Visible";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Collapsed";
                    break;
                case "4":
                    LoginAciton();
                    break;
                case "5":
                    HelpPageVisibility = "Visible";
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Collapsed";
                    ParameterPage1Visibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Collapsed";
                    IsLogin = false;
                    break;
                case "6":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Collapsed";
                    ParameterPage1Visibility = "Collapsed";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Visible";
                    ParameterPage3Visibility = "Collapsed";
                    break;
                case "7":
                    HomePageVisibility = "Collapsed";
                    ParameterPageVisibility = "Collapsed";
                    CameraPageVisibility = "Collapsed";
                    ParameterPage1Visibility = "Collapsed";
                    HelpPageVisibility = "Collapsed";
                    ParameterPage2Visibility = "Collapsed";
                    ParameterPage3Visibility = "Visible";
                    break;
                default:
                    break;
            }
        }
        private async void LoginAciton()
        {
            List<string> r;
            if (!IsLogin)
            {
                r = await mydialog.showlogin();
                if (r[1] == "ABCD4321")
                {
                    IsLogin = true;
                }
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
                    CloseAlarmText();
                    break;
                case "2":
                    robot1.PauseOperate();
                    robot2.PauseOperate();
                    break;
                case "3":
                    robot1.ContinueOperate();
                    robot2.ContinueOperate();
                    CloseAlarmText();
                    break;
                case "4":
                    mydialog.changeaccent("red");
                    CloseAlarmText();
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
                USBCameraConnect = camera.status;
            }
 
        }
        private void ModelPrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
        }
        private void Robot1PrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
            switch (str)
            {
                case "MsgRev: 右侧膜吸取失败":
                    ShowAlarmText("右侧膜吸取失败");
                    break;
                case "MsgRev: 左侧膜吸取失败":
                    ShowAlarmText("左侧膜吸取失败");
                    break;
                case "MsgRev: 拍照失败":
                    ShowAlarmText("双面胶拍照失败");
                    break;
                case "MsgRev: 撕下膜失败":
                    ShowAlarmText("机械手1撕下膜失败");
                    break;
                default:
                    break;
            }
        }
        private void Robot2PrintEventProcess(string str)
        {
            MsgText = AddMessage(str);
            switch (str)
            {
                case "MsgRev: 拍照失败":
                    ShowAlarmText("钢片拍照失败");
                    break;
                
                default:
                    break;
            }
        }
        private void Robot1StatusUpdateProcess(string str)
        {
            EpsonStatusAuto = str[2] == '1';
            EpsonStatusWarning = str[3] == '1';
            EpsonStatusSError = str[4] == '1';
            EpsonStatusSafeGuard = str[5] == '1';
            EpsonStatusEStop = str[6] == '1';
            EpsonStatusError = str[7] == '1';
            deltaAS300.YY[48] = EpsonStatusPaused = str[8] == '1';
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
            deltaAS300.YY[49] = EpsonStatusPaused1 = str[8] == '1';
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
                R1XOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R1", "R1XOffset1", "0"));
                R1YOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R1", "R1YOffset1", "0"));
                R1UOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R1", "R1UOffset1", "0"));

                R2XOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset1", "0"));
                R2YOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset1", "0"));
                R2UOffset1 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset1", "0"));

                R2XOffset2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset2", "0"));
                R2YOffset2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset2", "0"));
                R2UOffset2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset2", "0"));

                R2XOffset3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset3", "0"));
                R2YOffset3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset3", "0"));
                R2UOffset3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset3", "0"));

                R2XOffset4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset4", "0"));
                R2YOffset4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset4", "0"));
                R2UOffset4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset4", "0"));

                R2XOffset5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset5", "0"));
                R2YOffset5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset5", "0"));
                R2UOffset5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset5", "0"));

                R2XOffset1_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset1_2", "0"));
                R2YOffset1_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset1_2", "0"));
                R2UOffset1_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset1_2", "0"));

                R2XOffset2_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset2_2", "0"));
                R2YOffset2_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset2_2", "0"));
                R2UOffset2_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset2_2", "0"));

                R2XOffset3_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset3_2", "0"));
                R2YOffset3_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset3_2", "0"));
                R2UOffset3_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset3_2", "0"));

                R2XOffset4_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset4_2", "0"));
                R2YOffset4_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset4_2", "0"));
                R2UOffset4_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset4_2", "0"));

                R2XOffset5_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset5_2", "0"));
                R2YOffset5_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset5_2", "0"));
                R2UOffset5_2 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset5_2", "0"));

                R2XOffset1_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset1_3", "0"));
                R2YOffset1_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset1_3", "0"));
                R2UOffset1_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset1_3", "0"));

                R2XOffset2_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset2_3", "0"));
                R2YOffset2_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset2_3", "0"));
                R2UOffset2_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset2_3", "0"));

                R2XOffset3_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset3_3", "0"));
                R2YOffset3_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset3_3", "0"));
                R2UOffset3_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset3_3", "0"));

                R2XOffset4_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset4_3", "0"));
                R2YOffset4_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset4_3", "0"));
                R2UOffset4_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset4_3", "0"));

                R2XOffset5_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset5_3", "0"));
                R2YOffset5_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset5_3", "0"));
                R2UOffset5_3 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset5_3", "0"));

                R2XOffset1_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset1_4", "0"));
                R2YOffset1_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset1_4", "0"));
                R2UOffset1_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset1_4", "0"));

                R2XOffset2_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset2_4", "0"));
                R2YOffset2_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset2_4", "0"));
                R2UOffset2_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset2_4", "0"));

                R2XOffset3_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset3_4", "0"));
                R2YOffset3_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset3_4", "0"));
                R2UOffset3_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset3_4", "0"));

                R2XOffset4_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset4_4", "0"));
                R2YOffset4_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset4_4", "0"));
                R2UOffset4_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset4_4", "0"));

                R2XOffset5_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset5_4", "0"));
                R2YOffset5_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset5_4", "0"));
                R2UOffset5_4 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset5_4", "0"));

                R2XOffset1_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset1_5", "0"));
                R2YOffset1_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset1_5", "0"));
                R2UOffset1_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset1_5", "0"));

                R2XOffset2_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset2_5", "0"));
                R2YOffset2_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset2_5", "0"));
                R2UOffset2_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset2_5", "0"));

                R2XOffset3_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset3_5", "0"));
                R2YOffset3_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset3_5", "0"));
                R2UOffset3_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset3_5", "0"));

                R2XOffset4_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset4_5", "0"));
                R2YOffset4_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset4_5", "0"));
                R2UOffset4_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset4_5", "0"));

                R2XOffset5_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2XOffset5_5", "0"));
                R2YOffset5_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2YOffset5_5", "0"));
                R2UOffset5_5 = double.Parse(Inifile.INIGetStringValue(iniCameraDataPath, "R2", "R2UOffset5_5", "0"));

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
