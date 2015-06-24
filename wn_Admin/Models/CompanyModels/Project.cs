using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models
{
    public class Project
    {
        [DisplayName("Project")]
        public string ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        public int Client { get; set; }


        [ForeignKey("Client")]
        public virtual Client FK_Client { get; set; }
        

        public virtual ICollection<Working> Workings { get; set; }

    }
}