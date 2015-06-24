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
    public class WorksForsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: WorksFors
        public ActionResult Index()
        {
            var worksFors = db.WorksFors.Include(w => w.Department).Include(w => w.Employee);
            return View(worksFors.ToList());
        }

        // GET: WorksFors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksFor worksFor = db.WorksFors.Find(id);
            if (worksFor == null)
            {
                return HttpNotFound();
            }
            return View(worksFor);
        }

        // GET: WorksFors/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            return View();
        }

        // POST: WorksFors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorksForID,EmployeeID,DepartmentID")] WorksFor worksFor)
        {
            if (ModelState.IsValid)
            {
                db.WorksFors.Add(worksFor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", worksFor.DepartmentID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", worksFor.EmployeeID);
            return View(worksFor);
        }

        // GET: WorksFors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksFor worksFor = db.WorksFors.Find(id);
            if (worksFor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", worksFor.DepartmentID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", worksFor.EmployeeID);
            return View(worksFor);
        }

        // POST: WorksFors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorksForID,EmployeeID,DepartmentID")] WorksFor worksFor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worksFor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", worksFor.DepartmentID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", worksFor.EmployeeID);
            return View(worksFor);
        }

        // GET: WorksFors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorksFor worksFor = db.WorksFors.Find(id);
            if (worksFor == null)
            {
                return HttpNotFound();
            }
            return View(worksFor);
        }

        // POST: WorksFors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorksFor worksFor = db.WorksFors.Find(id);
            db.WorksFors.Remove(worksFor);
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
