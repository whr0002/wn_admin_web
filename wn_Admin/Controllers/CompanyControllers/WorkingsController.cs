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
        public ActionResult Index(int[] employeeId, int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, string projectId = null, Boolean? exportToExcel = null)
        {


            var workings = db.Workings.Include(w => w.Employee).Include(w => w.FK_OffReason).Include(w => w.FK_Task).Include(w => w.Project);
            string userId = User.Identity.GetUserId();
            string currentFilter = "";
            string dateRange = "";

            if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
            {

                // User is an accoutant, he or she can search Timesheets by Employee ID

                if (employeeId != null && employeeId.Count() > 0)
                {
                    workings = workings.Where(w => employeeId.Contains(w.EmployeeID));
                    currentFilter += "&employeeId=" + string.Join("&employeeId=", employeeId);
                }
                ViewBag.hasReviewControl = true;
                ViewBag.hasFullControl = true;
                //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");

                ViewBag.EmployeeID = new MultiSelectList(db.Employees, "EmployeeID", "FullName");
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

                    var pids = db.Supervisions.Where(w => w.SupervisorID == employee.EmployeeID).Select(s => s.ProjectID);


                    workings = workings.Where(w => w.EmployeeID == employee.EmployeeID || (ids.Contains(w.EmployeeID) && pids.Contains(w.ProjectID) && w.isReviewed == false));

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
                //ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == employee.EmployeeID), "EmployeeID", "FullName");
                ViewBag.EmployeeID = new MultiSelectList(db.Employees.Where(w => w.EmployeeID == employee.EmployeeID), "EmployeeID", "FullName");
                ViewBag.clientId = new SelectList(workings.Select(s => s.Project.FK_Client).Distinct(), "ClientID", "ClientName");
                ViewBag.projectId = new SelectList(workings.Select(s => s.Project).Distinct(), "ProjectID", "ProjectName");

            }

            // Search Filters

            // Date
            if (startDate != null)
            {
                workings = workings.Where(w => w.Date >= startDate || w.EndDate >= startDate);
                currentFilter += "&startDate=" + String.Format("{0:yyyy-MM-dd}", startDate);

                dateRange = "From " + String.Format("{0:yyyy-MM-dd}", startDate);
            }

            if (endDate != null)
            {
                workings = workings.Where(w => w.Date <= endDate || w.EndDate <= endDate);
                currentFilter += "&endDate=" + String.Format("{0:yyyy-MM-dd}", endDate);

                dateRange += " To " + String.Format("{0:yyyy-MM-dd}", endDate);
            }

            // Get working records within 2 months
            if (startDate == null && endDate == null)
            {
                DateTime now = DateTime.Now;
                DateTime preMonth = now.AddMonths(-2);
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

            // Group by Date for events
            var events = workings
                .GroupBy(g => new{g.EmployeeID, g.Employee.FullName, g.Date, g.EndDate})
                .Select(s => new EventModel
                {
                    title = s.Key.FullName, 
                    totalHours = s.Sum(b => b.Hours),
                    startDT = s.Key.Date,
                    endDT = s.Key.EndDate
                })
                .ToList();


            // Giving each event color and description.
            List<EventViewModel> evm = new List<EventViewModel>();

            foreach (var e in events)
            {
                EventViewModel eventViewModel = new EventViewModel();
                eventViewModel.start = e.startDT.ToString("yyyy-MM-dd");
                eventViewModel.end = e.endDT.AddDays(1).ToString("yyyy-MM-dd");
                eventViewModel.backgroundColor = (e.totalHours == 0) ? "#004C99" : "#808080";

                //e.start = e.startDT.ToString("yyyy-MM-dd");
                //e.backgroundColor = (e.totalHours == 0) ? "#004C99" : "#808080";
                if (e.totalHours == 0)
                {
                    e.title += ": Off";
                }
                else
                {
                    e.title += ": " + e.totalHours + " h";
                }

                eventViewModel.title = e.title;

                evm.Add(eventViewModel);
            }

            // Generate time sheet summary
            var tempE = mUserInfo.getEmployee(userId);

            ViewBag.TMSummary = workings.Where(w => w.EmployeeID == tempE.EmployeeID).GroupBy(g => new {g.EmployeeID, g.Employee.FullName}).Select(
                s => new TimesheetSummaryViewModel
                {
                    EmployeeName = s.Key.FullName,
                    TotalHours = s.Sum(b => b.Hours),
                    DateRange = dateRange
                }).ToList();
            

            // Pass events to view
            ViewBag.Events = evm;


            return View(workings.ToList());
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
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessName", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleName", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects.Where(w => w.Status == 1), "ProjectID", "ProjectName");
            ViewBag.Equipment = new MultiSelectList(db.Equipments, "EquipmentName", "EquipmentName");

            setEmployeeDropdowns();

            Working working = new Working();

            working.Date = DateTime.Today;
            working.EndDate = DateTime.Today;
            PayPeriodCalculator calc = new PayPeriodCalculator();
            PPViewModel ppv = calc.getPayPeriod(working.Date);
            working.PPYr = ppv.PPYear;
            working.PP = ppv.PPNumber;
            //working.OffReason = 2;

            return View(working);
        }

        public ActionResult CreateOff()
        {
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            setEmployeeDropdowns();

            Working working = new Working();

            working.Date = DateTime.Today;
            working.EndDate = working.Date;
            PayPeriodCalculator calc = new PayPeriodCalculator();
            PPViewModel ppv = calc.getPayPeriod(working.Date);
            working.PPYr = ppv.PPYear;
            working.PP = ppv.PPNumber;

            working.ProjectID = "0-0-2015";
            working.Task = db.Tasks.Where(w => w.TaskName.Equals("None")).FirstOrDefault().TaskID;


            return View(working);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOff([Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP, ProjectID,Task, JobDescription,OffReason")] Working working)
        {
            if (ModelState.IsValid)
            {
                if(working.EndDate >= working.Date){

                    db.Workings.Add(working);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("EndDate", "'Date To' must be later than 'Date From'");
                }

            }

            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            setEmployeeDropdowns();
            return View(working);
        }

        // POST: Workings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours")] Working working)
        {


            if (ModelState.IsValid)
            {

                // Check date to see if it is valid.
                string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date);
                string userId = User.Identity.GetUserId();

                // For working days, set end date to start date
                working.EndDate = working.Date;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void ajaxCreate(string[] Equipment, [Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,Field,PD,JobDescription,OffReason,Hours")] Working working)
        {


            if (ModelState.IsValid)
            {
                // For working days, set end date to start date
                working.EndDate = working.Date;

                // Check date to see if it is valid.
                string validationError = TimesheetDateValidator.ValidateTimesheetDateRange(working.Date);
                string userId = User.Identity.GetUserId();
                
                // Combine equipments
                if (Equipment != null)
                {
                    string equipments = string.Join(", ", Equipment);
                    working.Equipment = equipments;
                }

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    db.Workings.Add(working);
                    db.SaveChanges();
                    Response.Write("valid");

                }
                else
                {
                    if (validationError != null)
                    {
                        Response.Write(validationError);
                        //ModelState.AddModelError("Date", validationError);
                    }
                    else
                    {
                        db.Workings.Add(working);
                        db.SaveChanges();
                        Response.Write("valid");
                    }

                }
               

            }
            else
            {
                var errors = string.Join("<br />", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).Select(s => s.ErrorMessage).ToArray());

                Response.Write(errors);
            }

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
        public ActionResult Edit([Bind(Include = "WorkingID,EmployeeID,Date,EndDate,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours,Bank,OT")] Working working)
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


        public ActionResult Review([ModelBinder(typeof(IntArrayModelBinder))] int[] ids)
        {

            //string[] sids = ids.Split(',');

            // Get current supervisor
            Employee employee = mUserInfo.getEmployee(User.Identity.GetUserId());
            if (employee != null)
            {

                string userId = User.Identity.GetUserId();

                if (mUserInfo.isInRole(userId, "SUPERADMIN") || mUserInfo.isInRole(userId, "Accountant"))
                {
                    // User is Accountant or Superadmin, they can review everyone's timesheet
                    //List<int> list = new List<int>();

                    //foreach (var id in sids)
                    //{
                    //    try
                    //    {
                    //        int i = Convert.ToInt32(id);
                    //        list.Add(i);
                    //    }
                    //    catch { }
                    //}

                    var wlist = from work in db.Workings
                                where ids.Contains(work.WorkingID)
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

                    foreach (var work in result)
                    {
                        if (ids.Contains(work.WorkingID))
                        {
                            work.isReviewed = true;
                        }
                    }

                    db.SaveChanges();

                    //foreach (string id in sids)
                    //{
                    //    try
                    //    {
                    //        int i = Convert.ToInt32(id);
                    //        // check the input IDs whether are in employee IDs which are under supervised by this supervisor
                    //        foreach (var work in result)
                    //        {
                    //            if (work.WorkingID == i)
                    //            {
                    //                work.isReviewed = true;
                    //            }
                    //        }

                    //        db.SaveChanges();

                    //    }
                    //    catch { }
                    //}
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
            table.Columns.Add("End Date", typeof(DateTime));
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
            table.Columns.Add("KmDiff", typeof(double));
            table.Columns.Add("Equipments", typeof(string));
            table.Columns.Add("Field", typeof(string));
            table.Columns.Add("PD", typeof(bool));
            table.Columns.Add("JobDescription", typeof(string));
            table.Columns.Add("Off", typeof(string));
            table.Columns.Add("Hours", typeof(double));


            if (timesheets != null)
            {
                foreach (var t in timesheets)
                {
                    table.Rows.Add
                        (
                        t.Employee.FullName,
                        t.Date,
                        t.EndDate,
                        t.PPYr,
                        t.PP,
                        t.Project.FK_Client.ClientName,
                        t.Project.ProjectName,
                        t.Project.ProjectID,
                        t.FK_Task.TaskName,
                        t.Identifier,
                        t.Veh,
                        t.Crew,
                        t.StartKm,
                        t.EndKm,
                        t.KmDiff,
                        t.Equipment,
                        t.Field,
                        t.PD,
                        t.JobDescription,
                        t.FK_OffReason.OffReasonName,
                        t.Hours
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

        public ActionResult demo()
        {
            return View();
        }
    }
}
