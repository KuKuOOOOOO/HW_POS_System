using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW_POS_Server.Models.ServerModels
{
    public class ReportModel
    {
        #region Fields
        public DateTime _StartDateData { get; set; }
        public DateTime _EndDateData { get; set; }
        public Visibility _DateControlVis { get; set; }
        #endregion
    }
}
