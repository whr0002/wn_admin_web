using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class SummaryModel
    {
        public IEnumerable<Working> MixedWorkings { get; set; }
        public IEnumerable<SummaryWorkingModel> IndividualWorkings { get; set; }
        public IEnumerable<Expense> ProjectExpenses { get; set; }
    }

    public class SummaryWorkingModel
    {
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime Date { get; set; }

        [DisplayName("Job Description")]
        public string JobDescription { get; set; }

        public double Hours { get; set; }

        public double? Bank { get; set; }

        [DisplayName("Over Time")]
        public double? OverTime { get; set; }

        public int ClientID { get; set; }
        public string ProjectID { get; set; }
    }
}