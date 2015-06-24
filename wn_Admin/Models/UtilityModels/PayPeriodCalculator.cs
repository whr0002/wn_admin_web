using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wn_Admin.Models.CModels;

namespace wn_Admin.Models.UtilityModels
{
    public class PayPeriodCalculator
    {
        private wn_admin_db mContext;

        public PayPeriodCalculator()
        {
            mContext = new wn_admin_db();
        }

        public PPViewModel getPayPeriod(DateTime selectedDate)
        {
            DateTime currentDate = selectedDate;
            int currentYear = currentDate.Year;
            PPViewModel ppModel = new PPViewModel();


            PayPeriod payperiod = mContext.PayPeriods.Where(w => w.isCurrent == true).FirstOrDefault();
            if (payperiod != null)
            {
                DateTime endDate = payperiod.StartDate.AddYears(1);
                if (selectedDate < payperiod.StartDate || selectedDate >= endDate)
                {
                    // Selected date is out of the range of current pay year.
                    return ppModel;
                }

                // Found the current year payperiod
                int diff = currentDate.Subtract(payperiod.StartDate).Days + 1;
                double result = Math.Ceiling(((double)diff) / 14);
                //Response.Write("Diff: " + diff + "<br />" + result + "<br />");

                ppModel.PPYear = currentYear;
                ppModel.PPNumber = (int)result;
                return ppModel;

            }

            return ppModel;
        }
    }
}