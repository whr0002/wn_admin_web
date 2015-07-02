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
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles="SUPERADMIN,Accountant,Employee")]
    public class TimeOffRequestsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo ui = new UserInfo();
        // GET: TimeOffRequests
        public ActionResult Index()
        {
            var timeOffRequests = db.TimeOffRequests.Include(t => t.Employee).Include(t => t.OffReason);
            var role = ui.getFirstRole(User.Identity.GetUserId());

            if (role.Equals("Employee"))
            {
                // Show records only from him.
                Employee e = ui.getEmployee(User.Identity.GetUserId());
                timeOffRequests = timeOffRequests.Where(w => w.EmployeeID == e.EmployeeID);

            }

            return View(timeOffRequests.ToList());
        }

        // GET: TimeOffRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOffRequest timeOffRequest = db.TimeOffRequests.Find(id);
            if (timeOffRequest == null)
            {
                return HttpNotFound();
            }
            return View(timeOffRequest);
        }

        // GET: TimeOffRequests/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            ViewBag.OffReasonID = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName");
            return View();
        }

        // POST: TimeOffRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeOffRequestID,EmployeeID,StartDate,EndDate,ReturnToWorkDate,NumberOfDays,OffReasonID,Notes")] TimeOffRequest timeOffRequest)
        {
            if (ModelState.IsValid)
            {
                db.TimeOffRequests.Add(timeOffRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", timeOffRequest.EmployeeID);
            ViewBag.OffReasonID = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", timeOffRequest.OffReasonID);
            return View(timeOffRequest);
        }

        // GET: TimeOffRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOffRequest timeOffRequest = db.TimeOffRequests.Find(id);
            if (timeOffRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", timeOffRequest.EmployeeID);
            ViewBag.OffReasonID = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", timeOffRequest.OffReasonID);
            return View(timeOffRequest);
        }

        // POST: TimeOffRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeOffRequestID,EmployeeID,StartDate,EndDate,ReturnToWorkDate,NumberOfDays,OffReasonID,Notes")] TimeOffRequest timeOffRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeOffRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", timeOffRequest.EmployeeID);
            ViewBag.OffReasonID = new SelectList(db.OffReasons, "OffReasonID", "OffReasonName", timeOffRequest.OffReasonID);
            return View(timeOffRequest);
        }

        // GET: TimeOffRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeOffRequest timeOffRequest = db.TimeOffRequests.Find(id);
            if (timeOffRequest == null)
            {
                return HttpNotFound();
            }
            return View(timeOffRequest);
        }

        // POST: TimeOffRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeOffRequest timeOffRequest = db.TimeOffRequests.Find(id);
            db.TimeOffRequests.Remove(timeOffRequest);
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
