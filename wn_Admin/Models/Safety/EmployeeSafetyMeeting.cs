using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.Safety
{
    public class EmployeeSafetyMeeting
    {
        public int EmployeeID { get; set; }
        public int SafetyMeetingID { get; set; }
        public string ProjectID { get; set; }

        [Required]
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [Required]
        [ForeignKey("EmployeeID")]
        public virtual SafetyMeeting SafetyMeeting { get; set; }

        public virtual Project Project { get; set; }
    }
}