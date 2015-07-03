using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.Safety
{
    public class SafetyLeavingItem
    {
        public int SafetyLeavingItemID { get; set; }

        [Index(IsUnique=true)]
        [StringLength(100)]
        [DisplayName("Prior to leaving item")]
        public string SafetyLeavingItemName { get; set; }
    }
}