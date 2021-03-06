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

        [Index]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Time - Start of the day")]
        public DateTime Date { get; set; }

        [Index]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Time - End of the day")]
        public DateTime EndDate { get; set; }

        [DisplayName("Period Year")]
        [Index]
        public int PPYr { get; set; }

        [DisplayName("Period")]
        [Index]
        public int PP { get; set; }

        [DisplayName("Project")]
        [Required]
        public string ProjectID { get; set; }


        public string Task { get; set; }
        public string Identifier { get; set; }

        [DisplayName("Vehicle")]
        public string Veh { get; set; }

        public string Crew { get; set; }

        [DisplayName("Start Km")]
        [Range(0, 500000)]
        public int? StartKm { get; set; }

        [KM("StartKm", "Veh", ErrorMessage = "EndKm must be greater than StartKm")]
        [DisplayName("End Km")]
        [Range(0, 500000)]
        public int? EndKm { get; set; }

 
        public int KmDiff 
        { 
            
            get {
                if (StartKm != null && EndKm != null)
                {
                    int diff = (int)(EndKm - StartKm) - 200;

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

        [DisplayName("Per Diem")]
        public bool PD { get; set; }

        [DisplayName("Job Description")]
        [StringLength(300)]
        public string JobDescription { get; set; }

        [DisplayName("Off Details")]
        public string OffReason { get; set; }

        [Range(0, 18)]
        [TotalHours("EmployeeID", "Date", "WorkingID", ErrorMessage = "Total hours for a day must not be over 18 hours")]
        public double Hours { get; set; }

        [Range(0, 6)]
        public double? Bank { get; set; }

        [DisplayName("Over time")]
        [Range(0, 6)]
        public double? OT { get; set; }

        [Index]
        [DisplayName("Is Reviewed?")]
        public bool isReviewed { get; set; }

        public string Reviewer { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }

    }
}