using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class SafetyItemValue
    {
        public int SafetyItemValueID { get; set; }

        [Index(IsUnique=true)]
        [StringLength(100)]
        [DisplayName("Prior to leaving item")]
        public string SafetyItemValueName { get; set; }

        public int SafetyCategoryID { get; set; }

        public virtual SafetyCategory SafetyCategory { get; set; }
    }
}