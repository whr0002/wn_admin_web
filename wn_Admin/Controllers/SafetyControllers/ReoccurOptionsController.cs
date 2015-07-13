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
    public class ReoccurOptionsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: ReoccurOptions
        public ActionResult Index()
        {
            return View(db.ReoccurOptions.ToList());
        }

        // GET: ReoccurOptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReoccurOption reoccurOption = db.ReoccurOptions.Find(id);
            if (reoccurOption == null)
            {
                return HttpNotFound();
            }
            return View(reoccurOption);
        }

        // GET: ReoccurOptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReoccurOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReoccurOptionID,ReoccurOptionName")] ReoccurOption reoccurOption)
        {
            if (ModelState.IsValid)
            {
                db.ReoccurOptions.Add(reoccurOption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reoccurOption);
        }

        // GET: ReoccurOptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReoccurOption reoccurOption = db.ReoccurOptions.Find(id);
            if (reoccurOption == null)
            {
                return HttpNotFound();
            }
            return View(reoccurOption);
        }

        // POST: ReoccurOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReoccurOptionID,ReoccurOptionName")] ReoccurOption reoccurOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reoccurOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reoccurOption);
        }

        // GET: ReoccurOptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReoccurOption reoccurOption = db.ReoccurOptions.Find(id);
            if (reoccurOption == null)
            {
                return HttpNotFound();
            }
            return View(reoccurOption);
        }

        // POST: ReoccurOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReoccurOption reoccurOption = db.ReoccurOptions.Find(id);
            db.ReoccurOptions.Remove(reoccurOption);
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
