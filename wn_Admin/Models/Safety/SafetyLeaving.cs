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

        public int SafetyLeavingItemID { get; set; }

        public int YesNoNAID { get; set; }

        public virtual SafetyMeeting SafetyMeeting { get; set; }
        public virtual SafetyLeavingItem SafetyLeavingItem { get; set; }
        public virtual YesNoNA YesNoNA { get; set; }
    }
}