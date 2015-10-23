using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class EmployeeSummary
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public double TotalHour { get; set; }
        public double? TotalBank { get; set; }
        public double? TotalOT { get; set; }
    }
}