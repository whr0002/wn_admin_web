using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CModels
{
    public class Timesheet
    {
        public int TimesheetID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? PPyr { get; set; }
        public int? PP { get; set; }
        public string Client { get; set; }
        public string Project { get; set; }
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

        [Required]
        public int? Hours { get; set; }
        public int? Bank { get; set; }
        public int? OT { get; set; }


        [ForeignKey("Client")]
        public virtual Client FK_Client { get; set; }
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