using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.Safety
{
    public class SafetyObservation
    {
        public int SafetyObservationID { get; set; }
        public int EmployeeID { get; set; }
        public string Crew { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Location { get; set; }
        public string Observation { get; set; }
        public string Description { get; set; }
        public string Investigation { get; set; }
        public string KeyState { get; set; }
        public string CriticalError { get; set; }
        public string Solution { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAction { get; set; }


        public int Status { get; set; }


        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}