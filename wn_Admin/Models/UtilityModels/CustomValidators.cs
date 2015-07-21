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

        public static ValidationResult CheckKM(int? start, int? end)
        {
            if(start != null && end != null){

                if(end < start){
                 
                    return new ValidationResult("EndKm must be greater than StartKM");
                }
            }

            return ValidationResult.Success;
        }
    }
}