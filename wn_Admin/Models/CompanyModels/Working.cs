﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CModels;
using wn_Admin.Models.UtilityModels;

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DisplayName("Period Year")]
        public int PPYr { get; set; }

        [DisplayName("Period")]
        public int PP { get; set; }

        [DisplayName("Project")]
        [Required]
        public string ProjectID { get; set; }

        [Required]
        public int Task { get; set; }
        public string Identifier { get; set; }

        [DisplayName("Vehicle")]
        public string Veh { get; set; }

        public string Crew { get; set; }

        [DisplayName("Start Km")]
        [Range(0, int.MaxValue)]
        public int? StartKm { get; set; }

        [KM("StartKm", ErrorMessage = "EndKm must be greater than StartKm")]
        [DisplayName("End Km")]
        [Range(0, int.MaxValue)]
        public int? EndKm { get; set; }

 
        public int KmDiff 
        { 
            
            get {
                if (StartKm != null && EndKm != null)
                {
                    int diff = (int)(EndKm - StartKm);

                    // Return only if distance >= 200 Km
                    if (diff >= 200)
                    {
                        return diff;
                    }
                    
                }
                
                return 0;
            }
        }

        public string Equipment { get; set; }

        [DisplayName("Field Access")]
        public string Field { get; set; }
        public bool PD { get; set; }

        [DisplayName("Job Description")]
        [StringLength(300)]
        public string JobDescription { get; set; }

        [DisplayName("Off Details")]
        public int OffReason { get; set; }

        [Range(0, 24)]
        public double Hours { get; set; }

        [Range(0, 6)]
        public int? Bank { get; set; }

        [DisplayName("Over time")]
        [Range(0,6)]
        public int? OT { get; set; }

        [DisplayName("Is Reviewed?")]
        public bool isReviewed { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }


        //[ForeignKey("Field")]
        //public virtual FieldAccess FK_FieldAccess { get; set; }

        [ForeignKey("OffReason")]
        public virtual OffReason FK_OffReason { get; set; }

        [ForeignKey("Task")]
        public virtual Task FK_Task { get; set; }
        //[ForeignKey("Veh")]
        //public virtual Vehicle FK_Vehicle { get; set; }
    }
}