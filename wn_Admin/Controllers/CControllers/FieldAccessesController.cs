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
    public class FieldAccessesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: FieldAccesses
        public ActionResult Index()
        {
            return View(db.FieldAccesses.ToList());
        }

        // GET: FieldAccesses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldAccess fieldAccess = db.FieldAccesses.Find(id);
            if (fieldAccess == null)
            {
                return HttpNotFound();
            }
            return View(fieldAccess);
        }

        // GET: FieldAccesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FieldAccesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FieldAccessID")] FieldAccess fieldAccess)
        {
            if (ModelState.IsValid)
            {
                db.FieldAccesses.Add(fieldAccess);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fieldAccess);
        }

        // GET: FieldAccesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldAccess fieldAccess = db.FieldAccesses.Find(id);
            if (fieldAccess == null)
            {
                return HttpNotFound();
            }
            return View(fieldAccess);
        }

        // POST: FieldAccesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FieldAccessID")] FieldAccess fieldAccess)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fieldAccess).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fieldAccess);
        }

        // GET: FieldAccesses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldAccess fieldAccess = db.FieldAccesses.Find(id);
            if (fieldAccess == null)
            {
                return HttpNotFound();
            }
            return View(fieldAccess);
        }

        // POST: FieldAccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FieldAccess fieldAccess = db.FieldAccesses.Find(id);
            db.FieldAccesses.Remove(fieldAccess);
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
