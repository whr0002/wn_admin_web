using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CModels;
using wn_Admin.Models.CompanyModels;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant, Employee")]
    public class WorkingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo mUserInfo = new UserInfo();

        // GET: Workings
        public ActionResult Index(int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, int employeeId = -1, string projectId = null, Boolean? exportToExcel = null)
        {


            var workings = db.Workings.Include(w => w.Employee).Include(w => w.FK_FieldAccess).Include(w => w.FK_OffReason).Include(w => w.FK_Task).Include(w => w.FK_Vehicle).Include(w => w.Project);
            string userId = User.Identity.GetUserId();
            string currentFilter = "";

            if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
            {

                // User is an accoutant, he or she can search Timesheets by Employee ID

                if (employeeId != -1)
                {
                    workings = workings.Where(w => w.EmployeeID == employeeId);
                    currentFilter += "&employeeId=" + employeeId;
                }
                ViewBag.hasReviewControl = true;
                ViewBag.hasFullControl = true;
                ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
                ViewBag.clientId = new SelectList(db.Clients, "ClientID", "ClientName");
                ViewBag.projectId = new SelectList(db.Projects, "ProjectID", "ProjectName");
            }
            else
            {
                // User is not accountant, show records from this user
                var employee = mUserInfo.getEmployee(userId);
                if (employee != null)
                {

                    // Query out all timesheets from this user and the timesheets of users under his supervision
                    var ids = from s in db.Supervisions
                              where s.SupervisorID == employee.EmployeeID
                              select s.EmployeeID;


                    workings = workings.Where(w => w.EmployeeID == employee.EmployeeID || (ids.Contains(w.EmployeeID) && w.isReviewed == false));

                    if (ids.Count() > 0)
                    {
                        ViewBag.hasReviewControl = true;
                        ViewBag.CurrentEID = employee.EmployeeID;
                    }
                    else
                    {
                        ViewBag.hasReviewControl = false;
                    }
                }
                else
                {
                    return View(new List<Working>());
                }

                ViewBag.hasFullControl = false;
                ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == employee.EmployeeID), "EmployeeID", "FullName");
                ViewBag.clientId = new SelectList(workings.Select(s => s.Project.FK_Client).Distinct(), "ClientID", "ClientName");
                ViewBag.projectId = new SelectList(workings.Select(s => s.Project).Distinct(), "ProjectID", "ProjectName");

            }

            // Search Filters

            // Date
            if (startDate != null)
            {
                workings = workings.Where(w => w.Date >= startDate);

                currentFilter += "&startDate=" + String.Format("{0:yyyy-MM-dd}", startDate);
            }

            if (endDate != null)
            {
                workings = workings.Where(w => w.Date <= endDate);
                currentFilter += "&endDate=" + String.Format("{0:yyyy-MM-dd}", endDate);
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

            if (exportToExcel != null && exportToExcel == true)
            {
                return Content(generateExcel(workings), "application/ms-excel");
            }

            ViewBag.CurrentFilter = currentFilter;
            workings = workings.OrderByDescending(o => o.Date);

            int pageSize = 15;
            int pageNumber = (page ?? 1);


            return View(workings.ToPagedList(pageNumber, pageSize));
        }

        // GET: Workings/Details/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }
            return View(working);
        }

        // GET: Workings/Create
        public ActionResult Create()
        {
            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            setEmployeeDropdowns();

            Working working = new Working();

            working.Date = DateTime.Today;
            PayPeriodCalculator calc = new PayPeriodCalculator();
            PPViewModel ppv = calc.getPayPeriod(working.Date);
            working.PPYr = ppv.PPYear;
            working.PP = ppv.PPNumber;


            return View(working);
        }

        // POST: Workings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingID,EmployeeID,Date,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours,Bank,OT")] Working working)
        {


            if (ModelState.IsValid)
            {

                // Check date to see if it is valid.
                string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date);
                string userId = User.Identity.GetUserId();

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    db.Workings.Add(working);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (validationError != null)
                    {
                        ModelState.AddModelError("Date", validationError);
                    }
                    else
                    {
                        db.Workings.Add(working);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }

            }

            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            setEmployeeDropdowns();
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName", working.Field);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Edit/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            setEmployeeDropdowns();
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName", working.Field);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // POST: Workings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Edit([Bind(Include = "WorkingID,EmployeeID,Date,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours,Bank,OT")] Working working)
        {
            if (ModelState.IsValid)
            {
                db.Entry(working).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            setEmployeeDropdowns();
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName", working.Field);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Delete/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Working working = db.Workings.Find(id);
            if (working == null)
            {
                return HttpNotFound();
            }
            return View(working);
        }

        // POST: Workings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            Working working = db.Workings.Find(id);
            db.Workings.Remove(working);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Review(string ids)
        {

            string[] sids = ids.Split(',');

            // Get current supervisor
            Employee employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (employee != null)
            {

                string userId = User.Identity.GetUserId();

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    // User is Accountant or Superadmin, they can review everyone's timesheet
                    List<int> list = new List<int>();

                    foreach (var id in sids)
                    {
                        try
                        {
                            int i = Convert.ToInt32(id);
                            list.Add(i);
                        }
                        catch { }
                    }

                    var wlist = from work in db.Workings
                                where list.Contains(work.WorkingID)
                                select work;



                    foreach (Working work in wlist)
                    {
                        work.isReviewed = true;
                    }

                    db.SaveChanges();
                }
                else if (mUserInfo.isInRole(userId, "Employee"))
                {
                    // User is an employee, check if he is supervisor or not.

                    var result = from super in db.Supervisions
                                 join work in db.Workings
                                 on super.EmployeeID equals work.EmployeeID
                                 where super.SupervisorID == employee.EmployeeID
                                 select work;

                    foreach (string id in sids)
                    {
                        try
                        {
                            int i = Convert.ToInt32(id);
                            // check the input IDs whether are in employee IDs which are under supervised by this supervisor
                            foreach (var work in result)
                            {
                                if (work.WorkingID == i)
                                {
                                    work.isReviewed = true;
                                }
                            }

                            db.SaveChanges();

                        }
                        catch { }
                    }
                }
                else
                {
                    //Response.Write("unknown role");
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }

        private string generateExcel(IQueryable<Working> ws)
        {
            var timesheets = ws.ToList();

            var table = new System.Data.DataTable("teste");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("PPyr", typeof(int));
            table.Columns.Add("PP", typeof(int));
            table.Columns.Add("Client", typeof(string));
            table.Columns.Add("Project", typeof(string));
            table.Columns.Add("ProjectID", typeof(string));
            table.Columns.Add("Task", typeof(string));
            table.Columns.Add("Identifier", typeof(string));
            table.Columns.Add("Veh", typeof(string));
            table.Columns.Add("Crew", typeof(string));
            table.Columns.Add("StartKm", typeof(double));
            table.Columns.Add("EndKm", typeof(double));
            table.Columns.Add("GPS", typeof(bool));
            table.Columns.Add("Field", typeof(string));
            table.Columns.Add("PD", typeof(bool));
            table.Columns.Add("JobDescription", typeof(string));
            table.Columns.Add("Off", typeof(string));
            table.Columns.Add("Hours", typeof(double));
            table.Columns.Add("Bank", typeof(int));
            table.Columns.Add("OT", typeof(int));

            if (timesheets != null)
            {
                foreach (var t in timesheets)
                {
                    table.Rows.Add
                        (
                        t.Employee.FullName,
                        t.Date,
                        t.PPYr,
                        t.PP,
                        t.Project.FK_Client.ClientName,
                        t.Project.ProjectName,
                        t.Project.ProjectID,
                        t.FK_Task.TaskName,
                        t.Identifier,
                        t.FK_Vehicle.VehicleName,
                        t.Crew,
                        t.StartKm,
                        t.EndKm,
                        t.GPS,
                        t.FK_FieldAccess.FieldAccessName,
                        t.PD,
                        t.JobDescription,
                        t.FK_OffReason.OffReasonName,
                        t.Hours,
                        t.Bank,
                        t.OT
                        );

                }
            }




            var grid = new GridView();
            grid.DataSource = table;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=timesheets.xls");
            //Response.ContentType = "application/excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            //return Content(sw.ToString(), "application/ms-excel");
            return sw.ToString();
        }

        private void setEmployeeDropdowns()
        {
            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);

            if (employee != null)
            {
                IQueryable<Employee> es = db.Employees;

                if (!mUserInfo.isInRole(userId, "SUPERADMIN") && !mUserInfo.isInRole(userId, "Accountant"))
                {
                    es = es.Where(w => w.EmployeeID == employee.EmployeeID);
                }

                ViewBag.EmployeeID = new SelectList(es, "EmployeeID", "FullName");

            }
            else
            {
                ViewBag.EmployeeID = new SelectList(new List<Employee>(), "EmployeeID", "FullName");
            }
        }
    }
}
