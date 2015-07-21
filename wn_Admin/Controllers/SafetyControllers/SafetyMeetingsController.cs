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
    [Authorize()]
    public class SafetyMeetingsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo ui = new UserInfo();

        // GET: SafetyMeetings
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
        public ActionResult Index()
        {

            var safetyMeetings = db.SafetyMeetings.Include(s => s.Project);
            return View(safetyMeetings.ToList());
        }

        // GET: SafetyMeetings/Details/5
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
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
            var uid = User.Identity.GetUserId();

            var e = ui.getEmployee(uid);

            if (ui.isInRole(uid, "SafetyOfficer") || ui.isInRole(uid, "SUPERADMIN"))
            {
                ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == e.EmployeeID), "EmployeeID", "FullName");
            }
            
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.People = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.Items = db.SafetyItemValues.ToList();
            ViewBag.YesNoNA = db.YesNoNA.ToList();

            return View();
        }

        // POST: SafetyMeetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int[] attendees, FormCollection fc, [Bind(Include = "SafetyMeetingID,Date,EmployeeID, ProjectID,FieldLocation,SafeWorkPermitNum,ScopeOfWork,SafetyLeavingID")] SafetyMeeting safetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.SafetyMeetings.Add(safetyMeeting);
                db.SaveChanges();

                CreateMultiple(fc, safetyMeeting.SafetyMeetingID);
                addAttendees(attendees, safetyMeeting.SafetyMeetingID);
                return RedirectToAction("Index");
            }

            ViewBag.Items = db.SafetyItemValues.ToList();
            ViewBag.YesNoNA = db.YesNoNA.ToList();
            ViewBag.People = new SelectList(db.Employees, "EmployeeID", "FullName");

            var e = ui.getEmployee(User.Identity.GetUserId());
            ViewBag.EmployeeID = new SelectList(db.Employees.Where(w => w.EmployeeID == e.EmployeeID), "EmployeeID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", safetyMeeting.ProjectID);
            return View(safetyMeeting);
        }

        private void addAttendees(int[] attendees, int mid)
        {
            if (attendees != null)
            {
                foreach (int id in attendees)
                {
                    EmployeeSafetyMeeting esm = new EmployeeSafetyMeeting();
                    esm.SafetyMeetingID = mid;
                    esm.EmployeeID = id;
                    db.EmployeeSafetyMeetings.Add(esm);
                    db.SaveChanges();
                }
            }
        }

        private void CreateMultiple(FormCollection fc, int mid)
        {
            
            int numOfItems = Convert.ToInt32(fc["NumOfItems"]);
            for (int i = 0; i < numOfItems; i++)
            {
                SafetyItem si = new SafetyItem();

                int cid = Convert.ToInt32(fc["item_cate_" + i]);
                string itemName = fc["item_name_" + i];
                var tempValue = fc["item_answer_" + i];
                var comment = fc["item_comment_" + i];

                if (tempValue != null && !tempValue.Equals(""))
                {
                    int itemValue = Convert.ToInt32(tempValue);
                    si.YesNoNAID = itemValue;
                }


                si.SafetyMeetingID = mid;
                si.SafetyCategoryID = cid;
                si.SafetyItemName = itemName;
                si.Description = comment;

                db.SafetyItems.Add(si);
                db.SaveChanges();
            }

            

        }

        // GET: SafetyMeetings/Edit/5
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
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
            ViewBag.People = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.Atts = safetyMeeting.EmployeeSafetyMeetings.ToList();
            ViewBag.Items = db.SafetyItems.Where(w => w.SafetyMeetingID == meetingID).ToList();
            ViewBag.YesNoNA = db.YesNoNA.ToList();

            return View(safetyMeeting);
        }

        // POST: SafetyMeetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
        public ActionResult Edit(FormCollection fc, [Bind(Include = "SafetyMeetingID,Date,EmployeeID,ProjectID,FieldLocation,SafeWorkPermitNum,ScopeOfWork,SafetyLeavingID,IsReviewedBySafetyManager")] SafetyMeeting safetyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(safetyMeeting).State = EntityState.Modified;
                db.SaveChanges();

                EditMultiple(fc);
                return RedirectToAction("Index");
            }

            ViewBag.People = new SelectList(db.Employees, "EmployeeID", "FullName");
            ViewBag.Atts = safetyMeeting.EmployeeSafetyMeetings.ToList();
            ViewBag.Items = db.SafetyItems.Where(w => w.SafetyMeetingID == safetyMeeting.SafetyMeetingID).ToList();
            ViewBag.YesNoNA = db.YesNoNA.ToList();
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", safetyMeeting.ProjectID);
            return View(safetyMeeting);
        }

        private void EditMultiple(FormCollection fc)
        {

            int numOfItems = Convert.ToInt32(fc["NumOfItems"]);
            for (int i = 0; i < numOfItems; i++)
            {

                string itemName = fc["itemID_" + i];
                var tempValue = fc["item_answer_" + i];
                var comment = fc["item_comment_" + i];

                if (tempValue != null && !tempValue.Equals(""))
                {
                    int itemID = Convert.ToInt32(itemName);
                    int itemValue = Convert.ToInt32(tempValue);

                    SafetyItem si = db.SafetyItems.Where(w => w.SafetyItemID == itemID).FirstOrDefault();

                    if (si != null)
                    {
                        si.YesNoNAID = itemValue;
                        si.Description = comment;
                        db.SaveChanges();
                    }

                }


            }
        }

        // GET: SafetyMeetings/Delete/5
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
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
        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
        public ActionResult DeleteConfirmed(int id)
        {
            SafetyMeeting safetyMeeting = db.SafetyMeetings.Find(id);
            db.SafetyMeetings.Remove(safetyMeeting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "SUPERADMIN,SafetyOfficer")]
        public ActionResult review([ModelBinder(typeof(IntArrayModelBinder))]int[] ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var meeting = db.SafetyMeetings.Find(id);
                    if (meeting != null)
                    {
                        meeting.IsReviewedBySafetyManager = true;
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }


    }
}
