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
    public class SafetyItem
    {

        public int SafetyItemID { get; set; }


        public int SafetyMeetingID { get; set; }

        public int SafetyCategoryID { get; set; }

        public string SafetyItemName { get; set; }

        public int? YesNoNAID { get; set; }

        public string Description { get; set; }

        public virtual SafetyMeeting SafetyMeeting { get; set; }
        public virtual SafetyCategory SafetyCategory { get; set; }
        public virtual YesNoNA YesNoNA { get; set; }
    }
}