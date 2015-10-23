using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Models.UtilityModels
{
    public class EmployeeFormService
    {
        public EmployeeFormViewModel getEmployeeFormViewModel(wn_admin_db db, string userId, UserInfo mUserInfo, Employee employee, IQueryable<Working> workings)
        {
            EmployeeFormViewModel ef = new EmployeeFormViewModel();

            if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
            {

                // User is an accoutant, he or she can search Timesheets by Employee ID
                ef.hasReviewControl = true;
                ef.hasFullControl = true;
                ef.EmployeeID = new MultiSelectList(db.Employees.Where(w => w.Status == 1).OrderBy(o => o.FirstMidName), "EmployeeID", "FullName");
                ef.clientId = new SelectList(db.Clients, "ClientID", "ClientName");
                ef.projectId = new SelectList(db.Projects, "ProjectID", "ProjectName");
                ef.workings = workings;
            }
            else
            {
                // User is not accountant, show records from this user
                if (employee != null)
                {
                    // Get all time sheets from employees who are under THIS PERSON's supervision
                    var tempAll = from ws in db.WorkingSupervisors
                                  join tm in workings
                                  on ws.WorkingID equals tm.WorkingID
                                  where ws.EmployeeID == employee.EmployeeID
                                  select tm;


                    // Get all time sheets from THIS PERSON
                    var personalWorkings = workings.Where(w => w.EmployeeID == employee.EmployeeID);
                    workings = tempAll.Union(personalWorkings);
                    ef.workings = workings;
                    // Give corresponding authorization
                    if (tempAll.Count() > 0)
                    {
                        ef.hasReviewControl = true;
                        ef.CurrentEID = employee.EmployeeID;
                    }
                    else
                    {
                        ef.hasReviewControl = false;
                    }
                    var selfList = db.Employees.Where(w => w.EmployeeID == employee.EmployeeID);

                    ef.EmployeeID = new MultiSelectList(tempAll.Where(w => w.Employee.Status == 1).Select(s => s.Employee).Union(selfList).Distinct().OrderBy(o => o.FirstMidName), "EmployeeID", "FullName");
                }
                else
                {
                    //return View(new List<Working>());
                    ef.workings = (IQueryable<Working>)new List<Working>();
                }


                ef.EID = employee.EmployeeID;
                ef.hasFullControl = false;
                ef.clientId = new SelectList(workings.Select(s => s.Project.FK_Client).Distinct(), "ClientID", "ClientName");
                ef.projectId = new SelectList(workings.Select(s => s.Project).Distinct(), "ProjectID", "ProjectName");

            }

            ef.isReviewed = new SelectList(db.YesNoNA.Where(w => w.YesNoNAName != "N/A"), "YesNoNAName", "YesNoNAName");
            ef.tasks = new MultiSelectList(db.Tasks, "TaskName", "TaskName");

            return ef;
        }
    }
}