using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class EmployeeFormViewModel
    {
        public Boolean hasReviewControl { get; set; }
        public Boolean hasFullControl { get; set; }
        public MultiSelectList EmployeeID { get; set; }
        public SelectList clientId { get; set; }
        public SelectList projectId { get; set; }
        public int? CurrentEID { get; set; }
        public int? EID { get; set; }
        public SelectList isReviewed { get; set; }
        public MultiSelectList tasks { get; set; }
        public IQueryable<Working> workings { get; set; }

    }
}