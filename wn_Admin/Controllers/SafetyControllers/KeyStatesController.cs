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
    public class KeyStatesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: KeyStates
        public ActionResult Index()
        {
            return View(db.KeyStates.ToList());
        }

        // GET: KeyStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyState keyState = db.KeyStates.Find(id);
            if (keyState == null)
            {
                return HttpNotFound();
            }
            return View(keyState);
        }

        // GET: KeyStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KeyStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyStateID,KeyStateName")] KeyState keyState)
        {
            if (ModelState.IsValid)
            {
                db.KeyStates.Add(keyState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keyState);
        }

        // GET: KeyStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyState keyState = db.KeyStates.Find(id);
            if (keyState == null)
            {
                return HttpNotFound();
            }
            return View(keyState);
        }

        // POST: KeyStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyStateID,KeyStateName")] KeyState keyState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyState);
        }

        // GET: KeyStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyState keyState = db.KeyStates.Find(id);
            if (keyState == null)
            {
                return HttpNotFound();
            }
            return View(keyState);
        }

        // POST: KeyStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyState keyState = db.KeyStates.Find(id);
            db.KeyStates.Remove(keyState);
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
