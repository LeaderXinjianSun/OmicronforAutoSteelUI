using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace OmicronforAutoSteel
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
            var doc = new System.Windows.Xps.Packaging.XpsDocument(Environment.CurrentDirectory + "\\自动贴钢片机说明书.xps", System.IO.FileAccess.Read);
            var xps = doc.GetFixedDocumentSequence();
            DocumentViewer1.Document = xps;
        }
    }
}
