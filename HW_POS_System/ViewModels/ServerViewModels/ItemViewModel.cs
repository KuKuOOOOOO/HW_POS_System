using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HW_POS_Server.ViewModels.ServerViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        #region Fields

        private ItemModel Item = new ItemModel();

        private SQLCommandClass commandClass = new SQLCommandClass();

        #endregion
        public ItemViewModel()
        {
            #region Constructor

            Item = new ItemModel()
            {
                _ItemVis=Visibility.Collapsed,
                _ItemClassEditorVis = Visibility.Hidden,
                _TextBoxItemClassName = "",
                _TextBoxItemClassPS = "",
                _ItemClassChecked = false,
                _ItemEditorVis = Visibility.Hidden,
                _TextBoxItemName = "",
                _TextBoxItemCash = 0,
                _TextBoxItemPS = "",
                _ItemChecked = false,
                _DataGridEnable = true,
                _ItemClassAddSetBtnEnable = true,
                _ItemClassEditSetBtnEnable = false,
                _ItemClassDelSetBtnEnable = false,
                _ItemAddSetBtnEnable = false,
                _ItemEditSetBtnEnable = false,
                _ItemDelSetBtnEnable = false,
            };
            List<int> ListItemClassID = commandClass.SelectItemClassIDAndAlive();
            for (int i = 0; i < ListItemClassID.Count; i++)
                ItemClassData.Add(new ItemClassDataModel()
                {
                    ID = ListItemClassID[i],
                    ClassName = commandClass.SelectItemClassNameFromID(ListItemClassID[i]),
                    ItemClassEnable = commandClass.SelectItemClassEnableFromID(ListItemClassID[i]),
                });

            #endregion
        }

        #region Properties

        #region ObservableCollection Properties
        ObservableCollection<ItemClassDataModel> _ItemClassData = new ObservableCollection<ItemClassDataModel>();
        public ObservableCollection<ItemClassDataModel> ItemClassData
        {
            get { return _ItemClassData; }
            set
            {
                _ItemClassData = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<ItemDataModel> _ItemData = new ObservableCollection<ItemDataModel>();
        public ObservableCollection<ItemDataModel> ItemData
        {
            get { return _ItemData; }
            set
            {
                _ItemData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Bool Properties
        public bool ItemClassChecked
        {
            get { return Item._ItemClassChecked; }
            set
            {
                Item._ItemClassChecked = value;
                OnPropertyChanged();
            }
        }
        public bool ItemChecked
        {
            get { return Item._ItemChecked; }
            set
            {
                Item._ItemChecked = value;
                OnPropertyChanged();
            }
        }
        public bool DataGridEnable
        {
            get { return Item._DataGridEnable; }
            set
            {
                Item._DataGridEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemClassAddSetBtnEnable
        {
            get { return Item._ItemClassAddSetBtnEnable; }
            set
            {
                Item._ItemClassAddSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemClassEditSetBtnEnable
        {
            get { return Item._ItemClassEditSetBtnEnable; }
            set
            {
                Item._ItemClassEditSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemClassDelSetBtnEnable
        {
            get { return Item._ItemClassDelSetBtnEnable; }
            set
            {
                Item._ItemClassDelSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemAddSetBtnEnable
        {
            get { return Item._ItemAddSetBtnEnable; }
            set
            {
                Item._ItemAddSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemEditSetBtnEnable
        {
            get { return Item._ItemEditSetBtnEnable; }
            set
            {
                Item._ItemEditSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool ItemDelSetBtnEnable
        {
            get { return Item._ItemDelSetBtnEnable; }
            set
            {
                Item._ItemDelSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region String Properties
        public string TextBoxItemClassName
        {
            get { return Item._TextBoxItemClassName; }
            set
            {
                Item._TextBoxItemClassName = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxItemClassPS
        {
            get { return Item._TextBoxItemClassPS; }
            set
            {
                Item._TextBoxItemClassPS = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxItemName
        {
            get { return Item._TextBoxItemName; }
            set
            {
                Item._TextBoxItemName = value;
                OnPropertyChanged();
            }
        }
        public int TextBoxItemCash
        {
            get { return Item._TextBoxItemCash; }
            set
            {
                Item._TextBoxItemCash = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxItemPS
        {
            get { return Item._TextBoxItemPS; }
            set
            {
                Item._TextBoxItemPS = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Visibility Properties
        public Visibility ItemVis
        {
            get { return Item._ItemVis; }
            set
            {
                Item._ItemVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemClassEditorVis
        {
            get { return Item._ItemClassEditorVis; }
            set
            {
                Item._ItemClassEditorVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemEditorVis
        {
            get { return Item._ItemEditorVis; }
            set
            {
                Item._ItemEditorVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Item Class DataGrid Properties
        public ItemClassDataModel ItemClassSelectValue
        {
            get { return Item._ItemClassSelectValue; }
            set
            {
                Item._ItemClassSelectValue = value;
                OnPropertyChanged();
                ItemClassBtnAndItemBtnEnableJudge();
                PopItemClass();
            }
        }
        #endregion
        #region Item DataGrid Properties
        public ItemDataModel ItemSelectValue
        {
            get { return Item._ItemSelectValue; }
            set
            {
                Item._ItemSelectValue = value;
                OnPropertyChanged();
                ItemClassBtnAndItemBtnEnableJudge();
            }
        }
        #endregion
        #region Button Properties
        public ICommand ItemCommand
        {
            get { return new RelayCommand(ShowItem, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedItem, CanExecute); }
        }
        public ICommand ItemClassAddBtn
        {
            get { return new RelayCommand(ItemClassAdd, CanExecute); }
        }
        public ICommand ItemClassEditBtn
        {
            get { return new RelayCommand(ItemClassEdit, CanExecute); }
        }
        public ICommand ItemClassDelBtn
        {
            get { return new RelayCommand(ItemClassDelete, CanExecute); }
        }
        public ICommand ItemClassSaveBtn
        {
            get { return new RelayCommand(ItemClassSave, CanExecute); }
        }
        public ICommand ItemClassCancelBtn
        {
            get { return new RelayCommand(ItemClassCancel, CanExecute); }
        }
        public ICommand ItemAddBtn
        {
            get { return new RelayCommand(ItemAdd, CanExecute); }
        }
        public ICommand ItemEditBtn
        {
            get { return new RelayCommand(ItemEdit, CanExecute); }
        }
        public ICommand ItemDelBtn
        {
            get { return new RelayCommand(ItemDelete, CanExecute); }
        }
        public ICommand ItemSaveBtn
        {
            get { return new RelayCommand(ItemSave, CanExecute); }
        }
        public ICommand ItemCancelBtn
        {
            get { return new RelayCommand(ItemCancel, CanExecute); }
        }
        #endregion

        #endregion

        #region Public methods

        private int ItemClassID;
        private int ItemID;
        private bool AddOrEdit; // Add = True, Edit = False

        public void ShowItem()
        {
            ItemVis = Visibility.Visible;
        }
        public void CollapsedItem()
        {
            ItemVis = Visibility.Collapsed;
        }
        public void PopItemClass()
        {
            ItemData.Clear();
            if (ItemClassSelectValue != null)
            {
                List<int> ListItemID = commandClass.SelectItemIDFromItemClassIDAndAlive(ItemClassSelectValue.ID);
                for (int i = 0; i < ListItemID.Count; i++)
                {
                    string ItemName = commandClass.SelectItemNameFromIDAndAlive(ListItemID[i]);
                    ItemData.Add(new ItemDataModel()
                    {
                        ID = ListItemID[i],
                        ItemName = ItemName,
                        ItemCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectValue.ID, ListItemID[i]),
                        ItemEnable = commandClass.SelectItemEnableFromItemClassandItemID(ItemClassSelectValue.ID, ListItemID[i]),
                    });
                }
            }

        }
        public void ItemClassAdd()
        {
            List<int> ListItemClassID = commandClass.SelectItemClassIDAndAlive();
            ItemClassEditorVis = Visibility.Visible;
            TextBoxItemClassName = "";
            TextBoxItemClassPS = "";
            ItemClassID = ListItemClassID[ListItemClassID.Count - 1] + 1;
            ItemClassAddSetBtnEnable = false;
            ItemClassEditSetBtnEnable = false;
            ItemClassDelSetBtnEnable = false;
            ItemAddSetBtnEnable = false;
            ItemEditSetBtnEnable = false;
            ItemDelSetBtnEnable = false;
            ItemClassChecked = false;
            DataGridEnable = false;
            AddOrEdit = true;
        }
        public void ItemClassEdit()
        {
            if (ItemClassSelectValue != null)
            {
                ItemClassEditorVis = Visibility.Visible;
                TextBoxItemClassName = ItemClassSelectValue.ClassName;
                TextBoxItemClassPS = commandClass.SelectItemClassPSFromID(ItemClassSelectValue.ID);
                ItemClassID = ItemClassSelectValue.ID;
                ItemClassChecked = ItemClassSelectValue.ItemClassEnable;
                ItemClassAddSetBtnEnable = false;
                ItemClassEditSetBtnEnable = false;
                ItemClassDelSetBtnEnable = false;
                ItemAddSetBtnEnable = false;
                ItemEditSetBtnEnable = false;
                ItemDelSetBtnEnable = false;
                DataGridEnable = false;
                AddOrEdit = false;
            }
        }
        public void ItemClassDelete()
        {
            int Pre_ItemClassPos = 0;
            if (ItemClassSelectValue != null)
            {
                Pre_ItemClassPos = ItemClassData.IndexOf(ItemClassSelectValue);
                commandClass.DeleteItemClassToDB(ItemClassSelectValue.ID);
                ItemClassData.Clear();
                List<int> ListItemClassID = commandClass.SelectItemClassIDAndAlive();
                for (int i = 0; i < ListItemClassID.Count; i++)
                    ItemClassData.Add(new ItemClassDataModel()
                    {
                        ID = ListItemClassID[i],
                        ClassName = commandClass.SelectItemClassNameFromID(ListItemClassID[i]),
                        ItemClassEnable = commandClass.SelectItemClassEnableFromID(ListItemClassID[i]),
                    });
                //ItemClassData.Remove(ItemClassData[Pre_ItemClassPos]);
                //for (int i = Pre_ItemClassPos; i < ItemClassData.Count; i++) { ItemClassData[i].ID = i + 1; commandClass.ItemClassAutoOrderReset(i + 1); }
                //ItemClassData = new ObservableCollection<ItemClassDataModel>(ItemClassData.OrderBy(x => x.ID));
            }
        }
        public void ItemClassSave()
        {
            if (TextBoxItemClassName != null && TextBoxItemClassName != "")
            {

                if (AddOrEdit == true)
                {
                    //ItemClassData.Add(new ItemClassDataModel { ID = ItemClassID, ClassName = TextBoxItemClassName, ItemClassEnable = ItemClassChecked });
                    commandClass.InsertItemClassToDB(TextBoxItemClassName, ItemClassChecked, TextBoxItemClassPS);
                    ItemClassSelectValue = null;
                }
                else if (AddOrEdit == false)
                {
                    commandClass.UpdateItemClassToDB(ItemClassID, TextBoxItemClassName, ItemClassChecked, TextBoxItemClassPS);
                    //foreach (var item in ItemClassData.Where(x => x.ClassName == ItemClassSelectValue.ClassName))
                    //{ item.ID = ItemClassID; item.ClassName = TextBoxItemClassName; item.ItemClassEnable = ItemClassChecked; }
                    //ItemClassData.Add(new ItemClassDataModel { ID = ItemClassID, ClassName = TextBoxItemClassName, ItemClassEnable = ItemClassChecked });
                    //ItemClassData.Remove(ItemClassData[i]);
                    //ItemClassData = new ObservableCollection<ItemClassDataModel>(ItemClassData.OrderBy(x => x.ID));
                }
                int Pre_SelectValue = ItemClassData.IndexOf(ItemClassSelectValue), Pre_ItemClassCount = ItemClassData.Count;
                ItemClassData.Clear();
                List<int> ListItemClassID = commandClass.SelectItemClassIDAndAlive();
                for (int i = 0; i < ListItemClassID.Count; i++)
                    ItemClassData.Add(new ItemClassDataModel()
                    {
                        ID = ListItemClassID[i],
                        ClassName = commandClass.SelectItemClassNameFromID(ListItemClassID[i]),
                        ItemClassEnable = commandClass.SelectItemClassEnableFromID(ListItemClassID[i]),
                    });
                ItemClassCancel();
                if (Pre_SelectValue != -1)
                    ItemClassSelectValue = ItemClassData[Pre_SelectValue];
                else if (ItemClassData.Count == Pre_ItemClassCount + 1)
                    ItemClassSelectValue = ItemClassData[ItemClassData.Count - 1];
                else
                    ItemClassSelectValue = null;
            }
            else
            {
                MessageBox.Show("請輸入商品類別名稱", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                
        }
        public void ItemClassCancel()
        {
            ItemClassEditorVis = Visibility.Hidden;
            TextBoxItemClassName = "";
            TextBoxItemClassPS = "";
            ItemClassBtnAndItemBtnEnableJudge();
            ItemClassChecked = false;
            DataGridEnable = true;
        }
        public void ItemAdd()
        {
            List<int> ListItemID = commandClass.SelectItemIDAndAlive();
            if (ItemClassSelectValue != null)
            {
                ItemEditorVis = Visibility.Visible;
                TextBoxItemName = "";
                TextBoxItemCash = 0;
                TextBoxItemPS = "";
                ItemID = ListItemID[ListItemID.Count - 1] + 1;
                ItemClassAddSetBtnEnable = false;
                ItemClassEditSetBtnEnable = false;
                ItemClassDelSetBtnEnable = false;
                ItemAddSetBtnEnable = false;
                ItemEditSetBtnEnable = false;
                ItemDelSetBtnEnable = false;
                ItemChecked = false;
                DataGridEnable = false;
                AddOrEdit = true;
            }
        }
        public void ItemEdit()
        {
            if (ItemSelectValue != null)
            {
                ItemEditorVis = Visibility.Visible;
                TextBoxItemName = ItemSelectValue.ItemName;
                TextBoxItemCash = ItemSelectValue.ItemCash;
                TextBoxItemPS = commandClass.SelectItemPSFromItemName(ItemSelectValue.ItemName);
                ItemID = ItemSelectValue.ID;
                ItemChecked = ItemSelectValue.ItemEnable;
                ItemClassAddSetBtnEnable = false;
                ItemClassEditSetBtnEnable = false;
                ItemClassDelSetBtnEnable = false;
                ItemAddSetBtnEnable = false;
                ItemEditSetBtnEnable = false;
                ItemDelSetBtnEnable = false;
                DataGridEnable = false;
                AddOrEdit = false;
            }
        }
        public void ItemDelete()
        {
            if (ItemSelectValue != null)
            {
                int Pre_ItemPos = 0;
                Pre_ItemPos = ItemData.IndexOf(ItemSelectValue);
                commandClass.DeleteItemToDB(ItemSelectValue.ItemName);
                ItemData.Remove(ItemData[Pre_ItemPos]);
                ItemData = new ObservableCollection<ItemDataModel>(ItemData.OrderBy(x => x.ID));
            }
        }
        public void ItemSave()
        {
            if (TextBoxItemName != null && TextBoxItemName != "" && ItemClassSelectValue != null)
            {
                if (AddOrEdit == true)
                {
                    //ItemData.Add(new ItemDataModel { ID = ItemID, ItemName = TextBoxItemName, ItemCash = TextBoxItemCash, ItemEnable = ItemChecked });
                    commandClass.InsertItemToDB(TextBoxItemName, ItemClassSelectValue.ID, ItemClassSelectValue.ClassName, TextBoxItemCash, ItemChecked, TextBoxItemPS);
                    ItemSelectValue = null;
                    //commandClass.ItemReSeedMaxVal();
                }
                else if (AddOrEdit == false)
                {
                    commandClass.UpdateItemToDB(ItemID, TextBoxItemName, ItemClassSelectValue.ID,ItemClassSelectValue.ClassName, TextBoxItemCash, ItemChecked, TextBoxItemPS);
                    //foreach (var item in ItemData.Where(x => x.ItemName == ItemSelectValue.ItemName))
                    //{ item.ID = ItemID; item.ItemName = TextBoxItemName; item.ItemCash = TextBoxItemCash; item.ItemEnable = ItemChecked; }
                    //ItemData.Add(new ItemDataModel { ID = ItemID, ItemName = TextBoxItemName, ItemCash = TextBoxItemCash, ItemEnable = ItemChecked });
                    //ItemData.Remove(ItemData[]);
                    //ItemData = new ObservableCollection<ItemDataModel>(ItemData.OrderBy(x => x.ID));
                }
                int Pre_SelectValue = ItemData.IndexOf(ItemSelectValue), Pre_ItemCount = ItemData.Count;
                ItemData.Clear();
                List<int> ListItemID = commandClass.SelectItemIDFromItemClassIDAndAlive(ItemClassSelectValue.ID);
                for (int i = 0; i < ListItemID.Count; i++)
                {
                    string ItemName = commandClass.SelectItemNameFromIDAndAlive(ListItemID[i]);
                    ItemData.Add(new ItemDataModel()
                    {
                        ID = ListItemID[i],
                        ItemName = ItemName,
                        ItemCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectValue.ID, ListItemID[i]),
                        ItemEnable = commandClass.SelectItemEnableFromItemClassandItemID(ItemClassSelectValue.ID, ListItemID[i]),
                    });
                }
                ItemCancel();
                if (Pre_SelectValue != -1)
                    ItemSelectValue = ItemData[Pre_SelectValue];
                else if (ItemData.Count == Pre_ItemCount + 1)
                    ItemSelectValue = ItemData[ItemData.Count - 1];
                else
                    ItemSelectValue = null;
            }
            else
                MessageBox.Show("請輸入商品名稱", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ItemCancel()
        {
            ItemEditorVis = Visibility.Hidden;
            TextBoxItemName = "";
            TextBoxItemCash = 0;
            TextBoxItemPS = "";
            ItemClassBtnAndItemBtnEnableJudge();
            ItemChecked = false;
            DataGridEnable = true;
        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion
        #region Private methods
        public void ItemClassBtnAndItemBtnEnableJudge()
        {
            if (ItemClassSelectValue != null && ItemSelectValue == null)
            {
                ItemClassAddSetBtnEnable = true;
                ItemClassEditSetBtnEnable = true;
                ItemClassDelSetBtnEnable = true;
                ItemAddSetBtnEnable = true;
                ItemEditSetBtnEnable = false;
                ItemDelSetBtnEnable = false;
            }
            else if (ItemClassSelectValue != null && ItemSelectValue != null)
            {
                ItemClassAddSetBtnEnable = true;
                ItemClassEditSetBtnEnable = true;
                ItemClassDelSetBtnEnable = true;
                ItemAddSetBtnEnable = true;
                ItemEditSetBtnEnable = true;
                ItemDelSetBtnEnable = true;
            }
            else
            {
                ItemClassAddSetBtnEnable = false;
                ItemClassEditSetBtnEnable = false;
                ItemClassDelSetBtnEnable = false;
                ItemAddSetBtnEnable = false;
                ItemEditSetBtnEnable = false;
                ItemDelSetBtnEnable = false;
            }
        }
        #endregion
    }
}
