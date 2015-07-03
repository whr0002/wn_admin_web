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
    public class SafetyCategoriesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyCategories
        public ActionResult Index()
        {
            return View(db.SafetyCategories.ToList());
        }

        // GET: SafetyCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyCategory safetyCategory = db.SafetyCategories.Find(id);
            if (safetyCategory == null)
            {
                return HttpNotFound();
            }
            return View(safetyCategory);
        }

        // GET: SafetyCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SafetyCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyCategoryID,SafetyCategoryName")] SafetyCategory safetyCategory)
        {
            if (ModelState.IsValid)
            {
                db.SafetyCategories.Add(safetyCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(safetyCategory);
        }

        // GET: SafetyCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyCategory safetyCategory = db.SafetyCategories.Find(id);
            if (safetyCategory == null)
            {
                return HttpNotFound();
            }
            return View(safetyCategory);
        }

        // POST: SafetyCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyCategoryID,SafetyCategoryName")] SafetyCategory safetyCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(safetyCategory);
        }

        // GET: SafetyCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyCategory safetyCategory = db.SafetyCategories.Find(id);
            if (safetyCategory == null)
            {
                return HttpNotFound();
            }
            return View(safetyCategory);
        }

        // POST: SafetyCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyCategory safetyCategory = db.SafetyCategories.Find(id);
            db.SafetyCategories.Remove(safetyCategory);
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
