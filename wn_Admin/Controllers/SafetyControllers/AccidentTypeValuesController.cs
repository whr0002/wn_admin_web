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
    public class AccidentTypeValuesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: AccidentTypeValues
        public ActionResult Index()
        {
            return View(db.AccidentTypeValues.ToList());
        }

        // GET: AccidentTypeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTypeValue accidentTypeValue = db.AccidentTypeValues.Find(id);
            if (accidentTypeValue == null)
            {
                return HttpNotFound();
            }
            return View(accidentTypeValue);
        }

        // GET: AccidentTypeValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccidentTypeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccidentTypeValueID,AccidentTypeValueName")] AccidentTypeValue accidentTypeValue)
        {
            if (ModelState.IsValid)
            {
                db.AccidentTypeValues.Add(accidentTypeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accidentTypeValue);
        }

        // GET: AccidentTypeValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTypeValue accidentTypeValue = db.AccidentTypeValues.Find(id);
            if (accidentTypeValue == null)
            {
                return HttpNotFound();
            }
            return View(accidentTypeValue);
        }

        // POST: AccidentTypeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccidentTypeValueID,AccidentTypeValueName")] AccidentTypeValue accidentTypeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accidentTypeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accidentTypeValue);
        }

        // GET: AccidentTypeValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentTypeValue accidentTypeValue = db.AccidentTypeValues.Find(id);
            if (accidentTypeValue == null)
            {
                return HttpNotFound();
            }
            return View(accidentTypeValue);
        }

        // POST: AccidentTypeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccidentTypeValue accidentTypeValue = db.AccidentTypeValues.Find(id);
            db.AccidentTypeValues.Remove(accidentTypeValue);
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
