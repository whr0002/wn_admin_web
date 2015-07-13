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
    public class ImmeCausesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: ImmeCauses
        public ActionResult Index()
        {
            return View(db.ImmeCauses.ToList());
        }

        // GET: ImmeCauses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImmeCause immeCause = db.ImmeCauses.Find(id);
            if (immeCause == null)
            {
                return HttpNotFound();
            }
            return View(immeCause);
        }

        // GET: ImmeCauses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImmeCauses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImmeCauseID,ImmeCauseName")] ImmeCause immeCause)
        {
            if (ModelState.IsValid)
            {
                db.ImmeCauses.Add(immeCause);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(immeCause);
        }

        // GET: ImmeCauses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImmeCause immeCause = db.ImmeCauses.Find(id);
            if (immeCause == null)
            {
                return HttpNotFound();
            }
            return View(immeCause);
        }

        // POST: ImmeCauses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImmeCauseID,ImmeCauseName")] ImmeCause immeCause)
        {
            if (ModelState.IsValid)
            {
                db.Entry(immeCause).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(immeCause);
        }

        // GET: ImmeCauses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImmeCause immeCause = db.ImmeCauses.Find(id);
            if (immeCause == null)
            {
                return HttpNotFound();
            }
            return View(immeCause);
        }

        // POST: ImmeCauses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImmeCause immeCause = db.ImmeCauses.Find(id);
            db.ImmeCauses.Remove(immeCause);
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
