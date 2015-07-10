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
    [Authorize(Roles = "SUPERADMIN")]
    public class SafetyItemValuesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyItemValues
        public ActionResult Index()
        {
            var safetyItemValues = db.SafetyItemValues.Include(s => s.SafetyCategory);
            return View(safetyItemValues.ToList());
        }

        // GET: SafetyItemValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyItemValue safetyItemValue = db.SafetyItemValues.Find(id);
            if (safetyItemValue == null)
            {
                return HttpNotFound();
            }
            return View(safetyItemValue);
        }

        // GET: SafetyItemValues/Create
        public ActionResult Create()
        {
            ViewBag.SafetyCategoryID = new SelectList(db.SafetyCategories, "SafetyCategoryID", "SafetyCategoryName");
            return View();
        }

        // POST: SafetyItemValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyItemValueID,SafetyItemValueName,SafetyCategoryID")] SafetyItemValue safetyItemValue)
        {
            if (ModelState.IsValid)
            {
                db.SafetyItemValues.Add(safetyItemValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SafetyCategoryID = new SelectList(db.SafetyCategories, "SafetyCategoryID", "SafetyCategoryName", safetyItemValue.SafetyCategoryID);
            return View(safetyItemValue);
        }

        // GET: SafetyItemValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyItemValue safetyItemValue = db.SafetyItemValues.Find(id);
            if (safetyItemValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.SafetyCategoryID = new SelectList(db.SafetyCategories, "SafetyCategoryID", "SafetyCategoryName", safetyItemValue.SafetyCategoryID);
            return View(safetyItemValue);
        }

        // POST: SafetyItemValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyItemValueID,SafetyItemValueName,SafetyCategoryID")] SafetyItemValue safetyItemValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyItemValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SafetyCategoryID = new SelectList(db.SafetyCategories, "SafetyCategoryID", "SafetyCategoryName", safetyItemValue.SafetyCategoryID);
            return View(safetyItemValue);
        }

        // GET: SafetyItemValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyItemValue safetyItemValue = db.SafetyItemValues.Find(id);
            if (safetyItemValue == null)
            {
                return HttpNotFound();
            }
            return View(safetyItemValue);
        }

        // POST: SafetyItemValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyItemValue safetyItemValue = db.SafetyItemValues.Find(id);
            db.SafetyItemValues.Remove(safetyItemValue);
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
