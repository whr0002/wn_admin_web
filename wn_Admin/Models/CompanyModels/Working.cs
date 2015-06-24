﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Employee")]
        public int EmployeeID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Period Year")]
        public int PPYr { get; set; }

        [DisplayName("Period")]
        public int PP { get; set; }

        [DisplayName("Client")]
        public int ClientName { get; set; }

        [DisplayName("Project")]
        public string ProjectID { get; set; }

        
        public int Task { get; set; }
        public string Identifier { get; set; }

        [DisplayName("Vehicle")]
        public int Veh { get; set; }
        public string Crew { get; set; }

        [DisplayName("Start Km")]
        public int? StartKm { get; set; }

        [DisplayName("End Km")]
        public int? EndKm { get; set; }
        public bool GPS { get; set; }

        [DisplayName("Field Access")]
        public int Field { get; set; }
        public bool PD { get; set; }

        [DisplayName("Job Description")]
        public string JobDescription { get; set; }

        [DisplayName("Off Details")]
        public int OffReason { get; set; }
        
        public double Hours { get; set; }
        public int? Bank { get; set; }

        [DisplayName("Over time")]
        public int? OT { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("ClientName")]
        public virtual Client Client { get; set; }

        [ForeignKey("Field")]
        public virtual FieldAccess FK_FieldAccess { get; set; }

        [ForeignKey("OffReason")]
        public virtual OffReason FK_OffReason { get; set; }

        [ForeignKey("Task")]
        public virtual Task FK_Task { get; set; }
        [ForeignKey("Veh")]
        public virtual Vehicle FK_Vehicle { get; set; }
    }
}