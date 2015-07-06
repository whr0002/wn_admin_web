using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CompanyModels;
using wn_Admin.Models.Safety;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class EmployeeSafetyMeetingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: EmployeeSafetyMeetings
        public ActionResult Index()
        {
            return View(db.EmployeeSafetyMeetings.ToList());
        }

        // GET: EmployeeSafetyMeetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSafetyMeeting employeeSafetyMeeting = db.EmployeeSafetyMeetings.Find(id);
            if (employeeSafetyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(employeeSafetyMeeting);
        }

        // GET: EmployeeSafetyMeetings/Create
        public ActionResult Create(int currentStep = -1)
        {
            var meetingID = Session["MeetingID"];
            int mid = -1;
            if (meetingID != null)
            {
                mid = Convert.ToInt32(meetingID);
            }

            if (mid != -1)
            {
                var eids = db.EmployeeSafetyMeetings.Where(w => w.SafetyMeetingID == mid).Select(s => s.EmployeeID).ToList();
                var attendees = db.Employees.Where(w => eids.Contains(w.EmployeeID)).ToList();
                var listNames = new List<string>();
                foreach (var a in attendees)
                {
                    listNames.Add(a.FullName);
                }
                ViewBag.Attendees = listNames;
                ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings.Where(w => w.SafetyMeetingID == mid), "SafetyMeetingID", "SafetyMeetingID");

                if (currentStep != -1)
                {
                    var m = Session["SafetyViewModel"] as SafetyViewModel;
                    m.currentStep = currentStep;
                    Session["SafetyViewModel"] = m;
                }
            }
            else
            {
                ViewBag.Attendees = new List<string>();
                ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "SafetyMeetingID");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");

            return View();
        }

        // POST: EmployeeSafetyMeetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,SafetyMeetingID")] EmployeeSafetyMeeting employeeSafetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeSafetyMeetings.Add(employeeSafetyMeeting);
                db.SaveChanges();

                var model = Session["SafetyViewModel"] as SafetyViewModel;
                model.finishedSections.Add(model.currentStep);
                model.currentStep = SafetyItemsController.getNextUnfinishedStep(model);
                Session["SafetyViewModel"] = model;



                return RedirectToAction("Create");
            }

            var meetingID = Session["MeetingID"];
            int mid = -1;
            if (meetingID != null)
            {
                mid = Convert.ToInt32(meetingID);
            }

            if (mid != -1)
            {
                var attendees = db.EmployeeSafetyMeetings.Where(w => w.SafetyMeetingID == mid).Select(s => s.Employee);
                ViewBag.Attendees = attendees;
                ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings.Where(w => w.SafetyMeetingID == mid), "SafetyMeetingID", "SafetyMeetingID");
            }
            else
            {
                ViewBag.Attendees = Enumerable.Empty<Employee>().AsQueryable();
                ViewBag.SafetyMeetingID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "SafetyMeetingID");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", employeeSafetyMeeting.EmployeeID);
            ViewBag.EmployeeID = new SelectList(db.SafetyMeetings, "SafetyMeetingID", "ProjectID", employeeSafetyMeeting.EmployeeID);
            return View(employeeSafetyMeeting);
        }


        // GET: EmployeeSafetyMeetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSafetyMeeting employeeSafetyMeeting = db.EmployeeSafetyMeetings.Find(id);
            if (employeeSafetyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(employeeSafetyMeeting);
        }

        // POST: EmployeeSafetyMeetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,SafetyMeetingID")] EmployeeSafetyMeeting employeeSafetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSafetyMeeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeSafetyMeeting);
        }

        // GET: EmployeeSafetyMeetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSafetyMeeting employeeSafetyMeeting = db.EmployeeSafetyMeetings.Find(id);
            if (employeeSafetyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(employeeSafetyMeeting);
        }

        // POST: EmployeeSafetyMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSafetyMeeting employeeSafetyMeeting = db.EmployeeSafetyMeetings.Find(id);
            db.EmployeeSafetyMeetings.Remove(employeeSafetyMeeting);
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
