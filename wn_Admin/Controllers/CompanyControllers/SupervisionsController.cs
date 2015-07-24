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
    [Authorize(Roles = "SUPERADMIN, Accountant")]
    public class SupervisionsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Supervisions
        public ActionResult Index()
        {
            var supervisions = db.Supervisions.Include(s => s.Employee).Include(s => s.Supervisor);
            return View(supervisions.ToList());
        }

        // GET: Supervisions/Details/5
        public ActionResult Details(int? eid, int? mid, string pid)
        {
            if (eid == null || mid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervision supervision = db.Supervisions.Find(eid, mid, pid);
            if (supervision == null)
            {
                return HttpNotFound();
            }
            return View(supervision);
        }

        // GET: Supervisions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.SupervisorID = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View();
        }

        // POST: Supervisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,SupervisorID,ProjectID,StartDate,EndDate")] Supervision supervision)
        {
            if (ModelState.IsValid)
            {
                db.Supervisions.Add(supervision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.EmployeeID);
            ViewBag.SupervisorID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.SupervisorID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View(supervision);
        }

        // GET: Supervisions/Edit/5
        public ActionResult Edit(int? eid, int? mid, string pid)
        {
            if (eid == null || mid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervision supervision = db.Supervisions.Find(eid, mid, pid);
            if (supervision == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.EmployeeID);
            ViewBag.SupervisorID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.SupervisorID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View(supervision);
        }

        // POST: Supervisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int oldEmployeeID, int oldSupervisorID, string oldProjectID, [Bind(Include = "EmployeeID,SupervisorID,ProjectID,StartDate,EndDate")] Supervision supervision)
        {
            if (ModelState.IsValid)
            {
                Supervision s = db.Supervisions.Find(oldEmployeeID, oldSupervisorID, oldProjectID);
                db.Supervisions.Remove(s);
                db.Supervisions.Add(supervision);
                //db.Entry(supervision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeToID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.EmployeeID);
            ViewBag.SupervisorToID = new SelectList(db.Employees, "EmployeeID", "FullName", supervision.SupervisorID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View(supervision);
        }

        // GET: Supervisions/Delete/5
        public ActionResult Delete(int? eid, int? mid, string pid)
        {
            if (eid == null || mid == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervision supervision = db.Supervisions.Find(eid, mid, pid);
            if (supervision == null)
            {
                return HttpNotFound();
            }
            return View(supervision);
        }

        // POST: Supervisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int eid, int mid, string pid)
        {
            Supervision supervision = db.Supervisions.Find(eid, mid, pid);
            db.Supervisions.Remove(supervision);
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
