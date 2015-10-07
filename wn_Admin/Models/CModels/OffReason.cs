using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CModels
{
    public class OffReason
    {
        public int OffReasonID { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(100)]
        [DisplayName("Off Details")]
        public string OffReasonName { get; set; }

        public int Status { get; set; }
    }
}