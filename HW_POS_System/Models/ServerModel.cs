using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HW_POS_Server.Models
{
    public class ServerModel
    {
        #region Server Fields
        public string _AccountBox { get; set; }
        //public SecureString _PasswordBox { get; set; }
        public SecureString _PasswordBox { get; set; }
        public string _StaffName { get; set; }
        public string _StaffGender { get; set; }
        public bool _CheckOutReportChecked { get; set; }
        public bool _MemberConsReportChecked { get; set; }
        public bool _DatePickerEnable { get; set; }
        public Visibility _LoginSCVis { get; set; }
        public Visibility _ServerSCVis { get; set; }
        public Visibility _ReportVis { get; set; }
        public Visibility _DateControlVis { get; set; }
        public Visibility _ServerButtonVis { get; set; }
        public DateTime _StartDateData { get; set; }
        public DateTime _EndDateData { get; set; }
        #endregion

        #region Client Fields
        public Visibility _ClientButtonVis { get; set; }

        #endregion
    }
    public class CheckOutReportModel
    {
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string ItemName { get; set; }
        public int ItemAmount { get; set; }
        public int ItemSubTotal { get; set; }
        public int OrderTotal { get; set; }
        public string StaffName { get; set; }
    }
    public class MemberConsReportModel
    {
        public string MemberName { get; set; }
        public string MemberGender { get; set; }
        public int ConsTotal { get; set; }
        public string MemberRank { get; set; }
        public string LastConsTime { get; set; }
    }
}
