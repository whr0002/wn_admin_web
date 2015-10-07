using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class EmployeeRole
    {
        public int EmployeeRoleID { get; set; }

        public int EmployeeID { get; set; }

        public int RoleID { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual ERole Role { get; set; }
    }
}