using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace wn_Admin.Models.UtilityModels
{
    public class DataFilterService
    {
        public DataFilterModel filter(wn_admin_db db, int[] employeeId, string[] tasks, int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, string projectId = null, string isReviewed = null, Boolean? exportToExcel = null, Boolean? isGroupBy = null)
        {

            DataFilterModel dataFilterModel = new DataFilterModel();
            var workings = db.Workings.Include(w => w.Employee).Include(w => w.Project);
            string currentFilter = "";
            string dateRange = "";

            // Search Filters
            // Employees
            if (employeeId != null && employeeId.Count() > 0)
            {
                workings = workings.Where(w => employeeId.Contains(w.EmployeeID));
                currentFilter += "&employeeId=" + string.Join("&employeeId=", employeeId);
            }

            // Date
            if (startDate != null)
            {
                workings = workings.Where(w => w.Date >= startDate || w.EndDate >= startDate);
                currentFilter += "&startDate=" + String.Format("{0:yyyy-MM-dd}", startDate);

                dateRange = "From " + String.Format("{0:yyyy-MM-dd}", startDate);
            }

            if (endDate != null)
            {
                endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59, 999);
                workings = workings.Where(w => w.Date <= endDate || w.EndDate <= endDate);
                currentFilter += "&endDate=" + String.Format("{0:yyyy-MM-dd}", endDate);

                dateRange += " To " + String.Format("{0:yyyy-MM-dd}", endDate);
            }

            // Get working records within 2 months
            if (startDate == null && endDate == null && ppYear == -1 && pp == -1)
            {
                DateTime now = TimesheetDateValidator.getEdmontonTime();
                DateTime preMonth = now.AddMonths(-1);
                workings = workings.Where(w => w.Date >= preMonth || w.EndDate >= preMonth);

                // Used by timesheet summary
                dateRange = "From " + preMonth.ToString("yyyy-MM-dd") + " to " + now.ToString("yyyy-MM-dd");

            }

            // Pay Period Year
            if (ppYear != -1)
            {
                workings = workings.Where(w => w.PPYr == ppYear);
                currentFilter += "&ppYear=" + ppYear;
            }

            //Pay Period
            if (pp != -1)
            {
                workings = workings.Where(w => w.PP == pp);
                currentFilter += "&pp=" + pp;
            }

            //Task
            if (tasks != null && tasks.Count() > 0)
            {
                workings = workings.Where(w => tasks.Contains(w.Task));
                currentFilter += "&tasks=" + string.Join("&tasks=", tasks);
            }

            //Client
            if (clientId != -1)
            {
                workings = workings.Where(w => w.Project.Client == clientId);
                currentFilter += "&clientId=" + clientId;
            }

            //Project
            if (!String.IsNullOrWhiteSpace(projectId))
            {
                workings = workings.Where(w => w.ProjectID.Equals(projectId));
                currentFilter += "&projectId=" + projectId;
            }

            // Aprovel
            if (!String.IsNullOrWhiteSpace(isReviewed))
            {
                workings = workings.Where(w => w.isReviewed == (isReviewed.Equals("Yes")));
                currentFilter += "&isReviewed=" + isReviewed;
            }


            dataFilterModel.Workings = workings;
            dataFilterModel.filters = currentFilter;





            return dataFilterModel;
        }
    }
}