using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class WorksFor
    {
        [Key]
        [Column(Order=0)]
        public int EmployeeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int DepartmentID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }
    }
}