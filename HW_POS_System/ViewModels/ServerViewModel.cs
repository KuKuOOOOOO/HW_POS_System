using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Reporting.WinForms;
using System.Windows.Input;
using HW_POS_Server.Models;
using HW_POS_Server.DataBaseData;
using System.IO;
using System.Security.AccessControl;
using System.Linq.Expressions;
using System.Windows.Data;
using HW_POS_Server.ViewModels.ServerViewModels;
using HW_POS_Server.ViewModels.ClientViewModels;
using HW_POS_Server.Models.ClientModels;
using System.Security;
using HW_POS_Server.Views.ServerViews;
using System.Windows.Forms.Integration;
using System.Data;
using Xceed.Wpf.AvalonDock.Layout;
using System.Windows.Shapes;
using HW_POS_Server.Views.ClientViews;
using System.Windows.Controls;

namespace HW_POS_Server.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        #region Fields
        private ServerModel Server { get; set; }

        private SQLCommandClass commandClass = new SQLCommandClass();

        public event EventHandler<ExitArgs> Exit;
        #endregion
        public ServerViewModel()
        {
            Server = new ServerModel()
            {
                #region Server Constructor
                _AccountBox = "",
                //_PasswordBox = "",
                _StaffName = "",
                _CheckOutReportChecked = true,
                _MemberConsReportChecked = false,
                _DatePickerEnable = true,
                _LoginSCVis = Visibility.Visible,
                _ServerSCVis = Visibility.Hidden,
                _ReportVis = Visibility.Hidden,
                _DateControlVis = Visibility.Hidden,
                _ServerButtonVis = Visibility.Hidden,
                _StartDateData = DateTime.Now,
                _EndDateData = DateTime.Now.AddDays(1),
                #endregion

                #region Client Constructor
                _ClientButtonVis = Visibility.Visible,
                #endregion
            };
        }

        #region Properties

        #region Server Properties

        #region Login Properties
        public string AccountBox
        {
            get { return Server._AccountBox; }
            set
            {
                Server._AccountBox = value;
                OnPropertyChanged();
            }
        }
        public SecureString PasswordBox
        {
            get { return Server._PasswordBox; }
            set
            {
                Server._PasswordBox = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region MainWindow Properties
        public string StaffName
        {
            get { return Server._StaffName; }
            set
            {
                Server._StaffName = value;
                OnPropertyChanged();
            }
        }
        public string StaffGender
        {
            get { return Server._StaffGender; }
            set
            {
                Server._StaffGender = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Report Viewer Properties

        public ICollection<CheckOutReportModel> CheckOutReport = new List<CheckOutReportModel>();
        public ICollection<MemberConsReportModel> MemberConsReport = new List<MemberConsReportModel>();

        #endregion
        #region DateControl Properties
        public DateTime StartDateData
        {
            get { return Server._StartDateData; }
            set
            {
                Server._StartDateData = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDateData
        {
            get { return Server._EndDateData; }
            set
            {
                Server._EndDateData = value;
                OnPropertyChanged();
            }
        }
        public bool CheckOutReportChecked
        {
            get { return Server._CheckOutReportChecked; }
            set
            {
                Server._CheckOutReportChecked = value;
                OnPropertyChanged();
                JudgeCheckOutOrMemberCons();
            }
        }
        public bool MemberConsReportChecked
        {
            get { return Server._MemberConsReportChecked; }
            set
            {
                Server._MemberConsReportChecked = value;
                OnPropertyChanged();
                JudgeCheckOutOrMemberCons();
            }
        }
        public bool DatePickerEnable
        {
            get { return Server._DatePickerEnable; }
            set
            {
                Server._DatePickerEnable = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Visibility Properties
        public Visibility LoginSCVis
        {
            get { return Server._LoginSCVis; }
            set
            {
                Server._LoginSCVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ServerSCVis
        {
            get { return Server._ServerSCVis; }
            set
            {
                Server._ServerSCVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ReportVis
        {
            get { return Server._ReportVis; }
            set
            {
                Server._ReportVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility ServerButtonVis
        {
            get { return Server._ServerButtonVis; }
            set
            {
                Server._ServerButtonVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility DateControlVis
        {
            get { return Server._DateControlVis; }
            set
            {
                Server._DateControlVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region ICommand Properties
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login, CanExecute); }
        }
        public ICommand QuickCommand
        {
            get { return new RelayCommand(QuickLogin, CanExecute); }
        }
        public ICommand CencelCommand
        {
            get { return new RelayCommand(Cencel, CanExecute); }
        }
        public ICommand DateSaveBtn
        {
            get { return new RelayCommand(DateSave, CanExecute); }
        }
        public ICommand DateCancelBtn
        {
            get { return new RelayCommand(DateCancel, CanExecute); }
        }
        public ICommand ReportCommand
        {
            get { return new RelayCommand(ReportVisControl, CanExecute); }
        }
        #endregion
        #endregion

        #region Client Properties
        public Visibility ClientButtonVis
        {
            get { return Server._ClientButtonVis; }
            set
            {
                Server._ClientButtonVis = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ICommand ExitCommand
        {
            get { return new RelayCommand(CloseUserControl, CanExecute); }
        }
        public ICommand ServerClientChangedCommand
        {
            get { return new RelayCommand(ServerClientChanged, CanExecute); }
        }

        #endregion


        #region Public methods

        #region Server Public methods
        public void Login()
        {
            commandClass.UpdateStaffIsLogOutToDB(); // Reset Login Account
            string StringPasswordBox = SecureStringToString(PasswordBox);
            bool CheckLoginMember = commandClass.CheckLoginStaff(Convert.ToInt32(AccountBox), StringPasswordBox);
            if (CheckLoginMember != true)
            {
                MessageBox.Show("Account or Password is wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("Login success!!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                StaffName = commandClass.SelectStaffNameFromID(Convert.ToInt32(AccountBox));
                StaffGender = commandClass.SelectStaffGender(Convert.ToInt32(AccountBox), StringPasswordBox);
                commandClass.UpdateStaffIsLoginedToDB(Convert.ToInt32(AccountBox));
                LoginSCVis = Visibility.Collapsed;
                ServerSCVis = Visibility.Visible;
            }
        }
        public void QuickLogin()
        {
            int AngelID = 1;
            commandClass.UpdateStaffIsLogOutToDB(); // Reset Login Account
            //MessageBox.Show("Login success!!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            StaffName = commandClass.SelectStaffNameFromID(AngelID);
            StaffGender = commandClass.SelectStaffGender(AngelID, "123456");
            commandClass.UpdateStaffIsLoginedToDB(AngelID);
            LoginSCVis = Visibility.Collapsed;
            ServerSCVis = Visibility.Visible;
        }
        public void OnExit()
        {
            if (Exit != null)
                Exit(this, new ExitArgs());
        }
        public void Cencel()
        {
            var Result = System.Windows.MessageBox.Show("Do you want to Exit?", "Information", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            if (Result == System.Windows.MessageBoxResult.Yes)
                OnExit();

        }
        public void DateSave()
        {
            string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
            string OriginalContent_MemberConsReport = _path + @"\HW_POS_System\MemberConsReport.rdlc";
            string ContentStart_MemberConsReport = @"MemberConsReport.rdlc";
            string OriginalContent_CheckOutReport = _path + @"\HW_POS_System\CheckOutReport.rdlc";
            string ContentStart_CheckOutReport = @"CheckOutReport.rdlc";

            if (CheckOutReportChecked == true && MemberConsReportChecked == false)
            {
                if (StartDateData.Date == EndDateData.Date || StartDateData.Date > EndDateData.Date)
                {
                    MessageBox.Show("請輸入正確的開始日期及結束日期\n(開始日期必須大於結束日期)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    // Check CheckOutReport.rdlc exist
                    if (!File.Exists(ContentStart_CheckOutReport))
                        if (!File.Exists(OriginalContent_CheckOutReport))
                        {
                            System.Windows.MessageBox.Show($@"遺失檔案: {ContentStart_CheckOutReport}{Environment.NewLine}請通報後台人員", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                            File.Copy(OriginalContent_CheckOutReport, ContentStart_CheckOutReport, true);
                    else
                        Console.WriteLine($@"{ContentStart_CheckOutReport} is existed!");

                    commandClass.UpdateCheckOutListReportViewToDB(true, StartDateData.Date.ToString("yyyy-MM-dd"), EndDateData.Date.AddDays(-1).ToString("yyyy-MM-dd"));
                    MainWindow w = new MainWindow();
                    w.Content = new CheckOutReportControl();
                    w.Show();
                    //ReportVis = Visibility.Visible;
                    DateCancel();
                }
            }
            else
            {
                // Check MemberConsReport.rdlc exist
                if (!File.Exists(ContentStart_MemberConsReport))
                    if (!File.Exists(OriginalContent_MemberConsReport))
                    {
                        System.Windows.MessageBox.Show($@"遺失檔案: {ContentStart_MemberConsReport}{Environment.NewLine}請通報後台人員", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                        File.Copy(OriginalContent_MemberConsReport, ContentStart_MemberConsReport, true);
                else
                    Console.WriteLine($@"{ContentStart_MemberConsReport} is existed!");

                MainWindow w = new MainWindow();
                w.Content = new MemberConsReportControl();
                w.Show();
                DateCancel();
            }

        }

        public void DateCancel()
        {
            commandClass.UpdateCheckOutListReportViewToDB(false, StartDateData.Date.ToString("yyyy-MM-dd"), EndDateData.Date.AddDays(-1).ToString("yyyy-MM-dd"));
            DateControlVis = Visibility.Hidden;
            CheckOutReportChecked = true;
            MemberConsReportChecked = false;
            DatePickerEnable = true;
            StartDateData = DateTime.Now;
            EndDateData = DateTime.Now.AddDays(1);
        }
        public void ReportVisControl()
        {
            DateControlVis = Visibility.Visible;
            //ReportVis = Visibility.Visible;
        }
        #endregion
        public void CloseUserControl()
        {
            commandClass.UpdateStaffIsLogOutToDB();
            OnExit();

        }
        public void ServerClientChanged()
        {
            if (ServerButtonVis == Visibility.Hidden)
            {
                ClientButtonVis = Visibility.Hidden;
                ServerButtonVis = Visibility.Visible;
            }

            else
            {
                ServerButtonVis = Visibility.Hidden;
                ClientButtonVis = Visibility.Visible;
            }

        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Private methods
        private String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            catch (ArgumentNullException ex)
            {
                return "";
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        private void JudgeCheckOutOrMemberCons()
        {
            if (CheckOutReportChecked == true && MemberConsReportChecked == false)
                DatePickerEnable = true;
            else
                DatePickerEnable = false;
        }
        #endregion
    }
    public class ExitArgs : EventArgs
    {
        public ExitArgs() { }
    }
}