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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.SafetyControllers
{
    [Authorize(Roles="SUPERADMIN")]
    public class SafetyMeetingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo ui = new UserInfo();

        // GET: SafetyMeetings
        public ActionResult Index()
        {
            var safetyMeetings = db.SafetyMeetings.Include(s => s.Project);
            return View(safetyMeetings.ToList());
        }

        // GET: SafetyMeetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyMeeting safetyMeeting = db.SafetyMeetings.Find(id);
            if (safetyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(safetyMeeting);
        }

        // GET: SafetyMeetings/Create
        public ActionResult Create()
        {
            var e = ui.getEmployee(User.Identity.GetUserId());

            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == e.EmployeeID), "EmployeeID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            return View();
        }

        // POST: SafetyMeetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SafetyMeetingID,Date,EmployeeID, ProjectID,FieldLocation,SafeWorkPermitNum,ScopeOfWork,SafetyLeavingID,IsReviewedBySafetyManager")] SafetyMeeting safetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.SafetyMeetings.Add(safetyMeeting);
                db.SaveChanges();
                Session["MeetingID"] = safetyMeeting.SafetyMeetingID;

                var model = Session["SafetyViewModel"] as SafetyViewModel;
                if (model != null)
                {
                    model.finishedSections.Add(model.currentStep);
                    model.currentStep += 1;
                    Session["SafetyViewModel"] = model;
                }

                return RedirectToAction("Index", "SafetyInterface");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", safetyMeeting.ProjectID);
            return View(safetyMeeting);
        }

        // GET: SafetyMeetings/Edit/5
        public ActionResult Edit(int? meetingID)
        {
            if (meetingID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyMeeting safetyMeeting = db.SafetyMeetings.Find(meetingID);
            if (safetyMeeting == null)
            {
                return HttpNotFound();
            }

            var e = ui.getEmployee(User.Identity.GetUserId());

            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == e.EmployeeID), "EmployeeID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", safetyMeeting.ProjectID);
            return View(safetyMeeting);
        }

        // POST: SafetyMeetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SafetyMeetingID,Date,EmployeeID,ProjectID,FieldLocation,SafeWorkPermitNum,ScopeOfWork,SafetyLeavingID,IsReviewedBySafetyManager")] SafetyMeeting safetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyMeeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", safetyMeeting.ProjectID);
            return View(safetyMeeting);
        }

        // GET: SafetyMeetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SafetyMeeting safetyMeeting = db.SafetyMeetings.Find(id);
            if (safetyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(safetyMeeting);
        }

        // POST: SafetyMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyMeeting safetyMeeting = db.SafetyMeetings.Find(id);
            db.SafetyMeetings.Remove(safetyMeeting);
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
