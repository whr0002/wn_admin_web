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

        public EventService(Employee employee) {
            this.mEmployee = employee;
        }

        public List<EventViewModel> getEventViewModel(IQueryable<Working> workings)
        {
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

            var missings = getMissingTimesheetEvents(events);
            var eventList = events.ToList();
            eventList.AddRange(missings);

            // Giving each event color and description.
            List<EventViewModel> evm = new List<EventViewModel>();

            foreach (var e in eventList)
            {
                EventViewModel eventViewModel = new EventViewModel();
                eventViewModel.start = e.startDT.ToString("yyyy-MM-dd");
                eventViewModel.end = e.endDT.AddDays(1).ToString("yyyy-MM-dd");
                

                //e.start = e.startDT.ToString("yyyy-MM-dd");
                //e.backgroundColor = (e.totalHours == 0) ? "#004C99" : "#808080";
                if (e.totalHours == -1) {
                    eventViewModel.backgroundColor = "#ff0000";
                }
                else if (e.totalHours == 0)
                {
                    e.title += ": Off";
                    eventViewModel.backgroundColor = "#004C99";
                }
                else 
                {
                    e.title += ": " + e.totalHours + " h";
                    eventViewModel.backgroundColor = "#808080";
                }

                eventViewModel.title = e.title;

                evm.Add(eventViewModel);
            }

            return evm;
        }

        private List<EventModel> getMissingTimesheetEvents(IQueryable<EventModel> events)
        {
            List<EventModel> ems = new List<EventModel>();
            var employees = getEmployeeList();

            DateTime now = TimesheetDateValidator.getEdmontonTime().Date;
            DateTime preMonth = now.AddDays(-7);
            while ( preMonth <= now)
            {
                var eventsByDay = getEventsByDate(events, preMonth);
                var missingEvents = getMissingEventsByDate(eventsByDay, employees, preMonth);
                ems.AddRange(missingEvents);
                preMonth = preMonth.AddDays(1);
            }

            return ems;
        }

        private IQueryable<EventModel> getEventsByDate(IQueryable<EventModel> events, DateTime date)
        {
            IQueryable<EventModel> eventsByDate = events.Where(w => w.startDT == date);

            return eventsByDate;
        }

        private List<EventModel> getMissingEventsByDate(IQueryable<EventModel> events, IQueryable<Employee> employees, DateTime date) {

            List<EventModel> missingEvents = new List<EventModel>();
            Hashtable hashtable = new Hashtable();
            foreach (var i in events)
            {
                hashtable[i.employeeID] = i;
            }

            foreach (var employee in employees)
            {
                if (hashtable[employee.EmployeeID] == null)
                {
                    // Missing time sheet from this employee
                    // Create a missing event
                    EventModel em = new EventModel();
                    em.employeeID = employee.EmployeeID;
                    em.startDT = date;
                    em.endDT = date;
                    em.title = "-Missing: " + employee.FirstMidName;
                    em.totalHours = -1;

                    missingEvents.Add(em);
                }
            }

            return missingEvents;
        }

        private IQueryable<Employee> getEmployeeList() {
            wn_admin_db db = new wn_admin_db();
            var employees = db.Employees.Where(w => w.Status == 1);
            UserInfo mUserInfo = new UserInfo();

            if (mUserInfo.isInRole(mEmployee.EmployeeID, "SUPERADMIN") || mUserInfo.isInRole(mEmployee.EmployeeID, "Accountant"))
            {
                return employees;
            }
            else
            {
                var groupList = db.EmployeeSupervisions.Where(w => w.SupervisorID == mEmployee.EmployeeID).Select(s => s.EmployeeID);
                employees = employees.Where(w => w.EmployeeID == mEmployee.EmployeeID || groupList.Contains(w.EmployeeID));
                return employees;
            }
        }
    }
}