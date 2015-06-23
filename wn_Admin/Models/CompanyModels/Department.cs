using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }



        public virtual ICollection<Control> Controls { get; set; }
    }
}