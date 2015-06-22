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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize()]
    public class TimesheetsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private ApplicationDbContext appDb = new ApplicationDbContext();
        // GET: Timesheets
        public ActionResult Index()
        {
            var timesheets = db.Timesheets.Include(t => t.FK_Client).Include(t => t.FK_FieldAccess).Include(t => t.FK_Off).Include(t => t.FK_Task).Include(t => t.FK_Vehicle);
            var user = getCurrentUser();
            timesheets = timesheets.Where(w => w.Name.Equals(user.FullName));
            return View(timesheets.ToList());
        }

        // GET: Timesheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: Timesheets/Create
        public ActionResult Create()
        {

            setupDropdowns();

            Timesheet timesheet = new Timesheet();
            timesheet.Date = DateTime.Today;
            PPViewModel model = getPayPeriod();
            timesheet.PP = model.PPNumber;
            timesheet.PPyr = model.PPYear;
            return View(timesheet);
        }

        private void setupDropdowns()
        {
            ViewBag.Client = new SelectList(db.Clients, "ClientID", "ClientID");
            ViewBag.Field = new SelectList(db.FieldAccesses, "FieldAccessID", "FieldAccessID");
            ViewBag.Off = new SelectList(db.Tasks, "TaskID", "TaskID");
            ViewBag.Task = new SelectList(db.Tasks, "TaskID", "TaskID");
            ViewBag.Veh = new SelectList(db.Vehicles, "VehicleID", "VehicleID");

            var role = getRole();
            if (role != null)
            {
                if (role.Equals("SUPERADMIN"))
                {
                    ViewBag.Name = new SelectList(appDb.Users, "FullName", "FullName");
                }
                else
                {
                    var user = getCurrentUser();
                    List<ApplicationUser> users = new List<ApplicationUser>();
                    users.Add(user);
                    ViewBag.Name = new SelectList(users, "FullName", "FullName");
                }
            }
        }

        private ApplicationUser getCurrentUser()
        {
            var id = User.Identity.GetUserId();
            var user = appDb.Users.Find(id);

            return user;
        }

        private string getRole()
        {
            var user = getCurrentUser();
            if (user != null) { 
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
               
                List<string> roles = um.GetRoles(user.Id) as List<string>;
                if (roles != null)
                {
                    return roles[0];
                }
            }

            return null;

        }

        private PPViewModel getPayPeriod()
        {
            DateTime currentDate = DateTime.Today;
            int cyear = currentDate.Year;
            string currentYear = cyear.ToString();
            PPViewModel ppModel = new PPViewModel();


            PayPeriod payperiod = db.PayPeriods.Where(w => w.PayPeriodID.Equals(currentYear)).FirstOrDefault();
            if (payperiod != null)
            {
                // Found the current year payperiod
                //currentDate = new DateTime(2015, 6, 6);
                int diff = currentDate.Subtract(payperiod.StartDate).Days+1;
                double result = Math.Ceiling(((double)diff) / 14);
                //Response.Write("Diff: " + diff + "<br />" + result + "<br />");
                ppModel.PPYear = cyear;
                ppModel.PPNumber = (int)result;
                return ppModel;

            }
            else
            {
                // Current year not found, try find previous year
                PayPeriod payperiodPrevious = db.PayPeriods.Where(w => w.PayPeriodID.Equals((cyear-1).ToString())).FirstOrDefault();


                if (payperiodPrevious != null)
                {
                    DateTime endDate = payperiodPrevious.StartDate.AddYears(1);
                    //currentDate = new DateTime(2015, 5, 23);
                    if (currentDate < endDate)
                    {
                        int diff = currentDate.Subtract(payperiodPrevious.StartDate).Days + 1;

                        double result = Math.Ceiling(((double)diff) / 14);
                        //Response.Write("Year Diff: " + diff + "<br />" + result + "<br />");
                        ppModel.PPYear = payperiodPrevious.StartDate.Year;
                        ppModel.PPNumber = (int)result;
                        return ppModel;
                    }
                }


            }

            return ppModel;
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetID,Name,Date,PPyr,PP,Client,Project,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,Off,Hours,Bank,OT")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            setupDropdowns();
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }

            setupDropdowns();

            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetID,Name,Date,PPyr,PP,Client,Project,ProjectID,Task,Identifier,Veh,Crew,StartKm,EndKm,GPS,Field,PD,JobDescription,Off,Hours,Bank,OT")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            setupDropdowns();

            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheet);
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
