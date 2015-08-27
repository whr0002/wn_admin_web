using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class TotalHoursAttribute : ValidationAttribute
    {
        public string[] mPropertyNames { get; private set; }

        public TotalHoursAttribute(params string[] propertyNames)
        {
            this.mPropertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.mPropertyNames.Select(validationContext.ObjectType.GetProperty);
            var employeeID = properties.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<int>().FirstOrDefault();
            var date = properties.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<DateTime>().FirstOrDefault();
            var workingID = properties.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<int>().LastOrDefault();
            var currentHours = Convert.ToDouble(value);
            UserTimesheetUtil ut = new UserTimesheetUtil(employeeID,workingID);
            var totalHours = ut.getTotalHoursByDate(date) + currentHours;

            if (totalHours > 18)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}