using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.Safety
{
    public class SafetyMeeting
    {


        public int SafetyMeetingID { get; set; }

        public int EmployeeID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime Date { get; set; }

        public string ProjectID { get; set; }

        [DisplayName("Field Location/ID")]
        public string FieldLocation { get; set; }

        [DisplayName("Safe Work Permit # (if applicable)")]
        public string SafeWorkPermitNum { get; set; }

        [DisplayName("Scope of Work & Meeting Agenda")]
        public string ScopeOfWork { get; set; }

        public bool IsReviewedBySafetyManager { get; set; }

        public bool  IsDone { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<SafetyItem> SafetyItems { get; set; }

        public virtual ICollection<EmployeeSafetyMeeting> EmployeeSafetyMeetings { get; set; }
    }
}