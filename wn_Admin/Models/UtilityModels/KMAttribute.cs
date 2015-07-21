using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class KMAttribute : ValidationAttribute
    {
        public string[] mPropertyNames { get; private set; }
        public KMAttribute(params string[] propertyNames)
        {
            this.mPropertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.mPropertyNames.Select(validationContext.ObjectType.GetProperty);
            var startKM = properties.Select(s => s.GetValue(validationContext.ObjectInstance, null)).OfType<int>().FirstOrDefault();
            var endKM = Convert.ToInt32(value);

            if (endKM < startKM)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}