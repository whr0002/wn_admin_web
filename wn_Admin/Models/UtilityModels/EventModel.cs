using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class EventModel
    {
        public int? employeeID { get; set; }
        public string title { get; set; }
        public DateTime startDT { get; set; }
        public DateTime endDT { get; set; }
        public double? totalHours { get; set; }
        public string start { get; set; }
        public string backgroundColor { get; set; }
    }
}