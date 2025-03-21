using HW_POS_Server.Common;
using HW_POS_Server.Models;
using HW_POS_Server.Models.ClientModels;
using HW_POS_Server.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace HW_POS_Server.Views.ServerViews
{
    /// <summary>
    /// MemberConsReport.xaml 的互動邏輯
    /// </summary>
    public partial class MemberConsReportControl : UserControl
    {
        public static string ContentStart = @"MemberConsReport.rdlc";
        ServerViewModel ViewModel { get; set; }
        WebAPIConnect APIConnect = new WebAPIConnect();
        ObservableCollection<ClientMemberDataModel> ClientMemberData = new ObservableCollection<ClientMemberDataModel>();
        public MemberConsReportControl()
        {
            InitializeComponent();
            APIConnect.connectOpen();
            ViewModel = new ServerViewModel();
            HttpResponseMessage response = APIConnect.ResponseGet("MemberData");
            if (response.IsSuccessStatusCode)
            {
                ClientMemberData = response.Content.ReadAsAsync<ObservableCollection<ClientMemberDataModel>>().Result;
                for (int i = 0; i < ClientMemberData.Count; i++)
                {
                    DateTime MemberLastConsTime = DateTime.MinValue;
                    string StringMemberLastConsTime = "Null";
                    response = APIConnect.ResponseGet($@"MemberData/MemberConsumeData/ConsTime/{ClientMemberData[i].ID}");
                    if (response.IsSuccessStatusCode)
                        MemberLastConsTime = response.Content.ReadAsAsync<DateTime>().Result;
                    if (MemberLastConsTime != DateTime.MinValue) StringMemberLastConsTime = MemberLastConsTime.ToString("yyyy/MM/dd HH:mm:ss");
                    else StringMemberLastConsTime = "Null";
                    ViewModel.MemberConsReport.Add(new MemberConsReportModel()
                    {
                        MemberName = ClientMemberData[i].Name,
                        MemberGender = ClientMemberData[i].Gender.Trim(),
                        ConsTotal = ClientMemberData[i].ConsTotal,
                        MemberRank = ClientMemberData[i].MemberRank.Trim(),
                        LastConsTime = StringMemberLastConsTime,
                    });
                }
            }
            _reportviewer.LocalReport.DataSources.Clear();

            ReportDataSource source = new ReportDataSource("MemberConsDB", ViewModel.MemberConsReport);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.LocalReport.ReportPath = ContentStart;
            _reportviewer.LocalReport.DataSources.Add(source);
            _reportviewer.SetDisplayMode(DisplayMode.Normal);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }
    }
}
