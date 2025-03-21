using HW_POS_Server.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HW_POS_Server.ViewModels.ServerViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        #region Fields
        SalesModel Sales { get; set; }
        #endregion
        public SalesViewModel()
        {
            #region Constructor
            Sales = new SalesModel()
            {
                _SalesVis = Visibility.Collapsed,
            };
            #endregion
        }

        #region Properties
        public Visibility SalesVis
        {
            get { return Sales._SalesVis; }
            set
            {
                Sales._SalesVis = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalesCommand
        {
            get { return new RelayCommand(ShowSales, CanExecute); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(CollapsedSales, CanExecute); }
        }

        #endregion

        #region Public methods
        public void ShowSales()
        {
            SalesVis = Visibility.Visible;
        }
        public void CollapsedSales()
        {
            SalesVis = Visibility.Collapsed;
        }
        public bool CanExecute()
        {
            return true;
        }
        #endregion
    }
}
