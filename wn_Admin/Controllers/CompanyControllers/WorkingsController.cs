using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CompanyModels;

namespace wn_Admin.Controllers.CompanyControllers
{
    public class WorkingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Workings
        public ActionResult Index()
        {
            var workings = db.Workings.Include(w => w.Employee).Include(w => w.FK_FieldAccess).Include(w => w.FK_Off).Include(w => w.FK_Task).Include(w => w.FK_Vehicle).Include(w => w.Project);
            return View(workings.ToList());
        }

        // GET: Workings/Details/5
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
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessID");
            ViewBag.Off = new SelectList(db.Tasks, "TaskID", "TaskID");
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskID");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleID");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View();
        }

        // POST: Workings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingID,Date,EmployeeID,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,Off,Hours,Bank,OT")] Working working)
        {
            if (ModelState.IsValid)
            {
                db.Workings.Add(working);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessID", working.Field);
            ViewBag.Off = new SelectList(db.Tasks, "TaskID", "TaskID", working.Off);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskID", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleID", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Edit/5
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
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessID", working.Field);
            ViewBag.Off = new SelectList(db.Tasks, "TaskID", "TaskID", working.Off);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskID", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleID", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // POST: Workings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkingID,Date,EmployeeID,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,Off,Hours,Bank,OT")] Working working)
        {
            if (ModelState.IsValid)
            {
                db.Entry(working).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", working.EmployeeID);
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessID", working.Field);
            ViewBag.Off = new SelectList(db.Tasks, "TaskID", "TaskID", working.Off);
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskID", working.Task);
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleID", working.Veh);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", working.ProjectID);
            return View(working);
        }

        // GET: Workings/Delete/5
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
