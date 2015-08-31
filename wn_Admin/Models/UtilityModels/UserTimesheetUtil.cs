using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wn_Admin.Models.UtilityModels
{
    public class UserTimesheetUtil
    {
        private int mEmployeeId;
        private int mWorkingId;
        private wn_admin_db db;
        public UserTimesheetUtil(int employeeId, int workingId)
        {
            mEmployeeId = employeeId;
            mWorkingId = workingId;
            db = new wn_admin_db();
        }

        public double getTotalHoursByDate(DateTime date)
        {
            if (date == null) return 0;

            var dateFrom = new DateTime(date.Year, date.Month, date.Day);
            var dateTo = dateFrom.AddDays(1);
            var timesheets = db.Workings.Where(w => w.EmployeeID == mEmployeeId && w.Date >= dateFrom && w.EndDate < dateTo && w.WorkingID != mWorkingId);

            var total = timesheets.GroupBy(g => g.EmployeeID).Select(s => new { TotalHour = s.Sum(u => u.Hours) }).FirstOrDefault();

            if(total != null) 
                return total.TotalHour;

            return 0;
        }
    }
}