using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Schema;

namespace HW_POS_Server.Models.ClientModels
{
    public class OrderModel
    {
        #region Fields
        public bool _RadioButtonChecked1 { get; set; }
        public bool _RadioButtonChecked2 { get; set; }
        public bool _RadioButtonChecked3 { get; set; }
        public bool _RadioButtonChecked4 { get; set; }
        public bool _RadioButtonChecked5 { get; set; }
        public bool _OrderButtonEnable { get; set; }
        public bool _BillButtonEnable { get; set; }
        public Visibility _OrderVis { get; set; }
        public Visibility _SearchMemberVis { get; set; }
        public Visibility _IsSearchedYes { get; set; }
        public Visibility _IsSearchedNo { get; set; }
        public Visibility _ShowMemberVis { get; set; }
        public Visibility _RadioBtnVis1 { get; set; }
        public Visibility _RadioBtnVis2 { get; set; }
        public Visibility _RadioBtnVis3 { get; set; }
        public Visibility _RadioBtnVis4 { get; set; }
        public Visibility _RadioBtnVis5 { get; set; }
        public Visibility _ItemClassNextPageVis { get; set; }
        public Visibility _ItemClassPreviousPageVis { get; set; }
        public Visibility _ItemNextPageVis { get; set; }
        public Visibility _ItemPreviousPageVis { get; set; }
        public OrderListDataModel _OrderListSelectValue { get; set; }
        public string _TextBoxMemberPhone { get; set; }
        public string _TextMemberName { get; set; }
        public string _NowMemberName { get; set; }
        public string _NowMemberGender { get; set; }
        public string _NowMemberRank { get; set; }

        #endregion
    }
    public class OrderListDataModel
    {
        #region Fields
        public string OrderName { get; set; }
        public int OrderAmount { get; set; }
        public int OrderCash { get; set; }
        public int OrderSubTotal { get; set; }

        #endregion
    }
    public class RadioClassModel
    {
        #region Fields
        public string _ItemClassRadio1 { get; set; }
        public string _ItemClassRadio2 { get; set; }
        public string _ItemClassRadio3 { get; set; }
        public string _ItemClassRadio4 { get; set; }
        public string _ItemClassRadio5 { get; set; }

        #endregion
    }

    public class ItemButtonsModel
    {
        #region Fields
        public ICommand Item1ClickCommand { get; set; }
        public ICommand Item2ClickCommand { get; set; }
        public ICommand Item3ClickCommand { get; set; }
        public ICommand Item4ClickCommand { get; set; }
        public ICommand Item5ClickCommand { get; set; }
        public ICommand Item6ClickCommand { get; set; }
        public ICommand Item7ClickCommand { get; set; }
        public ICommand Item8ClickCommand { get; set; }
        public ICommand Item9ClickCommand { get; set; }
        public ICommand Item10ClickCommand { get; set; }
        public ICommand Item11ClickCommand { get; set; }
        public ICommand Item12ClickCommand { get; set; }
        public ICommand Item13ClickCommand { get; set; }
        public ICommand Item14ClickCommand { get; set; }
        public ICommand Item15ClickCommand { get; set; }
        public ICommand Item16ClickCommand { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
        public string Item6 { get; set; }
        public string Item7 { get; set; }
        public string Item8 { get; set; }
        public string Item9 { get; set; }
        public string Item10 { get; set; }
        public string Item11 { get; set; }
        public string Item12 { get; set; }
        public string Item13 { get; set; }
        public string Item14 { get; set; }
        public string Item15 { get; set; }
        public string Item16 { get; set; }
        public Visibility Item1Vis { get; set; }
        public Visibility Item2Vis { get; set; }
        public Visibility Item3Vis { get; set; }
        public Visibility Item4Vis { get; set; }
        public Visibility Item5Vis { get; set; }
        public Visibility Item6Vis { get; set; }
        public Visibility Item7Vis { get; set; }
        public Visibility Item8Vis { get; set; }
        public Visibility Item9Vis { get; set; }
        public Visibility Item10Vis { get; set; }
        public Visibility Item11Vis { get; set; }
        public Visibility Item12Vis { get; set; }
        public Visibility Item13Vis { get; set; }
        public Visibility Item14Vis { get; set; }
        public Visibility Item15Vis { get; set; }
        public Visibility Item16Vis { get; set; }
        #endregion
    }
    public class InsertMemberConsumeData
    {
        #region Fields
        public int MemberID { get; set; }
        public string OrderNumber { get; set; }
        public int Total { get; set; }

        #endregion
    }
}
