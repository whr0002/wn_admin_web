using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string FirstMidName { get; set; }
        public string LastName { get; set; }

        private string _fullName;
        public string FullName
        {
            set { this._fullName = FirstMidName + " " + LastName; }
            get { return _fullName; }
        }



        public virtual ICollection<Working> Workings { get; set; }


    }
}