﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Supervision
    {
        public int SupervisionID { get; set; }


        [DisplayName("Supervisor Name")]
        public int SupervisorID { get; set; }

        [DisplayName("Project")]
        public string ProjectID { get; set; }


        [ForeignKey("SupervisorID")]
        public virtual Employee Supervisor { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
    }
}