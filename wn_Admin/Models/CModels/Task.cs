using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CModels
{
    public class Task
    {
        public int TaskID { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(100)]
        [DisplayName("Task")]
        public string TaskName { get; set; }
    }
}