using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class TimesheetDateValidator
    {
        public static ValidationResult ValidateTimesheetDateRange(DateTime date)
        {
            DateTime current = DateTime.Now;
            if (date > current)
            {
                return new ValidationResult("Date should not be in the future.");
            }else{
                DateTime minDate = current.AddDays(-10);

                if (date <= minDate)
                {
                    return new ValidationResult("Date must be within 10 days.");
                }

                
            }

            return ValidationResult.Success;

        }

    }
}