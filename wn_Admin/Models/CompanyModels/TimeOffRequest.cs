using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using wn_Admin.Models.CModels;

namespace wn_Admin.Models.CompanyModels
{
    public class TimeOffRequest
    {
        public int TimeOffRequestID { get; set; }

        public int EmployeeID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Return to Work")]
        public DateTime ReturnToWorkDate { get; set; }

        [DisplayName("# of days off")]
        public int NumberOfDays { get; set; }

        public string OffReasonID { get; set; }

        public string Notes { get; set; }


        public virtual Employee Employee { get; set; }
        //public virtual OffReason OffReason { get; set; }
    }
}