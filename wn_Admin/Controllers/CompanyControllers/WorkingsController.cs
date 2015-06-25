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

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles="SUPERADMIN, Accountant, Employee")]
    public class WorkingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo mUserInfo = new UserInfo();

        // GET: Workings
        public ActionResult Index(int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, int employeeId = -1, string projectId = null)
        {

            
            var workings = db.Workings.Include(w => w.Employee).Include(w => w.FK_FieldAccess).Include(w => w.FK_OffReason).Include(w => w.FK_Task).Include(w => w.FK_Vehicle).Include(w => w.Project);
            string userId = User.Identity.GetUserId();
            string role = mUserInfo.getFirstRole(userId);
            string currentFilter = "";
            


            if (!role.Equals("Accountant")) { 
                // User is not accountant, show records from this user
                var employee = mUserInfo.getEmployee(userId);
                if (employee != null)
                {
                    workings = workings.Where(w => w.EmployeeID == employee.EmployeeID);
                }
                else
                {
                    return View(new List<Working>());
                }

                ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == employee.EmployeeID), "EmployeeID", "FullName");   
                ViewBag.clientId = new SelectList(workings.Select(s => s.Project.FK_Client).Distinct(), "ClientID", "ClientName");
                ViewBag.projectId = new SelectList(workings.Select(s => s.Project).Distinct(), "ProjectID", "ProjectName");

            }else{

                // User is an accoutant, he or she can search Timesheets by Employee ID

                if(employeeId != -1){
                    workings = workings.Where(w => w.EmployeeID == employeeId);
                    currentFilter += "&employeeId=" + employeeId;
                }

                ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
                ViewBag.clientId = new SelectList(db.Clients, "ClientID", "ClientName");
                ViewBag.projectId = new SelectList(db.Projects, "ProjectID", "ProjectName");
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

            ViewBag.CurrentFilter = currentFilter;
            workings = workings.OrderByDescending(o => o.Date);

            int pageSize = 15;
            int pageNumber = (page ?? 1);


            return View(workings.ToPagedList(pageNumber, pageSize));
        }

        // GET: Workings/Details/5
        [Authorize(Roles="Accountant")]
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
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName");
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");


            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);
            var role = mUserInfo.getFirstRole(userId);


            Working working = new Working();

            if (employee != null)
            {
                IQueryable<Employee> es = db.Employees;

                if (!role.Equals("Accountant"))
                {
                    es = es.Where(w => w.EmployeeID == employee.EmployeeID);
                }



                ViewBag.EmployeeID = new SelectList(es, "EmployeeID", "FullName");

            }
            else
            {
                ViewBag.EmployeeID = new SelectList(new List<Employee>(), "EmployeeID", "FullName");
            }
            

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
                db.Workings.Add(working);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName", working.Field);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Edit/5
        [Authorize(Roles = "Accountant")]
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
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
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
        [Authorize(Roles = "Accountant")]
        public ActionResult Edit([Bind(Include = "WorkingID,EmployeeID,Date,PPYr,PP,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,OffReason,Hours,Bank,OT")] Working working)
        {
            if (ModelState.IsValid)
            {
                db.Entry(working).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientName = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessName", working.Field);
            ViewBag.OffReason = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", working.OffReason);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskName", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleName", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Delete/5
        [Authorize(Roles = "Accountant")]
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
        [Authorize(Roles = "Accountant")]
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
    }
}
