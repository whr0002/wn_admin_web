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
    public class MinorAccidentFormsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: MinorAccidentForms
        public ActionResult Index()
        {
            var minorAccidentForms = db.MinorAccidentForms.Include(m => m.Employee);
            return View(minorAccidentForms.ToList());
        }

        // GET: MinorAccidentForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorAccidentForm minorAccidentForm = db.MinorAccidentForms.Find(id);
            if (minorAccidentForm == null)
            {
                return HttpNotFound();
            }
            return View(minorAccidentForm);
        }

        // GET: MinorAccidentForms/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            return View();
        }

        // POST: MinorAccidentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MinorAccidentFormID,EmployeeID,Position,DateReported,LocationOfEvent,TaskConducted,AccidentType,RelatingTo,EventDesc,CauseAnalysis,KeyStates,CriticalErrors,FreqExpo,HazardProb,FirstAid,FirstAidDesc,CorrAction,PersonRespCorrAct,CorrActCompDate,FurtherActReq,isReviewed")] MinorAccidentForm minorAccidentForm)
        {
            if (ModelState.IsValid)
            {
                db.MinorAccidentForms.Add(minorAccidentForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", minorAccidentForm.EmployeeID);
            return View(minorAccidentForm);
        }

        // GET: MinorAccidentForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorAccidentForm minorAccidentForm = db.MinorAccidentForms.Find(id);
            if (minorAccidentForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", minorAccidentForm.EmployeeID);
            return View(minorAccidentForm);
        }

        // POST: MinorAccidentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MinorAccidentFormID,EmployeeID,Position,DateReported,LocationOfEvent,TaskConducted,AccidentType,RelatingTo,EventDesc,CauseAnalysis,KeyStates,CriticalErrors,FreqExpo,HazardProb,FirstAid,FirstAidDesc,CorrAction,PersonRespCorrAct,CorrActCompDate,FurtherActReq,isReviewed")] MinorAccidentForm minorAccidentForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(minorAccidentForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", minorAccidentForm.EmployeeID);
            return View(minorAccidentForm);
        }

        // GET: MinorAccidentForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorAccidentForm minorAccidentForm = db.MinorAccidentForms.Find(id);
            if (minorAccidentForm == null)
            {
                return HttpNotFound();
            }
            return View(minorAccidentForm);
        }

        // POST: MinorAccidentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MinorAccidentForm minorAccidentForm = db.MinorAccidentForms.Find(id);
            db.MinorAccidentForms.Remove(minorAccidentForm);
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
