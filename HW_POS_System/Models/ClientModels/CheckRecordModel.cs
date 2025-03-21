using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace HW_POS_Server.Models.ClientModels
{
    public class CheckRecordModel
    {
        #region Fields
        public CheckOutDataModel _CheckOutSelectValue { get; set; }
        public CheckItemDataModel _CheckItemSelectValue { get; set; }
        public DateTime _DateKeyword { get; set; }
        public string _OrderNumberKeyword { get; set; }
        public string _SelectValueMemberName { get; set; }
        public int _SelectValueTotal { get; set; }
        public int _SelectValueAmountTotal { get; set; }
        public ICollectionView _FilterCheckOutData { get; set; }
        public Visibility _CheckRecordVis { get; set; }
        #endregion
    }
    public class CheckOutDataModel
    {
        public string CheckNumber { get; set; }
        public string CheckTime { get; set; }
        public int CheckTotal { get; set; }
    }
    public class CheckItemDataModel
    {
        public string ItemName { get; set; }
        public int ItemAmount { get; set; }
        public int ItemCash { get; set; }
        public int ItemSubTotal { get; set; }
    }
}
