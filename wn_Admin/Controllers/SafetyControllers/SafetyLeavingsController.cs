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
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class SafetyLeavingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyLeavings
        public ActionResult Index()
        {
            var safetyLeavings = db.SafetyLeavings.Include(s => s.SafetyLeavingItem).Include(s => s.SafetyMeeting).Include(s => s.YesNoNA);
            return View(safetyLeavings.ToList());
        }

        // GET: SafetyLeavings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeaving safetyLeaving = db.SafetyLeavings.Find(id);
            if (safetyLeaving == null)
            {
                return HttpNotFound();
            }
            return View(safetyLeaving);
        }

        // GET: SafetyLeavings/CreateMultiple
        public ActionResult CreateMultiple()
        {
            
            var sessionData = Session["MeetingID"];
            int mid = -1;
            if (sessionData != null)
            {
                mid = Convert.ToInt32(sessionData);
            }

            if (mid == -1)
            {
                ViewBag.Message = "Missing Meeting ID";
                return RedirectToAction("Index", "SafetyInterface");
            }

            SafetyLeavingViewModel model = new SafetyLeavingViewModel();
            model.MeetingID = mid;
            model.LeavingItems = db.SafetyLeavingItems.ToList();
            model.YesNoNAs = db.YesNoNA.ToList();

            return View(model);
        }

        // GET: SafetyLeavings/Create
        public ActionResult Create()
        {
            ViewBag.SafetyLeavingItemID = new SelectList(db.SafetyLeavingItems, "SafetyLeavingItemID", "SafetyLeavingItemName");
            var sessionData = Session["MeetingID"];
            int mid = -1;
            if (sessionData != null)
            {
                mid = Convert.ToInt32(sessionData);
            }

            ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings.Where(w => w.SafetyMeetingID == mid), "SafetyMeetingID", "ProjectID");
            ViewBag.YesNoNAID = new SelectList(db.YesNoNA, "YesNoNAID", "YesNoNAName");
            return View();
        }

        // POST: SafetyLeavings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyLeavingID,SafetyMeetingID,SafetyLeavingItemID,YesNoNAID")] SafetyLeaving safetyLeaving)
        {
            if (ModelState.IsValid)
            {
                db.SafetyLeavings.Add(safetyLeaving);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SafetyLeavingItemID = new SelectList(db.SafetyLeavingItems, "SafetyLeavingItemID", "SafetyLeavingItemName", safetyLeaving.SafetyLeavingItemID);
            ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "FieldLocation", safetyLeaving.SafetyMeetingID);
            ViewBag.YesNoNAID = new SelectList(db.YesNoNA, "YesNoNAID", "YesNoNAName", safetyLeaving.YesNoNAID);
            return View(safetyLeaving);
        }

        // GET: SafetyLeavings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeaving safetyLeaving = db.SafetyLeavings.Find(id);
            if (safetyLeaving == null)
            {
                return HttpNotFound();
            }
            ViewBag.SafetyLeavingItemID = new SelectList(db.SafetyLeavingItems, "SafetyLeavingItemID", "SafetyLeavingItemName", safetyLeaving.SafetyLeavingItemID);
            ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "FieldLocation", safetyLeaving.SafetyMeetingID);
            ViewBag.YesNoNAID = new SelectList(db.YesNoNA, "YesNoNAID", "YesNoNAName", safetyLeaving.YesNoNAID);
            return View(safetyLeaving);
        }

        // POST: SafetyLeavings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyLeavingID,SafetyMeetingID,SafetyLeavingItemID,YesNoNAID")] SafetyLeaving safetyLeaving)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyLeaving).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SafetyLeavingItemID = new SelectList(db.SafetyLeavingItems, "SafetyLeavingItemID", "SafetyLeavingItemName", safetyLeaving.SafetyLeavingItemID);
            ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "FieldLocation", safetyLeaving.SafetyMeetingID);
            ViewBag.YesNoNAID = new SelectList(db.YesNoNA, "YesNoNAID", "YesNoNAName", safetyLeaving.YesNoNAID);
            return View(safetyLeaving);
        }

        // GET: SafetyLeavings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyLeaving safetyLeaving = db.SafetyLeavings.Find(id);
            if (safetyLeaving == null)
            {
                return HttpNotFound();
            }
            return View(safetyLeaving);
        }

        // POST: SafetyLeavings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyLeaving safetyLeaving = db.SafetyLeavings.Find(id);
            db.SafetyLeavings.Remove(safetyLeaving);
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
