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
    public class EmployeeSupervisionsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: EmployeeSupervisions
        public ActionResult Index()
        {
            var employeeSupervisions = db.EmployeeSupervisions.Include(e => e.Employee).Include(e => e.Supervisor);
            return View(employeeSupervisions.ToList());
        }

        // GET: EmployeeSupervisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSupervision employeeSupervision = db.EmployeeSupervisions.Find(id);
            if (employeeSupervision == null)
            {
                return HttpNotFound();
            }
            return View(employeeSupervision);
        }

        // GET: EmployeeSupervisions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName");
            ViewBag.SupervisorID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName");
            return View();
        }

        // POST: EmployeeSupervisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeSupervisionID,SupervisorID,EmployeeID")] EmployeeSupervision employeeSupervision)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeSupervisions.Add(employeeSupervision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.EmployeeID);
            ViewBag.SupervisorID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.SupervisorID);
            return View(employeeSupervision);
        }

        // GET: EmployeeSupervisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSupervision employeeSupervision = db.EmployeeSupervisions.Find(id);
            if (employeeSupervision == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.EmployeeID);
            ViewBag.SupervisorID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.SupervisorID);
            return View(employeeSupervision);
        }

        // POST: EmployeeSupervisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeSupervisionID,SupervisorID,EmployeeID")] EmployeeSupervision employeeSupervision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSupervision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.EmployeeID);
            ViewBag.SupervisorID = new SelectList(db.Employees.Where(w => w.Status == 1), "EmployeeID", "FirstMidName", employeeSupervision.SupervisorID);
            return View(employeeSupervision);
        }

        // GET: EmployeeSupervisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSupervision employeeSupervision = db.EmployeeSupervisions.Find(id);
            if (employeeSupervision == null)
            {
                return HttpNotFound();
            }
            return View(employeeSupervision);
        }

        // POST: EmployeeSupervisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSupervision employeeSupervision = db.EmployeeSupervisions.Find(id);
            db.EmployeeSupervisions.Remove(employeeSupervision);
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
