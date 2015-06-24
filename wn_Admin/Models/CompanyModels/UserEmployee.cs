using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class UserEmployee
    {
        [Key]
        public string UserID { get; set; }

        
        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}