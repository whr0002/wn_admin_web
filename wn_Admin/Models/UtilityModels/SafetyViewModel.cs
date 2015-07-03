using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_Admin.Models.Safety;

namespace wn_Admin.Models.UtilityModels
{
    public class SafetyViewModel
    {
        public int currentStep { get; set; }
        public List<SafetyStep> steps { get; set; }
    }

    public class SafetyLeavingViewModel
    {
        public int MeetingID { get; set; }
        public List<SafetyLeavingItem> LeavingItems { get; set; }
        public List<YesNoNA> YesNoNAs { get; set; }
    }
}