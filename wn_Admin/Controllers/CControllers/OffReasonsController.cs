using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CModels;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant")]
    public class OffReasonsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: OffReasons
        public ActionResult Index()
        {
            return View(db.OffReasons.ToList());
        }

        // GET: OffReasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffReason offReason = db.OffReasons.Find(id);
            if (offReason == null)
            {
                return HttpNotFound();
            }
            return View(offReason);
        }

        // GET: OffReasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffReasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OffReasonID,OffReasonName")] OffReason offReason)
        {
            if (ModelState.IsValid)
            {
                db.OffReasons.Add(offReason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offReason);
        }

        // GET: OffReasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffReason offReason = db.OffReasons.Find(id);
            if (offReason == null)
            {
                return HttpNotFound();
            }
            return View(offReason);
        }

        // POST: OffReasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OffReasonID,OffReasonName")] OffReason offReason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offReason).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offReason);
        }

        // GET: OffReasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OffReason offReason = db.OffReasons.Find(id);
            if (offReason == null)
            {
                return HttpNotFound();
            }
            return View(offReason);
        }

        // POST: OffReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OffReason offReason = db.OffReasons.Find(id);
            db.OffReasons.Remove(offReason);
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
