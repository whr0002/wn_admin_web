using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Employee
    {
        [DisplayName("Employee")]
        public int EmployeeID { get; set; }

        [DisplayName("First Name")]
        public string FirstMidName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        private string _fullName;
        [DisplayName("Full Name")]
        public string FullName
        {
            set { this._fullName = FirstMidName + " " + LastName; }
            get { return _fullName; }
        }



        public virtual ICollection<Working> Workings { get; set; }


    }
}