using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using HW_POS_Server.Common;
using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models.ClientModels;
using HW_POS_Server.Views.ClientViews;

namespace HW_POS_Server.ViewModels.ClientViewModels
{

    public class CheckRecordViewModel : ViewModelBase
    {
        #region Fields
        private CheckRecordModel CheckOut = new CheckRecordModel();
        private SQLCommandClass commandClass = new SQLCommandClass();
        private WebAPIConnect APIConnect = new WebAPIConnect();
        private DispatcherTimer CountTimer;
        #endregion
        public CheckRecordViewModel()
        {
            #region Constrcutor
            CheckOut = new CheckRecordModel()
            {
                _DateKeyword = DateTime.Now,
                _OrderNumberKeyword = "",
                _SelectValueTotal = 0,
                _SelectValueAmountTotal = 0,
                _SelectValueMemberName = "Null",
                _CheckRecordVis = Visibility.Collapsed,
            };
            APIConnect.connectOpen();
            CheckOutData.Clear();
            List<string> ListCheckOutNumber = commandClass.SelectChekcOutListOrderNumber();
            for (int i = 0; i < ListCheckOutNumber.Count; i++)
            {
                TimeSpan CheckOutTime = commandClass.SelectCheckOutListOrderTimeFromOrderNumber(ListCheckOutNumber[i]);
                DateTime CheckOutDateTime = commandClass.SelectCheckOutListOrderDateFromOrderNumber(ListCheckOutNumber[i]).Add(CheckOutTime);
                CheckOutData.Add(new CheckOutDataModel()
                {
                    CheckNumber = ListCheckOutNumber[i],
                    CheckTime = CheckOutDateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                    CheckTotal = commandClass.SelectCheckOutListOrderTotalFromOrderNumber(ListCheckOutNumber[i]),
                });
            }
            CountTimer = new DispatcherTimer(DispatcherPriority.Render);
            CountTimer.Interval = TimeSpan.FromSeconds(1);
            CountTimer.Tick += (sender, e) =>
            {
                Refresh();
            };

                #endregion
            }

        #region Properties

        #region Visibility Properties
        public Visibility CheckRecordVis
        {
            get { return CheckOut._CheckRecordVis; }
            set
            {
                CheckOut._CheckRecordVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region ObservableCollection Properties
        ObservableCollection<CheckOutDataModel> _CheckOutData = new ObservableCollection<CheckOutDataModel>();
        public ObservableCollection<CheckOutDataModel> CheckOutData
        {
            get { return _CheckOutData; }
            set
            {
                _CheckOutData = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<CheckItemDataModel> _CheckItemData = new ObservableCollection<CheckItemDataModel>();
        public ObservableCollection<CheckItemDataModel> CheckItemData
        {
            get { return _CheckItemData; }
            set
            {
                _CheckItemData = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region CheckOut DataGrid Properties
        public CheckOutDataModel CheckOutSelectValue
        {
            get { return CheckOut._CheckOutSelectValue; }
            set
            {
                CheckOut._CheckOutSelectValue = value;
                PopCheckOutData();
            }
        }
        public ICollectionView FilterCheckOutData
        {
            get { return CheckOut._FilterCheckOutData; }
            set
            {
                CheckOut._FilterCheckOutData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region CheckItem DataGrid Properties
        public CheckItemDataModel CheckItemSelectValue
        {
            get { return CheckOut._CheckItemSelectValue; }
            set
            {
                CheckOut._CheckItemSelectValue = value;
                PopCheckItemData();
            }
        }

        #endregion
        #region DateTime Properties
        public DateTime DateKeyword
        {
            get { return CheckOut._DateKeyword; }
            set
            {
                CheckOut._DateKeyword = value;
                OnPropertyChanged();
                FilterDate();
                OrderNumberKeyword = "";
            }
        }
        #endregion
        #region Button Properties
        public ICommand CheckRecordCommand
        {
            get { return new RelayCommand(ShowCheckRecord, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedScreen, CanExecute); }
        }
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(Refresh, CanExecute); }
        }
        #endregion
        public string OrderNumberKeyword
        {
            get { return CheckOut._OrderNumberKeyword; }
            set
            {
                CheckOut._OrderNumberKeyword = value;
                OnPropertyChanged();
                FilterOrderNumber();
            }
        }
        public int SelectValueTotal
        {
            get { return CheckOut._SelectValueTotal; }
            set
            {
                CheckOut._SelectValueTotal = value;
                OnPropertyChanged();
            }
        }
        public int SelectValueAmountTotal
        {
            get { return CheckOut._SelectValueAmountTotal; }
            set
            {
                CheckOut._SelectValueAmountTotal = value;
                OnPropertyChanged();
            }
        }
        public string SelectValueMemberName
        {
            get { return CheckOut._SelectValueMemberName; }
            set
            {
                CheckOut._SelectValueMemberName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Public methods
        public void ShowCheckRecord()
        {
            Refresh();
            CheckRecordVis = Visibility.Visible;
        }
        public void CollapsedScreen()
        {
            CheckRecordVis = Visibility.Collapsed;
        }
        public void PopCheckOutData()
        {
            if (CheckOutSelectValue != null)
            {
                int MemberID = 0;
                HttpResponseMessage response;
                Console.WriteLine("CheckOut");
                // List<int> ListItemID = commandClass.SelectCheckOutItemIDFromOrderNumber(CheckOutSelectValue.CheckNumber);
                List<int> ListOrderItemNumber = commandClass.SelectCheckOutDataOrderItemNumberFromOrderNumber(CheckOutSelectValue.CheckNumber);
                CheckItemData.Clear();
                SelectValueAmountTotal = 0;

                // Select Member Name
                response = APIConnect.ResponseGet($@"MemberData/MemberConsumeData/{CheckOutSelectValue.CheckNumber}");
                if (response.IsSuccessStatusCode)
                    MemberID = response.Content.ReadAsAsync<int>().Result;
                if (MemberID > 0)
                {
                    response = APIConnect.ResponseGet($@"MemberData/MemberName/{MemberID}");
                    if (response.IsSuccessStatusCode)
                        SelectValueMemberName = response.Content.ReadAsStringAsync().Result.Trim('"');
                }
                else
                    SelectValueMemberName = "Null";

                // Select OrderNumber's Item
                for (int i = 0; i < ListOrderItemNumber.Count; i++)
                {
                    CheckItemData.Add(new CheckItemDataModel()
                    {
                        ItemName = commandClass.SelectItemNameFromID(commandClass.SelectCheckOutDataItemIDFromOrderNumberAndOrderItemNumber(CheckOutSelectValue.CheckNumber, ListOrderItemNumber[i])),
                        ItemAmount = commandClass.SelectCheckOutDataItemAmountFromOrderNumberAndOrderItemNumber(CheckOutSelectValue.CheckNumber, ListOrderItemNumber[i]),
                        ItemCash = commandClass.SelectCheckOutDataItemCashFromOrderNumberAndOrderItemNumber(CheckOutSelectValue.CheckNumber, ListOrderItemNumber[i]),
                        ItemSubTotal = commandClass.SelectCheckOutDataItemSubTotalFromOrderNumberAndOrderItemNumber(CheckOutSelectValue.CheckNumber, ListOrderItemNumber[i]),
                    });
                    SelectValueAmountTotal += CheckItemData[i].ItemAmount;
                }
                SelectValueTotal = CheckOutSelectValue.CheckTotal;
            }
            else
                CheckItemData.Clear();
        }
        public void PopCheckItemData()
        {
            Console.WriteLine("CheckItem");
        }
        public void FilterDate()
        {
            FilterCheckOutData = CollectionViewSource.GetDefaultView
                                    (CheckOutData.Where(x =>
                                                        Convert.ToDateTime(x.CheckTime) >= DateKeyword &&
                                                        Convert.ToDateTime(x.CheckTime) < DateKeyword.AddDays(1)
                                                        ));
        }
        public void FilterOrderNumber()
        {
            if (OrderNumberKeyword == "")
                FilterDate();
            else
                FilterCheckOutData = CollectionViewSource.GetDefaultView(CheckOutData.Where(x =>
                                                                         Convert.ToDateTime(x.CheckTime) >= DateKeyword &&
                                                                         Convert.ToDateTime(x.CheckTime) < DateKeyword.AddDays(1) &&
                                                                         x.CheckNumber.Contains(OrderNumberKeyword)
                                                                         ));
        }
        public void Refresh()
        {
            List<string> ListCheckOutNumber = commandClass.SelectChekcOutListOrderNumber();
            CheckOutData.Clear();
            for (int i = 0; i < ListCheckOutNumber.Count; i++)
            {
                TimeSpan CheckOutTime = commandClass.SelectCheckOutListOrderTimeFromOrderNumber(ListCheckOutNumber[i]);
                DateTime CheckOutDateTime = commandClass.SelectCheckOutListOrderDateFromOrderNumber(ListCheckOutNumber[i]).Add(CheckOutTime);
                CheckOutData.Add(new CheckOutDataModel()
                {
                    CheckNumber = ListCheckOutNumber[i],
                    CheckTime = CheckOutDateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                    CheckTotal = commandClass.SelectCheckOutListOrderTotalFromOrderNumber(ListCheckOutNumber[i]),
                });
            }
            DateKeyword = DateTime.Now;
            OrderNumberKeyword = "";
            FilterOrderNumber();
        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion
    }

}
