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
    public class AccidentTypesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: AccidentTypes
        public ActionResult Index()
        {
            var accidentTypes = db.AccidentTypes.Include(a => a.MajorAccidentForm);
            return View(accidentTypes.ToList());
        }

        // GET: AccidentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentType accidentType = db.AccidentTypes.Find(id);
            if (accidentType == null)
            {
                return HttpNotFound();
            }
            return View(accidentType);
        }

        // GET: AccidentTypes/Create
        public ActionResult Create()
        {
            ViewBag.MajorAccidentFormID = new SelectList(db.MajorAccidentForms, "MajorAccidentFormID", "Name");
            return View();
        }

        // POST: AccidentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccidentTypeID,AccidentTypeName,MajorAccidentFormID")] AccidentType accidentType)
        {
            if (ModelState.IsValid)
            {
                db.AccidentTypes.Add(accidentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorAccidentFormID = new SelectList(db.MajorAccidentForms, "MajorAccidentFormID", "Name", accidentType.MajorAccidentFormID);
            return View(accidentType);
        }

        // GET: AccidentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentType accidentType = db.AccidentTypes.Find(id);
            if (accidentType == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorAccidentFormID = new SelectList(db.MajorAccidentForms, "MajorAccidentFormID", "Name", accidentType.MajorAccidentFormID);
            return View(accidentType);
        }

        // POST: AccidentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccidentTypeID,AccidentTypeName,MajorAccidentFormID")] AccidentType accidentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accidentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorAccidentFormID = new SelectList(db.MajorAccidentForms, "MajorAccidentFormID", "Name", accidentType.MajorAccidentFormID);
            return View(accidentType);
        }

        // GET: AccidentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentType accidentType = db.AccidentTypes.Find(id);
            if (accidentType == null)
            {
                return HttpNotFound();
            }
            return View(accidentType);
        }

        // POST: AccidentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccidentType accidentType = db.AccidentTypes.Find(id);
            db.AccidentTypes.Remove(accidentType);
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
