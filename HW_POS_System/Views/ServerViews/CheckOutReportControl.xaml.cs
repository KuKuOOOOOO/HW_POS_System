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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using HW_POS_Server.DataBaseData;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Windows.Threading;
using HW_POS_Server.Models;

namespace HW_POS_Server.Views.ServerViews
{
    /// <summary>
    /// ReportControl.xaml 的互動邏輯
    /// </summary>
    public partial class CheckOutReportControl : UserControl
    {
        public static string ContentStart = @"CheckOutReport.rdlc";
        ServerViewModel ViewModel { get; set; }
        private SQLCommandClass commandClass = new SQLCommandClass();
        public CheckOutReportControl()
        {
            InitializeComponent();
            List<string> ListOrderNumber = commandClass.SelectChekcOutListOrderNumberFromTrue(); 
            ViewModel = new ServerViewModel();
            for (int i = 0; i < ListOrderNumber.Count; i++)
            {
                List<int> ListOrderItemNumber = commandClass.SelectCheckOutDataOrderItemNumberFromOrderNumber(ListOrderNumber[i]);
                for (int j = 0; j < ListOrderItemNumber.Count; j++)
                {
                    ViewModel.CheckOutReport.Add(new CheckOutReportModel()
                    {
                        OrderNumber = ListOrderNumber[i],
                        OrderDate = commandClass.SelectCheckOutListOrderDateFromOrderNumber(ListOrderNumber[i]).ToString("yyyy/MM/dd"),
                        OrderTime = commandClass.SelectCheckOutListOrderTimeFromOrderNumber(ListOrderNumber[i]).ToString(),
                        ItemName = commandClass.SelectItemNameFromID(commandClass.SelectCheckOutDataItemIDFromOrderNumberAndOrderItemNumber(ListOrderNumber[i], ListOrderItemNumber[j])),
                        ItemAmount = commandClass.SelectCheckOutDataItemAmountFromOrderNumberAndOrderItemNumber(ListOrderNumber[i], ListOrderItemNumber[j]),
                        ItemSubTotal = commandClass.SelectCheckOutDataItemSubTotalFromOrderNumberAndOrderItemNumber(ListOrderNumber[i], ListOrderItemNumber[j]),
                        OrderTotal = commandClass.SelectCheckOutListOrderTotalFromOrderNumber(ListOrderNumber[i]),
                        StaffName = commandClass.SelectStaffNameFromID(commandClass.SelectCheckOutListStaffIDFromOrderNumber(ListOrderNumber[i]))
                    });
                }
            }
            _reportviewer.LocalReport.DataSources.Clear();
            
            ReportDataSource source = new ReportDataSource("CheckOutDB", ViewModel.CheckOutReport);
            _reportviewer.LocalReport.EnableExternalImages = true;
            _reportviewer.LocalReport.ReportPath = ContentStart;
            _reportviewer.LocalReport.DataSources.Add(source);
            _reportviewer.SetDisplayMode(DisplayMode.Normal);
            _reportviewer.Refresh();
            _reportviewer.RefreshReport();
        }
    }
}
