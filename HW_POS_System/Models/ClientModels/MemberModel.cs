using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.Models.ClientModels
{
    public class MemberModel
    {
        #region Fields
        public MemberDataModel _MemberSelectValue { get; set; }
        public GenderDataModel _GenderSelectValue { get; set; }
        public Visibility _MemberVis { get; set; }
        public Visibility _ConsTotalVis { get; set; }
        public string _TextBoxName { get; set; }
        public string _TextBoxMemberRank { get; set; }
        public string _TextBoxPhoneNumber { get; set; }
        public string _TextBoxAddress { get; set; }
        public string _TextBoxPS { get; set; }
        public int _TextBoxContTotal { get; set; }
        public bool _DataGridEnable { get; set; }
        public bool _MemberComboBoxEnable { get; set; }
        public bool _MemberTextBoxEnable { get; set; }
        public bool _MemberAddSetBtnEnable { get; set; }
        public bool _MemberEditSetBtnEnable { get; set; }
        public bool _MemberDelSetBtnEnable { get; set; }
        public bool _SandCBtnEnable { get; set; }
        
        #endregion
    }
    public class GenderDataModel
    {
        public string Gender { get; set; }
    }
    public class MemberDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberRank { get; set; }
    }
    public class ClientMemberDataModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MemberRank { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PS { get; set; }
        public int ConsTotal { get; set; }
    }
    public class UpdateMemberData
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MemberRank { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PS { get; set; }
    }

    public class InsertMemberData
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MemberRank { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PS { get; set; }
        public int ConsTotal { get; set; }
    }
}
