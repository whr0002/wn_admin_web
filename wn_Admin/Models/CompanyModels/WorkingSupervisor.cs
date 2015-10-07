using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class WorkingSupervisor
    {
        public int WorkingSupervisorID { get; set; }

        public int WorkingID { get; set; }

        public int EmployeeID { get; set; }


        public virtual Working Working { get; set; }
        public virtual Employee Employee { get; set; }
    }
}