﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wn_Admin.Models
{
    public class Project
    {

        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Client { get; set; }

        [ForeignKey("Client")]
        public virtual Client FK_Client { get; set; }
    }
}