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
    public class AdvEffsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: AdvEffs
        public ActionResult Index()
        {
            return View(db.AdvEffs.ToList());
        }

        // GET: AdvEffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvEff advEff = db.AdvEffs.Find(id);
            if (advEff == null)
            {
                return HttpNotFound();
            }
            return View(advEff);
        }

        // GET: AdvEffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvEffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvEffID,AdvEffName")] AdvEff advEff)
        {
            if (ModelState.IsValid)
            {
                db.AdvEffs.Add(advEff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advEff);
        }

        // GET: AdvEffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvEff advEff = db.AdvEffs.Find(id);
            if (advEff == null)
            {
                return HttpNotFound();
            }
            return View(advEff);
        }

        // POST: AdvEffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvEffID,AdvEffName")] AdvEff advEff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advEff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advEff);
        }

        // GET: AdvEffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvEff advEff = db.AdvEffs.Find(id);
            if (advEff == null)
            {
                return HttpNotFound();
            }
            return View(advEff);
        }

        // POST: AdvEffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdvEff advEff = db.AdvEffs.Find(id);
            db.AdvEffs.Remove(advEff);
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
