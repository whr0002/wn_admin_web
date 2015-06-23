using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Control
    {
        [Required]
        [Key]
        [ForeignKey("Project")]
        public string ProjectID { get; set; }
        public int DepartmentID { get; set; }


        public virtual Project Project { get; set; }
        public virtual Department Department { get; set; }
    }
}