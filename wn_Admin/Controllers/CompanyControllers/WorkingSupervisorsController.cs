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
    public class WorkingSupervisorsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: WorkingSupervisors
        public ActionResult Index()
        {
            var workingSupervisors = db.WorkingSupervisors.Include(w => w.Employee).Include(w => w.Working);
            return View(workingSupervisors.ToList());
        }

        // GET: WorkingSupervisors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingSupervisor workingSupervisor = db.WorkingSupervisors.Find(id);
            if (workingSupervisor == null)
            {
                return HttpNotFound();
            }
            return View(workingSupervisor);
        }

        // GET: WorkingSupervisors/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            ViewBag.WorkingID = new SelectList(db.Workings, "WorkingID", "WorkingID");
            return View();
        }

        // POST: WorkingSupervisors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingSupervisorID,WorkingID,EmployeeID")] WorkingSupervisor workingSupervisor)
        {
            if (ModelState.IsValid)
            {
                db.WorkingSupervisors.Add(workingSupervisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", workingSupervisor.EmployeeID);
            ViewBag.WorkingID = new SelectList(db.Workings, "WorkingID", "WorkingID", workingSupervisor.WorkingID);
            return View(workingSupervisor);
        }

        // GET: WorkingSupervisors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingSupervisor workingSupervisor = db.WorkingSupervisors.Find(id);
            if (workingSupervisor == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", workingSupervisor.EmployeeID);
            ViewBag.WorkingID = new SelectList(db.Workings, "WorkingID", "WorkingID", workingSupervisor.WorkingID);
            return View(workingSupervisor);
        }

        // POST: WorkingSupervisors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkingSupervisorID,WorkingID,EmployeeID")] WorkingSupervisor workingSupervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workingSupervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", workingSupervisor.EmployeeID);
            ViewBag.WorkingID = new SelectList(db.Workings, "WorkingID", "WorkingID", workingSupervisor.WorkingID);
            return View(workingSupervisor);
        }

        // GET: WorkingSupervisors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingSupervisor workingSupervisor = db.WorkingSupervisors.Find(id);
            if (workingSupervisor == null)
            {
                return HttpNotFound();
            }
            return View(workingSupervisor);
        }

        // POST: WorkingSupervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkingSupervisor workingSupervisor = db.WorkingSupervisors.Find(id);
            db.WorkingSupervisors.Remove(workingSupervisor);
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
