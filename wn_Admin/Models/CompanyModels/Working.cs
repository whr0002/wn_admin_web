using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CModels;

namespace wn_Admin.Models.CompanyModels
{
    public class Working
    {

        public int WorkingID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int EmployeeID { get; set; }
        public string ProjectID { get; set; }

        public string Task { get; set; }
        public string Identifier { get; set; }
        public string Veh { get; set; }
        public string Crew { get; set; }
        public int? StartKm { get; set; }
        public int? EndKm { get; set; }
        public bool GPS { get; set; }


        public string Field { get; set; }
        public bool PD { get; set; }
        public string JobDescription { get; set; }
        public string Off { get; set; }
        
        public double Hours { get; set; }
        public int? Bank { get; set; }
        public int? OT { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("Field")]
        public virtual FieldAccess FK_FieldAccess { get; set; }
        [ForeignKey("Off")]
        public virtual Task FK_Off { get; set; }
        [ForeignKey("Task")]
        public virtual Task FK_Task { get; set; }
        [ForeignKey("Veh")]
        public virtual Vehicle FK_Vehicle { get; set; }
    }
}