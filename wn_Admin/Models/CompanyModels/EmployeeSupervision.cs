using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class EmployeeSupervision
    {
        public int EmployeeSupervisionID { get; set; }
        public int SupervisorID { get; set; }

        public int EmployeeID { get; set; }

        [ForeignKey("SupervisorID")]
        public virtual Employee Supervisor { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}