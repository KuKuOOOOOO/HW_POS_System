using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using System.Runtime.InteropServices.ComTypes;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Xml.Linq;
using HW_POS_Server.Common;

namespace HW_POS_Server.ViewModels.ClientViewModels
{
    public class MemberViewModel : ViewModelBase
    {
        #region Fields
        private MemberModel Member { get; set; }
        ObservableCollection<ClientMemberDataModel> ClientMemberData;
        WebAPIConnect APIConnect = new WebAPIConnect();

        #endregion
        public MemberViewModel()
        {
            #region Constructor
            Member = new MemberModel()
            {

                _TextBoxName = "",
                _TextBoxMemberRank = "",
                _TextBoxPhoneNumber = "",
                _TextBoxAddress = "",
                _TextBoxPS = "",
                _TextBoxContTotal = 0,
                _DataGridEnable = true,
                _MemberComboBoxEnable = false,
                _MemberTextBoxEnable = true,
                _MemberAddSetBtnEnable = true,
                _MemberEditSetBtnEnable = false,
                _MemberDelSetBtnEnable = false,
                _SandCBtnEnable = false,
                _MemberVis = Visibility.Collapsed,
                _ConsTotalVis = Visibility.Hidden,
            };
            GenderData.Add(new GenderDataModel() { Gender = "" });
            GenderData.Add(new GenderDataModel() { Gender = "Male" });
            GenderData.Add(new GenderDataModel() { Gender = "Female" });
            APIConnect.connectOpen();
            HttpResponseMessage response = APIConnect.ResponseGet("MemberData");
            if (response.IsSuccessStatusCode)
            {
                ClientMemberData = response.Content.ReadAsAsync<ObservableCollection<ClientMemberDataModel>>().Result;
                for (int i = 0; i < ClientMemberData.Count; i++)
                    MemberData.Add(new MemberDataModel()
                    {
                        ID = ClientMemberData[i].ID,
                        Name = ClientMemberData[i].Name,
                        Gender = ClientMemberData[i].Gender.Trim(),
                        MemberRank = ClientMemberData[i].MemberRank,
                        PhoneNumber = ClientMemberData[i].PhoneNumber
                    });
            }
            #endregion
        }
        #region Properites
        #region Observablecollection Properties
        public ObservableCollection<MemberDataModel> _MemberData = new ObservableCollection<MemberDataModel>();
        public ObservableCollection<MemberDataModel> MemberData
        {
            get { return _MemberData; }
            set
            {
                _MemberData = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<GenderDataModel> _GenderData = new ObservableCollection<GenderDataModel>();
        public ObservableCollection<GenderDataModel> GenderData
        {
            get { return _GenderData; }
            set
            {
                _GenderData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Member Select Value Properties
        public MemberDataModel MemberSelectValue
        {
            get { return Member._MemberSelectValue; }
            set
            {
                Member._MemberSelectValue = value;
                OnPropertyChanged();
                if(Member._MemberSelectValue != null)
                {
                    MemberEditSetBtnEnable = true;
                    MemberDelSetBtnEnable = true;
                }
                else
                {
                    MemberEditSetBtnEnable = false;
                    MemberDelSetBtnEnable = false;
                }
                PopMemberData();
            }
        }
        #endregion
        #region Gender Select Value Properties
        public GenderDataModel GenderSelectValue
        {
            get { return Member._GenderSelectValue; }
            set
            {
                Member._GenderSelectValue = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Visility Properties
        public Visibility MemberVis
        {
            get { return Member._MemberVis; }
            set
            {
                Member._MemberVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ConsTotalVis
        {
            get { return Member._ConsTotalVis; }
            set
            {
                Member._ConsTotalVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region String Properties
        public string TextBoxName
        {
            get { return Member._TextBoxName; }
            set
            {
                Member._TextBoxName = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxMemberRank
        {
            get { return Member._TextBoxMemberRank; }
            set
            {
                Member._TextBoxMemberRank = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxPhoneNumber
        {
            get { return Member._TextBoxPhoneNumber; }
            set
            {
                Member._TextBoxPhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxAddress
        {
            get { return Member._TextBoxAddress; }
            set
            {
                Member._TextBoxAddress = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxPS
        {
            get { return Member._TextBoxPS; }
            set
            {
                Member._TextBoxPS = value;
                OnPropertyChanged();
            }
        }
        public int TextBoxContTotal
        {
            get { return Member._TextBoxContTotal; }
            set
            {
                Member._TextBoxContTotal = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Bool Properties
        public bool DataGridEnable
        {
            get { return Member._DataGridEnable; }
            set
            {
                Member._DataGridEnable = value;
                OnPropertyChanged();
            }
        }
        public bool MemberComboBoxEnable
        {
            get { return Member._MemberComboBoxEnable; }
            set
            {
                Member._MemberComboBoxEnable = value;
                OnPropertyChanged();
            }
        }
        public bool MemberTextBoxEnable
        {
            get { return Member._MemberTextBoxEnable; }
            set
            {
                Member._MemberTextBoxEnable = value;
                OnPropertyChanged();
            }
        }
        public bool MemberAddSetBtnEnable
        {
            get { return Member._MemberAddSetBtnEnable; }
            set
            {
                Member._MemberAddSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool MemberEditSetBtnEnable
        {
            get { return Member._MemberEditSetBtnEnable; }
            set
            {
                Member._MemberEditSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool MemberDelSetBtnEnable
        {
            get { return Member._MemberDelSetBtnEnable; }
            set
            {
                Member._MemberDelSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool SandCBtnEnable
        {
            get { return Member._SandCBtnEnable; }
            set
            {
                Member._SandCBtnEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Button Properties
        public ICommand MemberCommand
        {
            get { return new RelayCommand(ShowMember, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedMember, CanExecute); }
        }
        public ICommand MemberAddBtn
        {
            get { return new RelayCommand(MemberAdd, CanExecute); }
        }
        public ICommand MemberEditBtn
        {
            get { return new RelayCommand(MemberEdit, CanExecute); }
        }
        public ICommand MemberDelBtn
        {
            get { return new RelayCommand(MemberDel, CanExecute); }
        }
        public ICommand MemberSaveBtn
        {
            get { return new RelayCommand(MemberSave, CanExecute); }
        }
        public ICommand MemberCancelBtn
        {
            get { return new RelayCommand(MemberCancel, CanExecute); }
        }
        #endregion
        #endregion

        #region Public Methods
        private bool AddOrEdit; // Add = True, Edit = False
        public void ShowMember()
        {
            MemberVis = Visibility.Visible;
        }
        public void CollapsedMember()
        {
            MemberVis = Visibility.Collapsed;
        }
        public void PopMemberData()
        {
            if (MemberSelectValue != null)
            {
                TextBoxName = MemberSelectValue.Name;
                if (MemberSelectValue.Gender == "M")
                    GenderSelectValue = GenderData[1];
                else if (MemberSelectValue.Gender == "F")
                    GenderSelectValue = GenderData[2];
                else
                    GenderSelectValue = GenderData[0];
                TextBoxMemberRank = MemberSelectValue.MemberRank;
                TextBoxPhoneNumber = MemberSelectValue.PhoneNumber;
                TextBoxAddress = ClientMemberData.Where(x => x.ID == MemberSelectValue.ID).ToList()[0].Address;
                TextBoxPS = ClientMemberData.Where(x => x.ID == MemberSelectValue.ID).ToList()[0].PS;
            }
        }
        public void MemberAdd()
        {
            TextBoxName = "";
            TextBoxMemberRank = "";
            TextBoxPhoneNumber = "";
            GenderSelectValue = GenderData[0];
            TextBoxAddress = "";
            TextBoxPS = "";
            TextBoxContTotal = 0;
            ConsTotalVis = Visibility.Visible;
            AddOrEdit = true;
            DataGridEnable = false;
            MemberComboBoxEnable = true;
            MemberTextBoxEnable = false;
            MemberAddSetBtnEnable = false;
            MemberEditSetBtnEnable = false;
            MemberDelSetBtnEnable = false;
            SandCBtnEnable = true;
        }
        public void MemberEdit()
        {
            if (MemberSelectValue != null)
            {
                TextBoxName = MemberSelectValue.Name;
                if (MemberSelectValue.Gender == "M")
                    GenderSelectValue = GenderData[1];
                else if (MemberSelectValue.Gender == "F")
                    GenderSelectValue = GenderData[2];
                else
                    GenderSelectValue = GenderData[0];
                TextBoxMemberRank = MemberSelectValue.MemberRank;
                TextBoxPhoneNumber = MemberSelectValue.PhoneNumber;
                TextBoxAddress = ClientMemberData.Where(x => x.ID == MemberSelectValue.ID).ToList()[0].Address;
                TextBoxPS = ClientMemberData.Where(x => x.ID == MemberSelectValue.ID).ToList()[0].PS;
                TextBoxContTotal = ClientMemberData.Where(x => x.ID == MemberSelectValue.ID).ToList()[0].ConsTotal;
                AddOrEdit = false;
                DataGridEnable = false;
                MemberComboBoxEnable = true;
                MemberTextBoxEnable = false;
                MemberAddSetBtnEnable = false;
                MemberEditSetBtnEnable = false;
                MemberDelSetBtnEnable = false;
                SandCBtnEnable = true;
            }

        }
        public void MemberDel()
        {
            if (MemberSelectValue != null)
            {
                APIConnect.ResponsePut($@"MemberData/UpdateMemberAlive/{MemberSelectValue.ID}/false", "");
            }
            MemberData.Clear();
            MemberCancel();
            HttpResponseMessage response = APIConnect.ResponseGet("MemberData");
            if (response.IsSuccessStatusCode)
            {
                ClientMemberData = response.Content.ReadAsAsync<ObservableCollection<ClientMemberDataModel>>().Result;
                for (int i = 0; i < ClientMemberData.Count; i++)
                    MemberData.Add(new MemberDataModel()
                    {
                        ID = ClientMemberData[i].ID,
                        Name = ClientMemberData[i].Name,
                        Gender = ClientMemberData[i].Gender.Trim(),
                        MemberRank = ClientMemberData[i].MemberRank,
                        PhoneNumber = ClientMemberData[i].PhoneNumber
                    });
            }
        }
        public void MemberSave()
        {
            if (TextBoxName != null && TextBoxName != "" && GenderSelectValue.Gender != "")
            {
                HttpResponseMessage response;
                if (AddOrEdit == true)
                {
                    List<InsertMemberData> InsertMemberData = new List<InsertMemberData>();
                    InsertMemberData.Add(new InsertMemberData()
                    {
                        Name = TextBoxName,
                        Gender = PopGender(),
                        MemberRank = "Copper",
                        PhoneNumber = TextBoxPhoneNumber,
                        Address = TextBoxAddress,
                        PS = TextBoxPS,
                        ConsTotal = TextBoxContTotal,
                    });
                    string StringJson = JsonConvert.SerializeObject(InsertMemberData).Trim('[').Trim(']');
                    APIConnect.ResponsePost($@"MemberData/InsertMemberData", StringJson);
                    MemberSelectValue = null;
                }
                else if (AddOrEdit == false)
                {
                    List<UpdateMemberData> UpdateMemberData = new List<UpdateMemberData>();
                    UpdateMemberData.Add(new UpdateMemberData()
                    {
                        Name = TextBoxName,
                        Gender = PopGender(),
                        MemberRank = TextBoxMemberRank,
                        PhoneNumber = TextBoxPhoneNumber,
                        Address = TextBoxAddress,
                        PS = TextBoxPS,
                    });
                    string StringJson = JsonConvert.SerializeObject(UpdateMemberData).Trim('[').Trim(']');
                    APIConnect.ResponsePost($@"MemberData/UpdateMemberData/{MemberSelectValue.ID}", StringJson);
                }
                // Setting MemberRank
                if (AddOrEdit == true)
                {
                    long MaxID = 0;
                    response = APIConnect.ResponseGet($@"MemberData/MAXIDGet");
                    if (response.IsSuccessStatusCode)
                        MaxID = response.Content.ReadAsAsync<long>().Result;
                    JudgeMemberRank(Convert.ToInt32(MaxID));
                }
                else
                    JudgeMemberRank(MemberSelectValue.ID);
                // Reset Member Data
                int Pre_SelectValue = MemberData.IndexOf(MemberSelectValue), Pre_MemberCount = MemberData.Count;
                MemberData.Clear();
                response = APIConnect.ResponseGet("MemberData");
                if (response.IsSuccessStatusCode)
                {
                    ClientMemberData = response.Content.ReadAsAsync<ObservableCollection<ClientMemberDataModel>>().Result;
                    for (int i = 0; i < ClientMemberData.Count; i++)
                        MemberData.Add(new MemberDataModel()
                        {
                            ID = ClientMemberData[i].ID,
                            Name = ClientMemberData[i].Name,
                            Gender = ClientMemberData[i].Gender.Trim(),
                            MemberRank = ClientMemberData[i].MemberRank,
                            PhoneNumber = ClientMemberData[i].PhoneNumber
                        });
                }
                MemberCancel();
                if (Pre_SelectValue != -1)
                    MemberSelectValue = MemberData[Pre_SelectValue];
                else if (MemberData.Count == Pre_MemberCount + 1)
                    MemberSelectValue = MemberData[MemberData.Count - 1];
                else
                    MemberSelectValue = null;
            }
            else
                MessageBox.Show("請輸入會員姓名及性別再進行儲存", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void MemberCancel()
        {
            TextBoxName = "";
            TextBoxMemberRank = "";
            TextBoxPhoneNumber = "";
            GenderSelectValue = GenderData[0];
            TextBoxAddress = "";
            TextBoxPS = "";
            TextBoxContTotal = 0;
            ConsTotalVis = Visibility.Hidden;
            DataGridEnable = true;
            MemberComboBoxEnable = false;
            MemberTextBoxEnable = true;
            MemberAddSetBtnEnable = true;
            MemberEditSetBtnEnable = true;
            MemberDelSetBtnEnable = true;
            SandCBtnEnable = false;
        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Private Methods
        private string PopGender()
        {
            if (GenderSelectValue != null)
            {
                if (GenderSelectValue.Gender == "Male")
                    return "M";
                else
                    return "F";
            }
            else
                return "";
        }
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

        #endregion
    }
}
