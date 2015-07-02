using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class CustomValidators
    {
        public static ValidationResult CheckYNN(string value)
        {
            if (value != null && (value.Equals("Yes") || value.Equals("No") || value.Equals("N/A")))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Only 'Yes', 'No', or 'N/A' is allowed.");
        }
    }
}