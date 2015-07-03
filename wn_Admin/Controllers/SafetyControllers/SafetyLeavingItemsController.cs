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
    public class SafetyLeavingItemsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyLeavingItems
        public ActionResult Index()
        {
            return View(db.SafetyLeavingItems.ToList());
        }

        // GET: SafetyLeavingItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeavingItem safetyLeavingItem = db.SafetyLeavingItems.Find(id);
            if (safetyLeavingItem == null)
            {
                return HttpNotFound();
            }
            return View(safetyLeavingItem);
        }

        // GET: SafetyLeavingItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SafetyLeavingItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyLeavingItemID,SafetyLeavingItemName")] SafetyLeavingItem safetyLeavingItem)
        {
            if (ModelState.IsValid)
            {
                db.SafetyLeavingItems.Add(safetyLeavingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(safetyLeavingItem);
        }

        // GET: SafetyLeavingItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeavingItem safetyLeavingItem = db.SafetyLeavingItems.Find(id);
            if (safetyLeavingItem == null)
            {
                return HttpNotFound();
            }
            return View(safetyLeavingItem);
        }

        // POST: SafetyLeavingItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyLeavingItemID,SafetyLeavingItemName")] SafetyLeavingItem safetyLeavingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyLeavingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(safetyLeavingItem);
        }

        // GET: SafetyLeavingItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeavingItem safetyLeavingItem = db.SafetyLeavingItems.Find(id);
            if (safetyLeavingItem == null)
            {
                return HttpNotFound();
            }
            return View(safetyLeavingItem);
        }

        // POST: SafetyLeavingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyLeavingItem safetyLeavingItem = db.SafetyLeavingItems.Find(id);
            db.SafetyLeavingItems.Remove(safetyLeavingItem);
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
