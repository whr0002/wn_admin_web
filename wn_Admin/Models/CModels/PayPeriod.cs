using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CModels
{
    public class PayPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayPeriodID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime StartDate { get; set; }

        [DisplayName("Is it the current Pay Period Year?")]
        public bool isCurrent { get; set; }

    }
}