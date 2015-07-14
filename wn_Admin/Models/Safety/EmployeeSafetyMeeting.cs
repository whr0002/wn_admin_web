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

        [Key]
        [Column(Order=1)]
        public int EmployeeID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SafetyMeetingID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SafetyMeeting SafetyMeeting { get; set; }


    }
}