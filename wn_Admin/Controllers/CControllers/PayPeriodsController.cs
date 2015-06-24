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
    public class PayPeriodsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: PayPeriods
        public ActionResult Index()
        {
            return View(db.PayPeriods.ToList());
        }

        // GET: PayPeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayPeriod payPeriod = db.PayPeriods.Find(id);
            if (payPeriod == null)
            {
                return HttpNotFound();
            }
            return View(payPeriod);
        }

        // GET: PayPeriods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayPeriodID,StartDate,isCurrent")] PayPeriod payPeriod)
        {
            if (ModelState.IsValid)
            {
                db.PayPeriods.Add(payPeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payPeriod);
        }

        // GET: PayPeriods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayPeriod payPeriod = db.PayPeriods.Find(id);
            if (payPeriod == null)
            {
                return HttpNotFound();
            }
            return View(payPeriod);
        }

        // POST: PayPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayPeriodID,StartDate,isCurrent")] PayPeriod payPeriod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payPeriod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payPeriod);
        }

        // GET: PayPeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayPeriod payPeriod = db.PayPeriods.Find(id);
            if (payPeriod == null)
            {
                return HttpNotFound();
            }
            return View(payPeriod);
        }

        // POST: PayPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayPeriod payPeriod = db.PayPeriods.Find(id);
            db.PayPeriods.Remove(payPeriod);
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
