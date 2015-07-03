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
    public class YesNoNAsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: YesNoNAs
        public ActionResult Index()
        {
            return View(db.YesNoNA.ToList());
        }

        // GET: YesNoNAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesNoNA yesNoNA = db.YesNoNA.Find(id);
            if (yesNoNA == null)
            {
                return HttpNotFound();
            }
            return View(yesNoNA);
        }

        // GET: YesNoNAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YesNoNAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YesNoNAID,YesNoNAName")] YesNoNA yesNoNA)
        {
            if (ModelState.IsValid)
            {
                db.YesNoNA.Add(yesNoNA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yesNoNA);
        }

        // GET: YesNoNAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesNoNA yesNoNA = db.YesNoNA.Find(id);
            if (yesNoNA == null)
            {
                return HttpNotFound();
            }
            return View(yesNoNA);
        }

        // POST: YesNoNAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YesNoNAID,YesNoNAName")] YesNoNA yesNoNA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yesNoNA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yesNoNA);
        }

        // GET: YesNoNAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YesNoNA yesNoNA = db.YesNoNA.Find(id);
            if (yesNoNA == null)
            {
                return HttpNotFound();
            }
            return View(yesNoNA);
        }

        // POST: YesNoNAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YesNoNA yesNoNA = db.YesNoNA.Find(id);
            db.YesNoNA.Remove(yesNoNA);
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
