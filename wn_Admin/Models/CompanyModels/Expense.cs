using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.CompanyModels
{
    public class Expense
    {
        public int ExpenseID { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        public string ProjectID { get; set; }

        public int AccountTypeID { get; set; }

        [DisplayName("Date submitted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime DateSubmitted { get; set; }

        public string Item { get; set; }

        [DisplayName("Receipt Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceiptDate { get; set; }

        [DisplayName("Amount")]
        [Range(0, int.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        public string ReceiptLink { get; set; }

        [DisplayName("Is Approved?")]
        public bool isApproved { get; set; }



        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}