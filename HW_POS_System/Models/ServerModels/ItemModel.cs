using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.Models.ServerModels
{
    public class ItemModel
    {
        #region Fields
        public ItemClassDataModel _ItemClassSelectValue { get; set; }
        public ItemDataModel _ItemSelectValue { get; set; }
        public Visibility _ItemVis { get; set; }
        public Visibility _ItemClassEditorVis { get; set; }
        public string _TextBoxItemClassName { get; set; }
        public string _TextBoxItemClassPS { get; set; }
        public bool _ItemClassChecked { get; set; }
        public Visibility _ItemEditorVis { get; set; }
        public string _TextBoxItemName { get; set; }
        public int _TextBoxItemCash { get; set; }
        public string _TextBoxItemPS { get; set; }
        public bool _ItemChecked { get; set; }
        public bool _DataGridEnable { get; set; }
        public bool _ItemClassAddSetBtnEnable { get; set; }
        public bool _ItemClassEditSetBtnEnable { get; set; }
        public bool _ItemClassDelSetBtnEnable { get; set; }
        public bool _ItemAddSetBtnEnable { get; set; }
        public bool _ItemEditSetBtnEnable { get; set; }
        public bool _ItemDelSetBtnEnable { get; set; }

        #endregion
    }
    public class ItemClassDataModel
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public bool ItemClassEnable { get; set; }
    }
    public class ItemDataModel
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int ItemCash { get; set; }
        public bool ItemEnable { get; set; }
    }
}
