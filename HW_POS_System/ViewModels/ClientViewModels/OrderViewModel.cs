using HW_POS_Server.Common;
using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models.ClientModels;
using HW_POS_Server.Views.ClientViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace HW_POS_Server.ViewModels.ClientViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        #region Fields
        private OrderModel Order { get; set; }
        private SQLCommandClass commandClass = new SQLCommandClass();
        private WebAPIConnect APIConnect = new WebAPIConnect();
        private ClientMemberDataModel ClientMemberData = new ClientMemberDataModel();
        private RadioClassModel RadioClass { get; set; }
        private int ItemClassPage = 0;
        private List<int> ListItemClassID = new List<int>();

        #endregion
        public OrderViewModel()
        {
            #region Constrcutor

            APIConnect.connectOpen();
            ListItemClassID = commandClass.SelectItemClassIDFromEnableAndAlive();
            Order = new OrderModel()
            {
                _RadioButtonChecked1 = false,
                _RadioButtonChecked2 = false,
                _RadioButtonChecked3 = false,
                _RadioButtonChecked4 = false,
                _RadioButtonChecked5 = false,
                _OrderButtonEnable = false,
                _BillButtonEnable = false,
                _OrderVis = Visibility.Collapsed,
                _SearchMemberVis = Visibility.Hidden,
                _IsSearchedYes = Visibility.Hidden,
                _IsSearchedNo = Visibility.Hidden,
                _ShowMemberVis = Visibility.Hidden,
                _RadioBtnVis1 = Visibility.Visible,
                _RadioBtnVis2 = Visibility.Visible,
                _RadioBtnVis3 = Visibility.Visible,
                _RadioBtnVis4 = Visibility.Visible,
                _RadioBtnVis5 = Visibility.Visible,
                _ItemClassPreviousPageVis = Visibility.Hidden,
                _ItemClassNextPageVis = Visibility.Visible,
                _ItemNextPageVis = Visibility.Hidden,
                _ItemPreviousPageVis = Visibility.Hidden,
                _TextBoxMemberPhone = "",
                _TextMemberName = "",
                _NowMemberName = "",
                _NowMemberGender = "",
                _NowMemberRank = "",
            };

            if (ListItemClassID.Count <= 5) { for (int i = ListItemClassID.Count; i < 5; i++) ListItemClassID.Add(0); Order._ItemClassNextPageVis = Visibility.Hidden; }
            else Order._ItemClassNextPageVis = Visibility.Visible;
            RadioClass = new RadioClassModel()
            {
                _ItemClassRadio1 = commandClass.SelectItemClassNameFromID(ListItemClassID[0]),
                _ItemClassRadio2 = commandClass.SelectItemClassNameFromID(ListItemClassID[1]),
                _ItemClassRadio3 = commandClass.SelectItemClassNameFromID(ListItemClassID[2]),
                _ItemClassRadio4 = commandClass.SelectItemClassNameFromID(ListItemClassID[3]),
                _ItemClassRadio5 = commandClass.SelectItemClassNameFromID(ListItemClassID[4]),
            };
            if (RadioClass._ItemClassRadio1 == null) Order._RadioBtnVis1 = Visibility.Hidden;
            else Order._RadioBtnVis1 = Visibility.Visible;
            if (RadioClass._ItemClassRadio2 == null) Order._RadioBtnVis2 = Visibility.Hidden;
            else Order._RadioBtnVis2 = Visibility.Visible;
            if (RadioClass._ItemClassRadio3 == null) Order._RadioBtnVis3 = Visibility.Hidden;
            else Order._RadioBtnVis3 = Visibility.Visible;
            if (RadioClass._ItemClassRadio4 == null) Order._RadioBtnVis4 = Visibility.Hidden;
            else Order._RadioBtnVis4 = Visibility.Visible;
            if (RadioClass._ItemClassRadio5 == null) Order._RadioBtnVis5 = Visibility.Hidden;
            else Order._RadioBtnVis5 = Visibility.Visible;
            ItemButtons.Clear();
            ItemClassPage = 1;
            #endregion
        }

        #region Properties

        #region ObservableCollection Properties

        ObservableCollection<ItemButtonsModel> _ItemButtons = new ObservableCollection<ItemButtonsModel>();
        public ObservableCollection<ItemButtonsModel> ItemButtons
        {
            get { return _ItemButtons; }
            set
            {
                _ItemButtons = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<OrderListDataModel> _OrderListData = new ObservableCollection<OrderListDataModel>();
        public ObservableCollection<OrderListDataModel> OrderListData
        {
            get { return _OrderListData; }
            set
            {
                _OrderListData = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Visility Properties
        public Visibility OrderVis
        {
            get { return Order._OrderVis; }
            set
            {
                Order._OrderVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility SearchMemberVis
        {
            get { return Order._SearchMemberVis; }
            set
            {
                Order._SearchMemberVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility IsSearchedYes
        {
            get { return Order._IsSearchedYes; }
            set
            {
                Order._IsSearchedYes = value;
                OnPropertyChanged();
            }
        }
        public Visibility IsSearchedNo
        {
            get { return Order._IsSearchedNo; }
            set
            {
                Order._IsSearchedNo = value;
                OnPropertyChanged();
            }
        }
        public Visibility ShowMemberVis
        {
            get { return Order._ShowMemberVis; }
            set
            {
                Order._ShowMemberVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility RadioBtnVis1
        {
            get { return Order._RadioBtnVis1; }
            set
            {
                Order._RadioBtnVis1 = value;
                OnPropertyChanged();
            }
        }
        public Visibility RadioBtnVis2
        {
            get { return Order._RadioBtnVis2; }
            set
            {
                Order._RadioBtnVis2 = value;
                OnPropertyChanged();
            }
        }
        public Visibility RadioBtnVis3
        {
            get { return Order._RadioBtnVis3; }
            set
            {
                Order._RadioBtnVis3 = value;
                OnPropertyChanged();
            }
        }
        public Visibility RadioBtnVis4
        {
            get { return Order._RadioBtnVis4; }
            set
            {
                Order._RadioBtnVis4 = value;
                OnPropertyChanged();
            }
        }
        public Visibility RadioBtnVis5
        {
            get { return Order._RadioBtnVis5; }
            set
            {
                Order._RadioBtnVis5 = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemClassPreviousPageVis
        {
            get { return Order._ItemClassPreviousPageVis; }
            set
            {
                Order._ItemClassPreviousPageVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemClassNextPageVis
        {
            get { return Order._ItemClassNextPageVis; }
            set
            {
                Order._ItemClassNextPageVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemNextPageVis
        {
            get { return Order._ItemNextPageVis; }
            set
            {
                Order._ItemNextPageVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ItemPreviousPageVis
        {
            get { return Order._ItemPreviousPageVis; }
            set
            {
                Order._ItemPreviousPageVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region String Properties
        public bool RadioButtonChecked1
        {
            get { return Order._RadioButtonChecked1; }
            set
            {
                Order._RadioButtonChecked1 = value;
                OnPropertyChanged();
            }
        }
        public bool RadioButtonChecked2
        {
            get { return Order._RadioButtonChecked2; }
            set
            {
                Order._RadioButtonChecked2 = value;
                OnPropertyChanged();
            }
        }
        public bool RadioButtonChecked3
        {
            get { return Order._RadioButtonChecked3; }
            set
            {
                Order._RadioButtonChecked3 = value;
                OnPropertyChanged();
            }
        }
        public bool RadioButtonChecked4
        {
            get { return Order._RadioButtonChecked4; }
            set
            {
                Order._RadioButtonChecked4 = value;
                OnPropertyChanged();
            }
        }
        public bool RadioButtonChecked5
        {
            get { return Order._RadioButtonChecked5; }
            set
            {
                Order._RadioButtonChecked5 = value;
                OnPropertyChanged();
            }
        }
        public bool OrderButtonEnable
        {
            get { return Order._OrderButtonEnable; }
            set
            {
                Order._OrderButtonEnable = value;
                OnPropertyChanged();
            }
        }
        public bool BillButtonEnable
        {
            get { return Order._BillButtonEnable; }
            set
            {
                Order._BillButtonEnable = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxMemberPhone
        {
            get { return Order._TextBoxMemberPhone; }
            set
            {
                Order._TextBoxMemberPhone = value;
                OnPropertyChanged();
                SearchMemberID();
            }
        }
        public string TextMemberName
        {
            get { return Order._TextMemberName; }
            set
            {
                Order._TextMemberName = value;
                OnPropertyChanged();
            }
        }
        public string NowMemberName
        {
            get { return Order._NowMemberName; }
            set
            {
                Order._NowMemberName = value;
                OnPropertyChanged();
            }
        }
        public string NowMemberGender
        {
            get { return Order._NowMemberGender; }
            set
            {
                Order._NowMemberGender = value;
                OnPropertyChanged();
            }
        }
        public string NowMemberRank
        {
            get { return Order._NowMemberRank; }
            set
            {
                Order._NowMemberRank = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Order List(DataGrid) Properties
        public OrderListDataModel OrderListSelectValue
        {
            get { return Order._OrderListSelectValue; }
            set
            {
                Order._OrderListSelectValue = value;
                if (OrderListSelectValue != null)
                {
                    BillButtonEnable = true;
                    OrderButtonEnable = true;
                }
                else
                {
                    BillButtonEnable = false;
                    OrderButtonEnable = false;
                }
                OnPropertyChanged();
            }
        }
        #endregion
        #region ClassRadio Properties
        public string ItemClassRadio1
        {
            get { return RadioClass._ItemClassRadio1; }
            set
            {
                RadioClass._ItemClassRadio1 = value;
                OnPropertyChanged();
            }
        }
        public string ItemClassRadio2
        {
            get { return RadioClass._ItemClassRadio2; }
            set
            {
                RadioClass._ItemClassRadio2 = value;
                OnPropertyChanged();
            }
        }
        public string ItemClassRadio3
        {
            get { return RadioClass._ItemClassRadio3; }
            set
            {
                RadioClass._ItemClassRadio3 = value;
                OnPropertyChanged();
            }
        }
        public string ItemClassRadio4
        {
            get { return RadioClass._ItemClassRadio4; }
            set
            {
                RadioClass._ItemClassRadio4 = value;
                OnPropertyChanged();
            }
        }
        public string ItemClassRadio5
        {
            get { return RadioClass._ItemClassRadio5; }
            set
            {
                RadioClass._ItemClassRadio5 = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Button Properties
        #region Search Member Button Properties
        public ICommand SearchMemberYesBtn
        {
            get { return new RelayCommand(SearchMemberYes, CanExecute); }
        }
        public ICommand SearchMemberNoBtn
        {
            get { return new RelayCommand(SearchMemberNo, CanExecute); }
        }
        public ICommand SearchMemberCancelBtn
        {
            get { return new RelayCommand(SearchMemberCancel, CanExecute); }
        }
        #endregion
        public ICommand OrderCommand
        {
            get { return new RelayCommand(ShowOrder, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedScreen, CanExecute); }
        }
        public ICommand ItemClassPreviousPageCommand
        {
            get { return new RelayCommand(ItemClassPreviousPage, CanExecute); }
        }
        public ICommand ItemClassNextPageCommand
        {
            get { return new RelayCommand(ItemClassNextPage, CanExecute); }
        }
        public ICommand ItemPreviousPageCommand
        {
            get { return new RelayCommand(ItemPreviousPage, CanExecute); }
        }
        public ICommand ItemNextPageCommand
        {
            get { return new RelayCommand(ItemNextPage, CanExecute); }
        }
        public ICommand ItemClassCommand1
        {
            get { return new RelayCommand(ShowItemClass1, CanExecute); }
        }
        public ICommand ItemClassCommand2
        {
            get { return new RelayCommand(ShowItemClass2, CanExecute); }
        }
        public ICommand ItemClassCommand3
        {
            get { return new RelayCommand(ShowItemClass3, CanExecute); }
        }
        public ICommand ItemClassCommand4
        {
            get { return new RelayCommand(ShowItemClass4, CanExecute); }
        }
        public ICommand ItemClassCommand5
        {
            get { return new RelayCommand(ShowItemClass5, CanExecute); }
        }
        public ICommand Item1ClickCommand
        {
            get { return new RelayCommand(Item1Click, CanExecute); }
        }
        public ICommand Item2ClickCommand
        {
            get { return new RelayCommand(Item2Click, CanExecute); }
        }
        public ICommand Item3ClickCommand
        {
            get { return new RelayCommand(Item3Click, CanExecute); }
        }
        public ICommand Item4ClickCommand
        {
            get { return new RelayCommand(Item4Click, CanExecute); }
        }
        public ICommand Item5ClickCommand
        {
            get { return new RelayCommand(Item5Click, CanExecute); }
        }
        public ICommand Item6ClickCommand
        {
            get { return new RelayCommand(Item6Click, CanExecute); }
        }
        public ICommand Item7ClickCommand
        {
            get { return new RelayCommand(Item7Click, CanExecute); }
        }
        public ICommand Item8ClickCommand
        {
            get { return new RelayCommand(Item8Click, CanExecute); }
        }
        public ICommand Item9ClickCommand
        {
            get { return new RelayCommand(Item9Click, CanExecute); }
        }
        public ICommand Item10ClickCommand
        {
            get { return new RelayCommand(Item10Click, CanExecute); }
        }
        public ICommand Item11ClickCommand
        {
            get { return new RelayCommand(Item11Click, CanExecute); }
        }
        public ICommand Item12ClickCommand
        {
            get { return new RelayCommand(Item12Click, CanExecute); }
        }
        public ICommand Item13ClickCommand
        {
            get { return new RelayCommand(Item13Click, CanExecute); }
        }
        public ICommand Item14ClickCommand
        {
            get { return new RelayCommand(Item14Click, CanExecute); }
        }
        public ICommand Item15ClickCommand
        {
            get { return new RelayCommand(Item15Click, CanExecute); }
        }
        public ICommand Item16ClickCommand
        {
            get { return new RelayCommand(Item16Click, CanExecute); }
        }
        public ICommand IncreaseAmountCommand
        {
            get { return new RelayCommand(IncreaseAmount, CanExecute); }
        }
        public ICommand DecreaseAmountCommand
        {
            get { return new RelayCommand(DecreaseAmount, CanExecute); }
        }
        public ICommand DeleteItemCommand
        {
            get { return new RelayCommand(DeleteItem, CanExecute); }
        }
        public ICommand LoadMemberCommand
        {
            get { return new RelayCommand(LoadMember, CanExecute); }
        }
        public ICommand BillCommand
        {
            get { return new RelayCommand(Bill, CanExecute); }
        }


        #endregion

        #endregion

        #region Public methods

        private int ItemClassSelectNow;  // Now Item Class Select Value
        private int SerialNumber = 1;               // 流水號 
        private List<int> ShowItemNameID = new List<int>(); //Item Name ID from one ItemClass
        private int ItemPage = 0;        // Item's page now
        #region Page methods

        public void ItemClassPreviousPage()
        {
            if (ItemClassPage > 1) ItemClassPage--;
            else ItemClassPage = 1;
            if (ItemClassPage <= 1) ItemClassPreviousPageVis = Visibility.Hidden; ItemClassNextPageVis = Visibility.Visible;
            try
            {
                RadioBtnVis1 = Visibility.Visible;
                RadioBtnVis2 = Visibility.Visible;
                RadioBtnVis3 = Visibility.Visible;
                RadioBtnVis4 = Visibility.Visible;
                RadioBtnVis5 = Visibility.Visible;
                ItemClassRadio1 = commandClass.SelectItemClassNameFromID(ListItemClassID[5 * ItemClassPage - 5]);
                ItemClassRadio2 = commandClass.SelectItemClassNameFromID(ListItemClassID[5 * ItemClassPage - 4]);
                ItemClassRadio3 = commandClass.SelectItemClassNameFromID(ListItemClassID[5 * ItemClassPage - 3]);
                ItemClassRadio4 = commandClass.SelectItemClassNameFromID(ListItemClassID[5 * ItemClassPage - 2]);
                ItemClassRadio5 = commandClass.SelectItemClassNameFromID(ListItemClassID[5 * ItemClassPage - 1]);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ItemClassPreviousPageVis = Visibility.Hidden; ItemClassNextPageVis = Visibility.Hidden;
                if (ListItemClassID.Count % 5 == 1)
                {
                    ItemClassRadio2 = "";
                    RadioBtnVis2 = Visibility.Hidden;
                    ItemClassRadio3 = "";
                    RadioBtnVis3 = Visibility.Hidden;
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 2)
                {
                    ItemClassRadio3 = "";
                    RadioBtnVis3 = Visibility.Hidden;
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 3)
                {
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 4)
                {
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
            };
            ItemPreviousPageVis = Visibility.Hidden;
            ItemNextPageVis = Visibility.Hidden;
            ItemButtons.Clear();
            RadioButtonChecked1 = false;
            RadioButtonChecked2 = false;
            RadioButtonChecked3 = false;
            RadioButtonChecked4 = false;
            RadioButtonChecked5 = false;
        }

        public void ItemClassNextPage()
        {
            try
            {
                RadioBtnVis1 = Visibility.Visible;
                RadioBtnVis2 = Visibility.Visible;
                RadioBtnVis3 = Visibility.Visible;
                RadioBtnVis4 = Visibility.Visible;
                RadioBtnVis5 = Visibility.Visible;
                ItemClassPreviousPageVis = Visibility.Visible; ItemClassNextPageVis = Visibility.Visible;
                ItemClassRadio1 = commandClass.SelectItemClassNameFromID(ListItemClassID[0 + 5 * ItemClassPage]);
                ItemClassRadio2 = commandClass.SelectItemClassNameFromID(ListItemClassID[1 + 5 * ItemClassPage]);
                ItemClassRadio3 = commandClass.SelectItemClassNameFromID(ListItemClassID[2 + 5 * ItemClassPage]);
                ItemClassRadio4 = commandClass.SelectItemClassNameFromID(ListItemClassID[3 + 5 * ItemClassPage]);
                ItemClassRadio5 = commandClass.SelectItemClassNameFromID(ListItemClassID[4 + 5 * ItemClassPage]);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ItemClassPreviousPageVis = Visibility.Visible;
                ItemClassNextPageVis = Visibility.Hidden;
                if (ListItemClassID.Count % 5 == 1)
                {
                    ItemClassRadio2 = "";
                    RadioBtnVis2 = Visibility.Hidden;
                    ItemClassRadio3 = "";
                    RadioBtnVis3 = Visibility.Hidden;
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 2)
                {
                    ItemClassRadio3 = "";
                    RadioBtnVis3 = Visibility.Hidden;
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 3)
                {
                    ItemClassRadio4 = "";
                    RadioBtnVis4 = Visibility.Hidden;
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
                if (ListItemClassID.Count % 5 == 4)
                {
                    ItemClassRadio5 = "";
                    RadioBtnVis5 = Visibility.Hidden;
                }
            };
            if (ItemClassPage <= (ListItemClassID.Count / 5)) ItemClassPage++;
            else ItemClassPage = ItemClassPage;
            ItemPreviousPageVis = Visibility.Hidden;
            ItemNextPageVis = Visibility.Hidden;
            ItemButtons.Clear();
            RadioButtonChecked1 = false;
            RadioButtonChecked2 = false;
            RadioButtonChecked3 = false;
            RadioButtonChecked4 = false;
            RadioButtonChecked5 = false;
        }
        public void ItemPreviousPage()
        {
            if (ShowItemNameID.Count > 16 && ItemPage > 0)
            {

                List<int> ShowItemNameIDManyPage = new List<int>();
                ItemButtons.Clear();
                if (ItemPage > 1) ItemPage--;
                else ItemPage = 1;
                double MaxPage = Math.Ceiling(Convert.ToDouble(ShowItemNameID.Count) / 16);
                int FillItemCount = ShowItemNameID.Count % 16;
                // 公式: i = 總ID個數 - ((總頁數 - 當前頁數) * 可塞項目個數) - 多出來的(%)
                for (int i = ShowItemNameID.Count - ((Convert.ToInt32(MaxPage) - ItemPage) * 16) - FillItemCount; i < ShowItemNameID.Count; i++) ShowItemNameIDManyPage.Add(ShowItemNameID[i]);
                for (int i = ShowItemNameIDManyPage.Count; i < 16; i++) ShowItemNameIDManyPage.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[15]),
                });
                JudgeVisFromPage();
            }
        }
        public void ItemNextPage()
        {
            if (ShowItemNameID.Count > 16 && ItemPage > 0)
            {
                List<int> ShowItemNameIDManyPage = new List<int>();
                ItemButtons.Clear();
                for (int i = ItemPage * 16; i < ShowItemNameID.Count; i++) ShowItemNameIDManyPage.Add(ShowItemNameID[i]);
                for (int i = ShowItemNameIDManyPage.Count; i < 16; i++) ShowItemNameIDManyPage.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameIDManyPage[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameIDManyPage[15]),
                });
                if (ItemPage <= (ShowItemNameID.Count / 16)) ItemPage++;
                else ItemPage = ItemPage;
                JudgeVisFromPage();
            }
        }
        #endregion

        #region ItemClass methods

        public void ShowItemClass1()
        {
            ShowItemNameID = commandClass.SelectItemIDFromItemClassIDAndEnableAndAlive(commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio1));
            ItemButtons.Clear();
            try
            {
                for (int i = ShowItemNameID.Count; i < 16; i++) ShowItemNameID.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameID[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameID[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameID[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameID[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameID[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameID[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameID[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameID[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameID[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameID[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameID[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameID[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameID[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameID[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameID[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameID[15]),
                });
                ItemClassSelectNow = commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio1);
                ItemPage = 1;
                JudgeVisFromPage();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Argument error. Detail: " + ex.ToString());
            }

        }
        public void ShowItemClass2()
        {

            ShowItemNameID = commandClass.SelectItemIDFromItemClassIDAndEnableAndAlive(commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio2));
            ItemButtons.Clear();
            try
            {
                for (int i = ShowItemNameID.Count; i < 16; i++) ShowItemNameID.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameID[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameID[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameID[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameID[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameID[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameID[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameID[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameID[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameID[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameID[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameID[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameID[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameID[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameID[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameID[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameID[15]),
                });
                ItemClassSelectNow = commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio2);
                ItemPage = 1;
                JudgeVisFromPage();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Argument error. Detail: " + ex.ToString());
            }
        }
        public void ShowItemClass3()
        {

            ShowItemNameID = commandClass.SelectItemIDFromItemClassIDAndEnableAndAlive(commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio3));
            ItemButtons.Clear();
            try
            {
                for (int i = ShowItemNameID.Count; i < 16; i++) ShowItemNameID.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameID[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameID[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameID[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameID[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameID[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameID[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameID[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameID[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameID[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameID[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameID[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameID[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameID[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameID[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameID[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameID[15]),
                });
                ItemClassSelectNow = commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio3);
                ItemPage = 1;
                JudgeVisFromPage();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Argument error. Detail: " + ex.ToString());
            }
        }
        public void ShowItemClass4()
        {

            ShowItemNameID = commandClass.SelectItemIDFromItemClassIDAndEnableAndAlive(commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio4));
            ItemButtons.Clear();
            try
            {
                for (int i = ShowItemNameID.Count; i < 16; i++) ShowItemNameID.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameID[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameID[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameID[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameID[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameID[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameID[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameID[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameID[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameID[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameID[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameID[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameID[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameID[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameID[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameID[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameID[15]),
                });
                ItemClassSelectNow = commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio4);
                ItemPage = 1;
                JudgeVisFromPage();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Argument error. Detail: " + ex.ToString());
            }
        }
        public void ShowItemClass5()
        {

            ShowItemNameID = commandClass.SelectItemIDFromItemClassIDAndEnableAndAlive(commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio5));
            ItemButtons.Clear();
            try
            {
                for (int i = ShowItemNameID.Count; i < 16; i++) ShowItemNameID.Add(0);
                ItemButtons.Add(new ItemButtonsModel()
                {
                    Item1ClickCommand = Item1ClickCommand,
                    Item2ClickCommand = Item2ClickCommand,
                    Item3ClickCommand = Item3ClickCommand,
                    Item4ClickCommand = Item4ClickCommand,
                    Item5ClickCommand = Item5ClickCommand,
                    Item6ClickCommand = Item6ClickCommand,
                    Item7ClickCommand = Item7ClickCommand,
                    Item8ClickCommand = Item8ClickCommand,
                    Item9ClickCommand = Item9ClickCommand,
                    Item10ClickCommand = Item10ClickCommand,
                    Item11ClickCommand = Item11ClickCommand,
                    Item12ClickCommand = Item12ClickCommand,
                    Item13ClickCommand = Item13ClickCommand,
                    Item14ClickCommand = Item14ClickCommand,
                    Item15ClickCommand = Item15ClickCommand,
                    Item16ClickCommand = Item16ClickCommand,
                    Item1 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[0]),
                    Item2 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[1]),
                    Item3 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[2]),
                    Item4 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[3]),
                    Item5 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[4]),
                    Item6 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[5]),
                    Item7 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[6]),
                    Item8 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[7]),
                    Item9 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[8]),
                    Item10 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[9]),
                    Item11 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[10]),
                    Item12 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[11]),
                    Item13 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[12]),
                    Item14 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[13]),
                    Item15 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[14]),
                    Item16 = commandClass.SelectItemNameFromIDAndAlive(ShowItemNameID[15]),
                    Item1Vis = ChangeVisFromShowItemNameID(ShowItemNameID[0]),
                    Item2Vis = ChangeVisFromShowItemNameID(ShowItemNameID[1]),
                    Item3Vis = ChangeVisFromShowItemNameID(ShowItemNameID[2]),
                    Item4Vis = ChangeVisFromShowItemNameID(ShowItemNameID[3]),
                    Item5Vis = ChangeVisFromShowItemNameID(ShowItemNameID[4]),
                    Item6Vis = ChangeVisFromShowItemNameID(ShowItemNameID[5]),
                    Item7Vis = ChangeVisFromShowItemNameID(ShowItemNameID[6]),
                    Item8Vis = ChangeVisFromShowItemNameID(ShowItemNameID[7]),
                    Item9Vis = ChangeVisFromShowItemNameID(ShowItemNameID[8]),
                    Item10Vis = ChangeVisFromShowItemNameID(ShowItemNameID[9]),
                    Item11Vis = ChangeVisFromShowItemNameID(ShowItemNameID[10]),
                    Item12Vis = ChangeVisFromShowItemNameID(ShowItemNameID[11]),
                    Item13Vis = ChangeVisFromShowItemNameID(ShowItemNameID[12]),
                    Item14Vis = ChangeVisFromShowItemNameID(ShowItemNameID[13]),
                    Item15Vis = ChangeVisFromShowItemNameID(ShowItemNameID[14]),
                    Item16Vis = ChangeVisFromShowItemNameID(ShowItemNameID[15]),
                });
                ItemClassSelectNow = commandClass.SelectItemClassIDFromItemClassName(ItemClassRadio5);
                ItemPage = 1;
                JudgeVisFromPage();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Argument error. Detail: " + ex.ToString());
            }
        }
        #endregion
        #region ItemClick methods

        public void Item1Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item1) || ItemButtons[0].Item1 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item1);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item1,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item2Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item2) || ItemButtons[0].Item2 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item2);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item2,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item3Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item3) || ItemButtons[0].Item3 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item3);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item3,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item4Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item4) || ItemButtons[0].Item4 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item4);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item4,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item5Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item5) || ItemButtons[0].Item5 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item5);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item5,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item6Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item6) || ItemButtons[0].Item6 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item6);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item6,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item7Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item7) || ItemButtons[0].Item7 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item7);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item7,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item8Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item8) || ItemButtons[0].Item8 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item8);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item8,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item9Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item9) || ItemButtons[0].Item9 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item9);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item9,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item10Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item10) || ItemButtons[0].Item10 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item10);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item10,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item11Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item11) || ItemButtons[0].Item11 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item11);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item11,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item12Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item12) || ItemButtons[0].Item12 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item12);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item12,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item13Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item13) || ItemButtons[0].Item13 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item13);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item13,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item14Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item14) || ItemButtons[0].Item14 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item14);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item14,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item15Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item15) || ItemButtons[0].Item15 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item15);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item15,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        public void Item16Click()
        {
            if (OrderListData.Any(x => x.OrderName == ItemButtons[0].Item16) || ItemButtons[0].Item16 == null)
                return;
            else
            {
                int ItemID = commandClass.SelectItemIDFromItemName(ItemButtons[0].Item16);
                OrderListData.Add(new OrderListDataModel()
                {
                    OrderName = ItemButtons[0].Item16,
                    OrderCash = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID),
                    OrderAmount = 1,
                    OrderSubTotal = commandClass.SelectItemCashFromItemClassandItemID(ItemClassSelectNow, ItemID)
                });
                OrderListSelectValue = OrderListData.Last();
            }
        }
        #endregion
        #region SearchMember methods
        public void SearchMemberID()
        {
            HttpResponseMessage response;
            long IsNotSearchID = 0;
            string SearchMemberName = null;
            response = APIConnect.ResponseGet($@"MemberData/MemberPhone/{TextBoxMemberPhone}");
            if (response.IsSuccessStatusCode)
                IsNotSearchID = response.Content.ReadAsAsync<long>().Result;
            response = APIConnect.ResponseGet($@"MemberData/MemberName/{Convert.ToInt32(IsNotSearchID)}");
            if (response.IsSuccessStatusCode)
                SearchMemberName = response.Content.ReadAsAsync<string>().Result;
            if (SearchMemberName == null)
            {
                IsSearchedYes = Visibility.Hidden;
                IsSearchedNo = Visibility.Visible;
            }
            else
            {
                TextMemberName = SearchMemberName;
                IsSearchedYes = Visibility.Visible;
                IsSearchedNo = Visibility.Hidden;
            }
        }
        public void SearchMemberYes()
        {
            HttpResponseMessage response;
            long IsSearchID = 0;
            response = APIConnect.ResponseGet($@"MemberData/MemberPhone/{TextBoxMemberPhone}");
            if (response.IsSuccessStatusCode)
                IsSearchID = response.Content.ReadAsAsync<long>().Result;
            response = APIConnect.ResponseGet($@"MemberData/{Convert.ToInt32(IsSearchID)}");
            if (response.IsSuccessStatusCode)
                ClientMemberData = response.Content.ReadAsAsync<ClientMemberDataModel>().Result;
            NowMemberName = ClientMemberData.Name;
            NowMemberGender = ClientMemberData.Gender;
            NowMemberRank = ClientMemberData.MemberRank;
            ShowMemberVis = Visibility.Visible;
            SearchMemberCancel();
        }
        public void SearchMemberNo()
        {
            ClientMemberData = new ClientMemberDataModel();
            TextMemberName = "";
            TextBoxMemberPhone = "";
            NowMemberName = "";
            NowMemberGender = "";
            NowMemberRank = "";
            ShowMemberVis = Visibility.Hidden;
            IsSearchedYes = Visibility.Hidden;
            IsSearchedNo = Visibility.Hidden;
        }
        public void SearchMemberCancel()
        {
            TextMemberName = "";
            TextBoxMemberPhone = "";
            IsSearchedYes = Visibility.Hidden;
            IsSearchedNo = Visibility.Hidden;
            SearchMemberVis = Visibility.Hidden;
            ItemPreviousPageVis = Visibility.Collapsed;
            ItemNextPageVis = Visibility.Collapsed;
        }
        #endregion
        public void ShowOrder()
        {

            OrderListData.Clear();
            ItemClassPreviousPageVis = Visibility.Hidden;
            RadioButtonChecked1 = false;
            RadioButtonChecked2 = false;
            RadioButtonChecked3 = false;
            RadioButtonChecked4 = false;
            RadioButtonChecked5 = false;
            ListItemClassID = commandClass.SelectItemClassIDFromEnableAndAlive();
            if (ListItemClassID.Count <= 5) { for (int i = ListItemClassID.Count; i < 5; i++) ListItemClassID.Add(0); Order._ItemClassNextPageVis = Visibility.Hidden; }
            else Order._ItemClassNextPageVis = Visibility.Visible;
            ItemClassRadio1 = commandClass.SelectItemClassNameFromID(ListItemClassID[0]);
            ItemClassRadio2 = commandClass.SelectItemClassNameFromID(ListItemClassID[1]);
            ItemClassRadio3 = commandClass.SelectItemClassNameFromID(ListItemClassID[2]);
            ItemClassRadio4 = commandClass.SelectItemClassNameFromID(ListItemClassID[3]);
            ItemClassRadio5 = commandClass.SelectItemClassNameFromID(ListItemClassID[4]);
            if (ItemClassRadio1 == null) RadioBtnVis1 = Visibility.Hidden;
            else RadioBtnVis1 = Visibility.Visible;
            if (RadioClass._ItemClassRadio2 == null) RadioBtnVis2 = Visibility.Hidden;
            else RadioBtnVis2 = Visibility.Visible;
            if (RadioClass._ItemClassRadio3 == null) RadioBtnVis3 = Visibility.Hidden;
            else RadioBtnVis3 = Visibility.Visible;
            if (RadioClass._ItemClassRadio4 == null) RadioBtnVis4 = Visibility.Hidden;
            else RadioBtnVis4 = Visibility.Visible;
            if (RadioClass._ItemClassRadio5 == null) RadioBtnVis5 = Visibility.Hidden;
            else RadioBtnVis5 = Visibility.Visible;
            if (ListItemClassID.Count <= 5) { for (int i = ListItemClassID.Count; i < 5; i++) ListItemClassID.Add(0); ItemClassNextPageVis = Visibility.Hidden; }
            else ItemClassNextPageVis = Visibility.Visible;
            ItemButtons.Clear();
            ItemPreviousPageVis = Visibility.Collapsed;
            ItemNextPageVis = Visibility.Collapsed;
            ItemClassPage = 1;
            SearchMemberNo();
            SearchMemberCancel();
            OrderVis = Visibility.Visible;
        }
        public void CollapsedScreen()
        {
            OrderVis = Visibility.Collapsed;
        }
        public void IncreaseAmount()
        {
            if (OrderListSelectValue != null)
            {
                if (OrderListSelectValue.OrderAmount < 100 && OrderListSelectValue.OrderSubTotal < int.MaxValue)
                {
                    OrderListSelectValue.OrderAmount++;
                    OrderListSelectValue.OrderSubTotal += OrderListSelectValue.OrderCash;
                    CollectionViewSource.GetDefaultView(OrderListData).Refresh();
                }
                else
                {
                    OrderListSelectValue.OrderAmount = OrderListSelectValue.OrderAmount;
                    OrderListSelectValue.OrderSubTotal = OrderListSelectValue.OrderSubTotal;
                }
            }
            else
            {
                MessageBox.Show("請先點選欲增加之商品再點選商品增加按鍵(＋)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        public void DecreaseAmount()
        {
            if (OrderListSelectValue != null)
            {
                if (OrderListSelectValue.OrderAmount > 1)
                {
                    OrderListSelectValue.OrderAmount--;
                    OrderListSelectValue.OrderSubTotal -= OrderListSelectValue.OrderCash;
                    CollectionViewSource.GetDefaultView(OrderListData).Refresh();
                }
                else
                {
                    OrderListSelectValue.OrderAmount = OrderListSelectValue.OrderAmount;
                    OrderListSelectValue.OrderSubTotal = OrderListSelectValue.OrderSubTotal;
                }
            }
            else
            {
                MessageBox.Show("請先點選欲減少之商品再點選商品減少按鍵(－)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void DeleteItem()
        {
            if (OrderListSelectValue != null)
            {
                if (OrderListData.Contains(OrderListSelectValue))
                {
                    int index = OrderListData.IndexOf(OrderListSelectValue);
                    OrderListData.Remove(OrderListData.Where(x => x.OrderName == OrderListSelectValue.OrderName).Single());
                    CollectionViewSource.GetDefaultView(OrderListData).Refresh();
                    if (OrderListData.Count > 0)
                    {
                        if (index == 0)
                            OrderListSelectValue = OrderListData[OrderListData.Count - 1];
                        else
                            OrderListSelectValue = OrderListData[index - 1];
                    }
                    else
                        OrderListSelectValue = null;

                }
                else
                {
                    MessageBox.Show("搜尋不到欲刪除之商品", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("請先點選欲刪除之商品再點選商品刪除按鍵(Del)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void LoadMember()
        {
            if (ListItemClassID.Count <= 5) { for (int i = ListItemClassID.Count; i < 5; i++) ListItemClassID.Add(0); Order._ItemClassNextPageVis = Visibility.Hidden; }
            else Order._ItemClassNextPageVis = Visibility.Visible;
            RadioClass = new RadioClassModel()
            {
                _ItemClassRadio1 = commandClass.SelectItemClassNameFromID(ListItemClassID[0]),
                _ItemClassRadio2 = commandClass.SelectItemClassNameFromID(ListItemClassID[1]),
                _ItemClassRadio3 = commandClass.SelectItemClassNameFromID(ListItemClassID[2]),
                _ItemClassRadio4 = commandClass.SelectItemClassNameFromID(ListItemClassID[3]),
                _ItemClassRadio5 = commandClass.SelectItemClassNameFromID(ListItemClassID[4]),
            };
            if (RadioClass._ItemClassRadio1 == null) Order._RadioBtnVis1 = Visibility.Hidden;
            else Order._RadioBtnVis1 = Visibility.Visible;
            if (RadioClass._ItemClassRadio2 == null) Order._RadioBtnVis2 = Visibility.Hidden;
            else Order._RadioBtnVis2 = Visibility.Visible;
            if (RadioClass._ItemClassRadio3 == null) Order._RadioBtnVis3 = Visibility.Hidden;
            else Order._RadioBtnVis3 = Visibility.Visible;
            if (RadioClass._ItemClassRadio4 == null) Order._RadioBtnVis4 = Visibility.Hidden;
            else Order._RadioBtnVis4 = Visibility.Visible;
            if (RadioClass._ItemClassRadio5 == null) Order._RadioBtnVis5 = Visibility.Hidden;
            else Order._RadioBtnVis5 = Visibility.Visible;
            ItemButtons.Clear();
            ItemClassPage = 1;
            SearchMemberVis = Visibility.Visible;
        }
        public void Bill()
        {
            DateTime dateTime = DateTime.Now;
            string OrderOutput;
            string StrInSQLOrderOutputDate = commandClass.SelectCheckOutDataOrderNumberFromMax().Substring(0, commandClass.SelectCheckOutDataOrderNumberFromMax().Length - 4);
            string StrInSQLOrderOutputSerialNumber = commandClass.SelectCheckOutDataOrderNumberFromMax().Substring(StrInSQLOrderOutputDate.Length, 4);
            int OrderTotal = 0, OrderItemNumber = 1;
            if (DateTime.ParseExact(StrInSQLOrderOutputDate, "yyyyMMdd", CultureInfo.InvariantCulture) == dateTime.Date)
                if (Convert.ToInt32(StrInSQLOrderOutputSerialNumber) >= SerialNumber)
                    SerialNumber = Convert.ToInt32(StrInSQLOrderOutputSerialNumber) + 1;
            OrderOutput = dateTime.ToString("yyyyMMdd") + SerialNumber.ToString(format: "0000");

            if (OrderListData.Count > 0)
            {
                for (int i = 0; i < OrderListData.Count; i++) { OrderTotal += OrderListData[i].OrderSubTotal; }
                if (MessageBox.Show($@"本次消費總金額為{OrderTotal}{Environment.NewLine}確定要進行結帳嗎?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // IF have Member ID
                    if (ClientMemberData.ID > 0)
                    {
                        List<InsertMemberConsumeData> insertMemberConsumeData = new List<InsertMemberConsumeData>();
                        insertMemberConsumeData.Add(new InsertMemberConsumeData()
                        {
                            MemberID = ClientMemberData.ID,
                            OrderNumber = OrderOutput,
                            Total = OrderTotal,
                        });
                        string StringJson = JsonConvert.SerializeObject(insertMemberConsumeData).Trim('[').Trim(']');
                        APIConnect.ResponsePost("MemberData/InsertMemberConsumeData", StringJson);

                        // Update Member Data(Consume)
                        ClientMemberData.ConsTotal += OrderTotal;
                        APIConnect.ResponsePut($@"MemberData/UpdateMemberConsTotal/{ClientMemberData.ID}/{ClientMemberData.ConsTotal}", "");
                        JudgeMemberRank(ClientMemberData.ID);
                    }
                    // Insert Order List DB
                    commandClass.InsertOrderListToDB(OrderOutput, dateTime.ToString("yyyy/MM/dd"), dateTime.ToString("HH:mm:ss"), OrderTotal, commandClass.SelectStaffIDFromIsLogined());
                    for (int i = 0; i < OrderListData.Count; i++)
                    {
                        commandClass.InsertOrderDataToDB(OrderOutput,
                                                         OrderItemNumber,
                                                         commandClass.SelectItemIDFromItemName(OrderListData[i].OrderName),
                                                         OrderListData[i].OrderAmount,
                                                         OrderListData[i].OrderCash,
                                                         OrderListData[i].OrderSubTotal,
                                                         dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
                        OrderItemNumber++;
                    }
                    SerialNumber++;
                    OrderListData.Clear();
                    SearchMemberNo();
                    SearchMemberCancel();
                    if (OrderButtonEnable == true) OrderButtonEnable = false;
                    if (BillButtonEnable == true) BillButtonEnable = false;
                    MessageBox.Show("結帳已完成\n下一位請就位", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    return;
            }
            else
            {
                MessageBox.Show("欲購項目為0無法結帳", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        public bool CanExecute()
        {
            return true;
        }

        #endregion

        #region Private methods
        private void JudgeMemberRank(int ID)
        {
            string MemberRank;
            long ConsTotal = 0;
            HttpResponseMessage response = APIConnect.ResponseGet($@"MemberData/ConsTotal/{ID}");
            if (response.IsSuccessStatusCode)
                ConsTotal = response.Content.ReadAsAsync<long>().Result;
            if (ConsTotal >= 5000)
                MemberRank = "Gold";
            else if (ConsTotal >= 2000 && ConsTotal < 5000)
                MemberRank = "Silver";
            else if (ConsTotal >= 500 && ConsTotal < 2000)
                MemberRank = "Copper";
            else
                MemberRank = "Normal";
            APIConnect.ResponsePut($@"MemberData/UpdateMemberRank/{ID}/{MemberRank}", "");

        }
        private void JudgeVisFromPage()
        {
            if (ItemPage >= 1 && ShowItemNameID.Count <= 16) { ItemPreviousPageVis = Visibility.Hidden; ItemNextPageVis = Visibility.Hidden; }
            else if (ItemPage > (ShowItemNameID.Count / 16)) { ItemPreviousPageVis = Visibility.Visible; ItemNextPageVis = Visibility.Hidden; }
            else if (ItemPage < (ShowItemNameID.Count / 16) && ShowItemNameID.Count >= 16) { ItemPreviousPageVis = Visibility.Hidden; ItemNextPageVis = Visibility.Visible; }
            else if (ItemPage > 1 && ItemPage <= (ShowItemNameID.Count / 16)) { ItemPreviousPageVis = Visibility.Visible; ItemNextPageVis = Visibility.Visible; }
        }
        private Visibility ChangeVisFromShowItemNameID(int ID)
        {
            if (ID == 0) return Visibility.Hidden;
            else return Visibility.Visible;
        }

        #endregion
    }
}
