using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.Safety;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class SafetyObservationsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyObservations
        public ActionResult Index()
        {
            var safetyObservations = db.SafetyObservations.Include(s => s.Employee);
            return View(safetyObservations.ToList());
        }

        // GET: SafetyObservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyObservation safetyObservation = db.SafetyObservations.Find(id);
            if (safetyObservation == null)
            {
                return HttpNotFound();
            }
            return View(safetyObservation);
        }

        // GET: SafetyObservations/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            ViewBag.Observation = new MultiSelectList(db.Observations, "ObservationName", "ObservationName");
            return View();
        }

        // POST: SafetyObservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyObservationID,EmployeeID,Crew,Date,Location,Observation,Description,Investigation,KeyState,CriticalError,Solution,DateAction,Status")] SafetyObservation safetyObservation)
        {
            if (ModelState.IsValid)
            {
                db.SafetyObservations.Add(safetyObservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", safetyObservation.EmployeeID);
            ViewBag.Observation = new MultiSelectList(db.Observations, "ObservationName", "ObservationName");
            return View(safetyObservation);
        }

        // GET: SafetyObservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyObservation safetyObservation = db.SafetyObservations.Find(id);
            if (safetyObservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", safetyObservation.EmployeeID);
            return View(safetyObservation);
        }

        // POST: SafetyObservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyObservationID,EmployeeID,Crew,Date,Location,Observation,Description,Investigation,KeyState,CriticalError,Solution,DateAction,Status")] SafetyObservation safetyObservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyObservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", safetyObservation.EmployeeID);
            return View(safetyObservation);
        }

        // GET: SafetyObservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyObservation safetyObservation = db.SafetyObservations.Find(id);
            if (safetyObservation == null)
            {
                return HttpNotFound();
            }
            return View(safetyObservation);
        }

        // POST: SafetyObservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyObservation safetyObservation = db.SafetyObservations.Find(id);
            db.SafetyObservations.Remove(safetyObservation);
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
