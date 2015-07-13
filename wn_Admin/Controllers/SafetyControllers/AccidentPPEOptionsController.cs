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
    public class AccidentPPEOptionsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: AccidentPPEOptions
        public ActionResult Index()
        {
            return View(db.AccidentPPEOptions.ToList());
        }

        // GET: AccidentPPEOptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentPPEOption accidentPPEOption = db.AccidentPPEOptions.Find(id);
            if (accidentPPEOption == null)
            {
                return HttpNotFound();
            }
            return View(accidentPPEOption);
        }

        // GET: AccidentPPEOptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccidentPPEOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccidentPPEOptionID,AccidentPPEOptionName")] AccidentPPEOption accidentPPEOption)
        {
            if (ModelState.IsValid)
            {
                db.AccidentPPEOptions.Add(accidentPPEOption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accidentPPEOption);
        }

        // GET: AccidentPPEOptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentPPEOption accidentPPEOption = db.AccidentPPEOptions.Find(id);
            if (accidentPPEOption == null)
            {
                return HttpNotFound();
            }
            return View(accidentPPEOption);
        }

        // POST: AccidentPPEOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccidentPPEOptionID,AccidentPPEOptionName")] AccidentPPEOption accidentPPEOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accidentPPEOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accidentPPEOption);
        }

        // GET: AccidentPPEOptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccidentPPEOption accidentPPEOption = db.AccidentPPEOptions.Find(id);
            if (accidentPPEOption == null)
            {
                return HttpNotFound();
            }
            return View(accidentPPEOption);
        }

        // POST: AccidentPPEOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccidentPPEOption accidentPPEOption = db.AccidentPPEOptions.Find(id);
            db.AccidentPPEOptions.Remove(accidentPPEOption);
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
