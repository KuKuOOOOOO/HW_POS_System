using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.Models.ServerModels
{
    public class StaffModel
    {
        #region Fields
        public Visibility _StaffVis { get; set; }
        public int _TextBoxID { get; set; }
        public string _TextBoxName { get; set; }
        public string _TextBoxNickName { get; set; }
        public string _TextBoxPassword { get; set; }
        public Visibility _TextBoxPasswordVis { get; set; }
        public string _TextBoxAddress { get; set; }
        public string _TextBoxPhoneNumber { get; set; }
        public string _TextBoxPS { get; set; }
        public StaffDataModel _StaffSelectValue { get; set; }
        public bool _DataGridEnable { get; set; }
        public bool _StaffTextBoxEnable { get; set; }
        public bool _StaffComboBoxEnable { get; set; }
        public bool _AddStaffSetBtnEnable { get; set; }
        public bool _EditStaffSetBtnEnable { get; set; }
        public bool _DelStaffSetBtnEnable { get; set; }
        public bool _SandCBtnEnable { get; set; }
        public GenderDataModel _SelectGender { get; set; }
        public PermissionDataModel _SelectPermission { get; set; }

        #endregion
    }
    public class StaffDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Permission { get; set; }
    }

    public class GenderDataModel
    {
        public string Gender { get; set; }
    }
    public class PermissionDataModel
    {
        public string Permission { get; set; }
    }
}
