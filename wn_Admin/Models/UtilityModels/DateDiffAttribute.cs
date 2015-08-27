using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace wn_Admin.Models.UtilityModels
{
    public class DateDiffAttribute : ValidationAttribute
    {
        private string[] mPropertyNames;
        public DateDiffAttribute(params string[] propertyNames)
        {
            this.mPropertyNames = propertyNames;
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            UserInfo ui = new UserInfo();
            
            var props = mPropertyNames.Select(validationContext.ObjectType.GetProperty);
            var date = props.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<DateTime>().FirstOrDefault();
            var endDate = Convert.ToDateTime(value);
            var userId = props.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<string>().FirstOrDefault();

            if (ui.isInRole(userId, "Accountant") || ui.isInRole(userId, "SUPERADMIN"))
            {
                return null;
            }

            DateTime current = DateTime.Now;
            if (date > current)
            {
                return new ValidationResult("Date should not be in the future.");
            }
            else
            {
                DateTime minDate = current.AddDays(-10);

                if (date <= minDate)
                {
                    return new ValidationResult("Date must be within 10 days.");
                }


            }

            if (endDate < date)
            {
                return new ValidationResult("End Date must be later than Start Date");
            }


            if (endDate.AddDays(-1) > date)
            {
                return new ValidationResult("End Date must be within 1 day of Start Date");
            }

            return null;
        }
    }
}