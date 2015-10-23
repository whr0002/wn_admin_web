using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class EventService
    {
        private Employee mEmployee;
        private IQueryable<Employee> employees;
        private DateTime now;
        //private DateTime preMonthDate;
        private DateTime preWeekDate;
        private DateTime prePeriodDate;
        private wn_admin_db db;
        private bool showMissingEvents = true;

        public EventService(Employee employee, wn_admin_db db, DateTime? startDate = null, DateTime? endDate = null, int? ppYear = -1, int? pp = -1)
        {
            this.db = db;
            this.mEmployee = employee;
            this.employees = getEmployeeList();
            this.now = TimesheetDateValidator.getEdmontonTime().Date;
            this.preWeekDate = now.AddDays(-14);
            this.prePeriodDate = now.AddDays(-14);


            if (startDate != null || endDate != null || ppYear != -1 || pp != -1)
            {
                // User want to query data based on a time period, do not show missing events
                showMissingEvents = false;
            }

        }



        // Create a View Model for events
        public List<EventViewModel> getEventViewModel(IQueryable<Working> workings)
        {
            //var filteredWokring = workings.Where(w => w.Date >= preWeekDate);

            var events = (from s in workings
                          group s by new
                          {
                              s.EmployeeID,
                              s.Employee.FirstMidName,
                              Date = DbFunctions.TruncateTime(s.Date),
                              EndDate = DbFunctions.TruncateTime(s.EndDate)
                          }
                              into g
                              select new EventModel
                              {
                                  employeeID = g.Key.EmployeeID,
                                  title = g.Key.FirstMidName,
                                  totalHours = g.Sum(b => b.Hours),
                                  startDT = (DateTime)g.Key.Date,
                                  endDT = (DateTime)g.Key.EndDate
                              });


            var eventList = events.ToList();

            if (showMissingEvents) {
                var filteredWokring = workings.Where(w => w.Date >= prePeriodDate);
                var missings = getMissingTimesheetEvents(filteredWokring);
                eventList.AddRange(missings);
            }

            // Giving each event color and description.
            List<EventViewModel> evm = new List<EventViewModel>();

            foreach (var e in eventList)
            {
                EventViewModel eventViewModel = new EventViewModel();
                
                eventViewModel.start = e.startDT.ToString("yyyy-MM-dd");
                eventViewModel.end = e.endDT.AddDays(1).ToString("yyyy-MM-dd");
                

                if (e.totalHours == -1) {
                    // Missing event
                    eventViewModel.backgroundColor = "#ff0000";
                }
                else if (e.totalHours == 0)
                {
                    // Off event
                    e.title += ": Off";
                    eventViewModel.backgroundColor = "#004C99";
                }
                else 
                {
                    // Regular hour event
                    e.title += ": " + e.totalHours + " h";
                    eventViewModel.backgroundColor = "#808080";
                }

                eventViewModel.title = e.title;

                evm.Add(eventViewModel);
            }

            return evm;
        }
        

        private List<EventModel> getMissingTimesheetEvents(IQueryable<Working> workings)
        {

            List<EventModel> ems = new List<EventModel>();
            

            while (preWeekDate <= now)
            {               
                var query = from ee in
                                (from em in employees
                                 select new
                                 {
                                     em.EmployeeID,
                                     Title = em.FirstMidName
                                 })
                            join eg in
                                (from wk in workings
                                 where (DbFunctions.TruncateTime(wk.Date) == preWeekDate) || (DbFunctions.TruncateTime(wk.Date) <= preWeekDate && DbFunctions.TruncateTime(wk.EndDate) >= preWeekDate)
                                 group wk by new
                                 {
                                     EmployeeID = wk.EmployeeID,
                                     StartDate = DbFunctions.TruncateTime(wk.Date),
                                     EndDate = DbFunctions.TruncateTime(wk.EndDate),
                                 } into grouped
                                 select new
                                 {
                                     EmployeeID = grouped.Key.EmployeeID,
                                     StartDate = grouped.Key.StartDate,
                                     EndDate = grouped.Key.EndDate,
                                     TotalHours = grouped.Sum(s => s.Hours)
                                 })
                            on ee.EmployeeID  equals  eg.EmployeeID 
                            into joined
                            from j in joined.DefaultIfEmpty()
                            where j.EmployeeID == null
                            select new EventModel
                            {
                                employeeID = (int?)j.EmployeeID,
                                startDT = preWeekDate,
                                endDT = preWeekDate,
                                totalHours = -1,
                                title = ee.Title
                            };




                ems.AddRange(query.ToList());
                preWeekDate = preWeekDate.AddDays(1);
                            
                            
            }

            return ems;
        }

        // Get a list of active employees
        private IQueryable<Employee> getEmployeeList() {
            
            var employees = db.Employees.Where(w => w.Status == 1);
            UserInfo mUserInfo = new UserInfo();

            if (mUserInfo.isInRole(mEmployee.EmployeeID, "SUPERADMIN") || mUserInfo.isInRole(mEmployee.EmployeeID, "Accountant"))
            {
                return employees;
            }
            else
            {
                //var groupList = db.EmployeeSupervisions.Where(w => w.SupervisorID == mEmployee.EmployeeID).Select(s => s.EmployeeID);
                //employees = employees.Where(w => w.EmployeeID == mEmployee.EmployeeID || groupList.Contains(w.EmployeeID));
                employees = employees.Where(w => w.EmployeeID == mEmployee.EmployeeID);
                return employees;
            }
        }
    }
}