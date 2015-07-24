using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class TimesheetSummaryViewModel
    {
        public string EmployeeName { get; set; }
        public double TotalHours { get; set; }
        public string DateRange { get; set; }
    }
}