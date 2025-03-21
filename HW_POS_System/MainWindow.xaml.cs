using HW_POS_Server.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using HW_POS_Server.Views.ServerViews;
using System.Data;
using HW_POS_Server.DataBaseData;
using HW_POS_Server.Views.ClientViews;
using HW_POS_Server.ViewModels.ClientViewModels;

namespace HW_POS_Server
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ServerViewModel();
            ViewModel.Exit += ViewModel_Exit;
            this.DataContext = ViewModel;
        }
        private void ViewModel_Exit(object sender, ExitArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                System.Environment.Exit(0);
            }));
        }
    }
}
