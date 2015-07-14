using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class SafetyCategory
    {
        [DisplayName("Safety Category")]
        public int SafetyCategoryID { get; set; }

        public string SafetyCategoryName { get; set; }
    }
}