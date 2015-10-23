using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class DataFilterModel
    {
        public IQueryable<Working> Workings { get; set; }
        public string filters { get; set; }
    }
}