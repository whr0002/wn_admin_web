using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.Safety
{
    public class MinorAccidentForm
    {
        public int MinorAccidentFormID { get; set; }

        public int EmployeeID { get; set; }

        public string Position { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DisplayName("Date Reported")]
        public DateTime DateReported { get; set; }

        [DisplayName("Location of Event")]
        public string LocationOfEvent { get; set; }

        [DisplayName("Task conducted during event")]
        public string TaskConducted { get; set; }

        [DisplayName("Accident Type")]
        public string AccidentType { get; set; }

        [DisplayName("RelatingTo")]
        public string RelatingTo { get; set; }

        [DisplayName("Description of Event")]
        public string EventDesc { get; set; }

        [DisplayName("Cause Analysis")]
        public string CauseAnalysis { get; set; }

        public string KeyStates { get; set; }
        public string CriticalErrors { get; set; }

        // 1,2,3,4
        [DisplayName("Frequency of Exposure")]
        [Range(1,4)]
        public int FreqExpo { get; set; }

        [DisplayName("Hazard Probability")]
        [Range(1,4)]
        public int HazardProb { get; set; }

        [DisplayName("First Aid Required?")]
        public bool FirstAid { get; set; }

        [DisplayName("If Yes - what was applied?")]
        public string FirstAidDesc { get; set; }

        [DisplayName("Suggested Corrective Action")]
        public string CorrAction { get; set; }

        [DisplayName("Person(s) Responsible for Corrective Action")]
        public string PersonRespCorrAct { get; set; }

        [DisplayName("Date Corrective Action Completed")]
        public DateTime CorrActCompDate { get; set; }

        [DisplayName("Further Action Required")]
        public string FurtherActReq { get; set; }

        public bool isReviewed { get; set; }


        public virtual Employee Employee { get; set; }

    }
}