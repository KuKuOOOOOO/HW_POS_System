using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models;
using HW_POS_Server.Models.ServerModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HW_POS_Server.ViewModels.ServerViewModels
{
    public class StaffViewModel : ViewModelBase
    {
        #region Fields
        private SQLCommandClass commandClass = new SQLCommandClass();

        public ServerViewModel Server { get; } = new ServerViewModel();
        StaffModel Staff { get; set; }
        #endregion

        public StaffViewModel()
        {
            #region Constructor

            Staff = new StaffModel()
            {
                _TextBoxID = 0,
                _TextBoxName = "",
                _TextBoxNickName = "",
                _TextBoxPassword = "",
                _StaffVis = Visibility.Collapsed,
                _TextBoxPasswordVis = Visibility.Hidden,
                _TextBoxAddress = "",
                _TextBoxPhoneNumber = "",
                _TextBoxPS = "",
                _DataGridEnable = true,
                _StaffComboBoxEnable = false,
                _StaffTextBoxEnable = true,
                _AddStaffSetBtnEnable = true,
                _EditStaffSetBtnEnable = false,
                _DelStaffSetBtnEnable = false,
                _SandCBtnEnable = false,
            };
            List<int> StaffID = commandClass.SelectStaffIDAndAlive();
            for (int i = 0; i < StaffID.Count; i++)
                StaffData.Add(new StaffDataModel()
                {
                    ID = StaffID[i],
                    Name = commandClass.SelectStaffNameFromID(StaffID[i]),
                    Gender = commandClass.SelectStaffGenderFromID(StaffID[i]),
                    Permission = commandClass.SelectStaffPerminssionFromID(StaffID[i]),
                });
            GenderData.Add(new GenderDataModel() { Gender = "Male" });
            GenderData.Add(new GenderDataModel() { Gender = "Female" });
            PermissionData.Add(new PermissionDataModel() { Permission = "High" });
            PermissionData.Add(new PermissionDataModel() { Permission = "Medium" });
            PermissionData.Add(new PermissionDataModel() { Permission = "Low" });

            #endregion
        }
        #region Properties

        #region ObservableCollection Properties
        ObservableCollection<StaffDataModel> _StaffData = new ObservableCollection<StaffDataModel>();
        public ObservableCollection<StaffDataModel> StaffData
        {
            get { return _StaffData; }
            set
            {
                _StaffData = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<GenderDataModel> _GenderData = new ObservableCollection<GenderDataModel>();
        public ObservableCollection<GenderDataModel> GenderData
        {
            get { return _GenderData; }
            set
            {
                _GenderData = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<PermissionDataModel> _PermissionData = new ObservableCollection<PermissionDataModel>();
        public ObservableCollection<PermissionDataModel> PermissionData
        {
            get { return _PermissionData; }
            set
            {
                _PermissionData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Visibility Properties
        public Visibility StaffVis
        {
            get { return Staff._StaffVis; }
            set
            {
                Staff._StaffVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility TextBoxPasswordVis
        {
            get { return Staff._TextBoxPasswordVis; }
            set
            {
                Staff._TextBoxPasswordVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region BoolProperties
        public bool DataGridEnable
        {
            get { return Staff._DataGridEnable; }
            set
            {
                Staff._DataGridEnable = value;
                OnPropertyChanged();
            }
        }
        public bool StaffTextBoxEnable
        {
            get { return Staff._StaffTextBoxEnable; }
            set
            {
                Staff._StaffTextBoxEnable = value;
                OnPropertyChanged();
            }
        }

        public bool StaffComboBoxEnable
        {
            get { return Staff._StaffComboBoxEnable; }
            set
            {
                Staff._StaffComboBoxEnable = value;
                OnPropertyChanged();
            }
        }
        public bool AddStaffSetBtnEnable
        {
            get { return Staff._AddStaffSetBtnEnable; }
            set
            {
                Staff._AddStaffSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool EditStaffSetBtnEnable
        {
            get { return Staff._EditStaffSetBtnEnable; }
            set
            {
                Staff._EditStaffSetBtnEnable= value;
                OnPropertyChanged();
            }
        }
        public bool DelStaffSetBtnEnable
        {
            get { return Staff._DelStaffSetBtnEnable; }
            set
            {
                Staff._DelStaffSetBtnEnable = value;
                OnPropertyChanged();
            }
        }
        public bool SandCBtnEnable
        {
            get { return Staff._SandCBtnEnable; }
            set
            {
                Staff._SandCBtnEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Object Properties
        public int TextBoxID
        {
            get { return Staff._TextBoxID; }
            set
            {
                Staff._TextBoxID = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxName
        {
            get { return Staff._TextBoxName; }
            set
            {
                Staff._TextBoxName = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxNickName
        {
            get { return Staff._TextBoxNickName; }
            set
            {
                Staff._TextBoxNickName = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxPassword
        {
            get { return Staff._TextBoxPassword; }
            set
            {
                Staff._TextBoxPassword = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxAddress
        {
            get { return Staff._TextBoxAddress; }
            set
            {
                Staff._TextBoxAddress = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxPhoneNumber
        {
            get { return Staff._TextBoxPhoneNumber; }
            set
            {
                Staff._TextBoxPhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string TextBoxPS
        {
            get { return Staff._TextBoxPS; }
            set
            {
                Staff._TextBoxPS = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Staff DataGrid Properties
        public StaffDataModel StaffSelectValue
        {
            get { return Staff._StaffSelectValue; }
            set
            {
                Staff._StaffSelectValue = value;
                OnPropertyChanged();
                if(Staff._StaffSelectValue != null)
                {
                    EditStaffSetBtnEnable = true;
                    DelStaffSetBtnEnable = true;
                }
                else
                {
                    EditStaffSetBtnEnable = false;
                    DelStaffSetBtnEnable = false;
                }
                PopTextBox();
            }
        }
        #endregion
        #region Gender SelectValue Properties
        public GenderDataModel SelectGender
        {
            get { return Staff._SelectGender; }
            set
            {
                Staff._SelectGender = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Permission SelectValue Properties
        public PermissionDataModel SelectPermission
        {
            get { return Staff._SelectPermission; }
            set
            {
                Staff._SelectPermission = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region ICommand Propeties
        public ICommand StaffCommand
        {
            get { return new RelayCommand(ShowStaff, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedStaff, CanExecute); }
        }
        public ICommand StaffAddBtn
        {
            get { return new RelayCommand(StaffAdd, CanExecute); }
        }
        public ICommand StaffEditBtn
        {
            get { return new RelayCommand(StaffEdit, CanExecute); }
        }
        public ICommand StaffDelBtn
        {
            get { return new RelayCommand(StaffDelete, CanExecute); }
        }
        public ICommand StaffSaveBtn
        {
            get { return new RelayCommand(StaffSave, CanExecute); }
        }
        public ICommand StaffCancelBtn
        {
            get { return new RelayCommand(StaffCancel, CanExecute); }
        }
        public ICommand PasswordVisCommand
        {
            get { return new RelayCommand(PasswordVisNow, CanExecute); }
        }
        #endregion
        public bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Public methods
        public void ShowStaff()
        {
            StaffVis = Visibility.Visible;
        }
        public void CollapsedStaff()
        {
            StaffVis = Visibility.Collapsed;
        }
        public void PopTextBox()
        {

            if (StaffSelectValue != null)
            {
                TextBoxID = StaffSelectValue.ID;
                TextBoxName = StaffSelectValue.Name;
                TextBoxPassword = commandClass.SelectStaffPasswordFromID(TextBoxID);
                TextBoxNickName = commandClass.SelectStaffNickNameFromID(StaffSelectValue.ID);
                TextBoxPasswordVis = Visibility.Hidden;
                if (StaffSelectValue.Gender == "M")
                    SelectGender = GenderData[0];
                else
                    SelectGender = GenderData[1];
                if (StaffSelectValue.Permission == "High")
                    SelectPermission = PermissionData[0];
                else if (StaffSelectValue.Permission == "Medium")
                    SelectPermission = PermissionData[1];
                else
                    SelectPermission = PermissionData[2];
                TextBoxAddress = commandClass.SelectStaffAddressFromID(StaffSelectValue.ID);
                TextBoxPhoneNumber = commandClass.SelectStaffPhoneNumberFromID(StaffSelectValue.ID);
                TextBoxPS = commandClass.SelectStaffPSFromID(StaffSelectValue.ID);
            }
            else
            {
                TextBoxID = 0;
                TextBoxName = "";
                TextBoxNickName = "";
                TextBoxAddress = "";
                TextBoxPhoneNumber = "";
                TextBoxPS = "";
            }
        }
        public void PasswordVisNow()
        {
            if (TextBoxPasswordVis == Visibility.Visible)
                TextBoxPasswordVis = Visibility.Hidden;
            else
                TextBoxPasswordVis = Visibility.Visible;
        }
        public void StaffAdd()
        {

            int MaxID = commandClass.SelectStaffIDFromMAX();
            StaffSelectValue = null;
            AddStaffSetBtnEnable = false;
            EditStaffSetBtnEnable = false;
            DelStaffSetBtnEnable = false;
            SandCBtnEnable = true;
            StaffTextBoxEnable = false;
            TextBoxID = MaxID + 1;
            TextBoxName = "";
            TextBoxNickName = "";
            TextBoxPassword = "";
            TextBoxPasswordVis = Visibility.Hidden;
            TextBoxAddress = "";
            TextBoxPhoneNumber = "";
            TextBoxPS = "";
            StaffComboBoxEnable = true;
            DataGridEnable = false;
        }
        public void StaffEdit()
        {
            if (StaffSelectValue != null)
            {

                AddStaffSetBtnEnable = false;
                EditStaffSetBtnEnable = false;
                DelStaffSetBtnEnable = false;
                SandCBtnEnable = true;
                StaffTextBoxEnable = false;
                TextBoxID = StaffSelectValue.ID;
                TextBoxName = StaffSelectValue.Name;
                TextBoxPasswordVis = Visibility.Hidden;
                StaffComboBoxEnable = true;
                DataGridEnable = false;
            }
        }
        public void StaffDelete()
        {
            if (StaffSelectValue != null)
            {
                for (int i = 0; i < StaffData.Count; i++)
                {
                    if (StaffData[i].Name == StaffSelectValue.Name)
                    {
                        commandClass.DeleteStaffToDB(StaffSelectValue.ID);
                        StaffData.Remove(StaffData[i]);
                        break;
                    }
                    else
                        continue;
                }
            }
        }
        public void StaffSave()
        {
            bool isSaved = false;
            if (TextBoxID != 0 && TextBoxPassword != "")
            {
                for (int i = 0; i < StaffData.Count; i++)
                {
                    if (TextBoxID == StaffData[i].ID)
                    {
                        bool IsLogined = false;
                        if (commandClass.SelectStaffIDFromIsLogined() == TextBoxID) IsLogined = true;
                        isSaved = commandClass.UpdateStaffToDB(TextBoxID, TextBoxName, TextBoxPassword, PopGender(), SelectPermission.Permission, TextBoxNickName, TextBoxAddress, TextBoxPhoneNumber, TextBoxPS, IsLogined);
                        break;
                    }
                    else if (i == StaffData.Count - 1)
                        isSaved = commandClass.InsertStaffToDB(TextBoxName, TextBoxPassword, PopGender(), SelectPermission.Permission, TextBoxNickName, TextBoxAddress, TextBoxPhoneNumber, TextBoxPS);
                    else
                        continue;
                }
            }
            if (isSaved == true)
            {
                int Pre_SelectValue = StaffData.IndexOf(StaffSelectValue), Pre_StaffCount = StaffData.Count;
                StaffData.Clear();
                List<int> StaffID = commandClass.SelectStaffIDAndAlive();
                for (int i = 0; i < StaffID.Count; i++)
                    StaffData.Add(new StaffDataModel()
                    {
                        ID = StaffID[i],
                        Name = commandClass.SelectStaffNameFromID(StaffID[i]),
                        Gender = commandClass.SelectStaffGenderFromID(StaffID[i]),
                        Permission = commandClass.SelectStaffPerminssionFromID(StaffID[i]),
                    });
                StaffCancel();
                if (Pre_SelectValue != -1)
                    StaffSelectValue = StaffData[Pre_SelectValue];
                else if (StaffData.Count == Pre_StaffCount + 1) 
                    StaffSelectValue = StaffData[StaffData.Count - 1];
                else
                    StaffSelectValue = null;
                AddStaffSetBtnEnable = true;
                EditStaffSetBtnEnable = true;
                DelStaffSetBtnEnable = true;
                SandCBtnEnable = false;
            }
            else
            {
                MessageBox.Show("密碼不能為空值", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                
        }
        public void StaffCancel()
        {
            if (StaffSelectValue != null)
                TextBoxID = StaffSelectValue.ID;
            else
                TextBoxID = 0;
            TextBoxPasswordVis = Visibility.Hidden;
            StaffTextBoxEnable = true;
            StaffComboBoxEnable = false;
            DataGridEnable = true;
            AddStaffSetBtnEnable = true;
            EditStaffSetBtnEnable = true;
            DelStaffSetBtnEnable = true;
            SandCBtnEnable = false;
        }
        #endregion

        #region Private methods
        private string PopGender()
        {
            if (SelectGender != null)
            {
                if (SelectGender.Gender == "Male")
                    return "M";
                else
                    return "F";
            }
            else
                return "";
        }
        #endregion
    }
}
