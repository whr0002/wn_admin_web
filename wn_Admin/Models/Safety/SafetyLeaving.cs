using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.Safety;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Models.Safety
{
    public class SafetyLeaving
    {

        public int SafetyLeavingID { get; set; }

        public int SafetyMeetingID { get; set; }

        [DisplayName("Item Name")]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Required]
        [CustomValidation(typeof(CustomValidators), "CheckYNN")]
        [DisplayName("Item Status")]
        [StringLength(15)]
        public string ItemStatus { get; set; }

        public virtual SafetyMeeting SafetyMeeting { get; set; }

    }
}