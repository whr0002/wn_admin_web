using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class TimesheetDateValidator
    {
        public static string ValidateTimesheetDateRange(DateTime date, DateTime endDate)
        {

            DateTime current = getEdmontonTime();

            if (date > current)
            {
                return "Date should not be in the future.";
            }else{
                DateTime minDate = current.AddDays(-10);

                if (date <= minDate)
                {
                    return "Date must be within 10 days.";
                }

                
            }

            if (endDate < date)
            {
                return "End Date must be later than Start Date";
            }


            if (endDate.AddDays(-1) > date)
            {
                return "End Date must be within 1 day of Start Date";
            }

            return null;

        }

        public static DateTime getEdmontonTime()
        {
            DateTimeOffset offset = new DateTimeOffset(DateTime.UtcNow).ToOffset(TimeSpan.FromHours(-6));
            //DateTime current = offset.;

            return offset.DateTime;
        }



    }
}