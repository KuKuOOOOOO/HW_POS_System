using HW_POS_Server.DataBaseData;
using HW_POS_Server.Models;
using HW_POS_Server.Models.ServerModels;
using HW_POS_Server.Views.ServerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HW_POS_Server.ViewModels.ServerViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        #region Fields
        private ReportModel Report { get; set; }
        private SQLCommandClass commandClass = new SQLCommandClass();
        public ServerViewModel Server { get; set; }

        #endregion
        public ReportViewModel()
        {
            Server = new ServerViewModel();
            Report = new ReportModel()
            {
                _StartDateData = DateTime.Now.AddDays(-1),
                _EndDateData = DateTime.Now,
                _DateControlVis = Visibility.Hidden,
            };
        }


        #region Properties
        #region DateTime Properties
        public DateTime StartDateData
        {
            get { return Report._StartDateData; }
            set
            {
                Report._StartDateData = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDateData
        {
            get { return Report._EndDateData; }
            set
            {
                Report._EndDateData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Visibility Properties
        public Visibility DateControlVis
        {
            get { return Report._DateControlVis; }
            set
            {
                Report._DateControlVis = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Button Properties
        public ICommand DateSaveBtn
        {
            get { return new RelayCommand(DateSave, CanExecute); }
        }
        public ICommand DateCancelBtn
        {
            get { return new RelayCommand(DateCancel, CanExecute); }
        }
        #endregion
        #endregion

        #region Public Methods
        public void DateSave()
        {
            if (StartDateData.Date == EndDateData.Date || StartDateData.Date > EndDateData.Date)
            {
                MessageBox.Show("請輸入正確的開始日期及結束日期\n(開始日期必須大於結束日期)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                commandClass.UpdateCheckOutReportViewToDB(true, StartDateData.ToString("yyyy-MM-dd"), EndDateData.Date.ToString("yyyy-MM-dd"));
                MainWindow w = new MainWindow();
                w.Content = new ReportControl();
                w.Show();
                //ReportVis = Visibility.Visible;
                DateCancel();
            }
        }
        public void DateCancel()
        {
            DateControlVis = Visibility.Hidden;
            StartDateData = DateTime.Now.AddDays(-1);
            EndDateData = DateTime.Now;
            commandClass.UpdateCheckOutReportViewToDB(false, StartDateData.ToString("yyyy-MM-dd"), EndDateData.Date.ToString("yyyy-MM-dd"));
        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion
    }
}
