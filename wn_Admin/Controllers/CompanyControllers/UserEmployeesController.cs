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

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant")]
    public class UserEmployeesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private ApplicationDbContext appDb = new ApplicationDbContext();

        // GET: UserEmployees
        public ActionResult Index()
        {
            var userEmployees = db.UserEmployees.Include(u => u.Employee).ToList();
            var uevm = new List<UEViewModel>();

            foreach(var ue in userEmployees){
                UEViewModel model = new UEViewModel();

                var username = appDb.Users.Find(ue.UserID).UserName;

                model.id = ue.UserID;
                model.accountName = username;
                model.fullName = ue.Employee.FullName;

                uevm.Add(model);
            }

            return View(uevm);
        }

        // GET: UserEmployees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmployee userEmployee = db.UserEmployees.Find(id);
            if (userEmployee == null)
            {
                return HttpNotFound();
            }
            return View(userEmployee);
        }

        // GET: UserEmployees/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(appDb.Users, "Id", "UserName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            
            return View();
        }

        // POST: UserEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,EmployeeID")] UserEmployee userEmployee)
        {
            if (ModelState.IsValid)
            {
                db.UserEmployees.Add(userEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(appDb.Users, "Id", "UserName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", userEmployee.EmployeeID);
            return View(userEmployee);
        }

        // GET: UserEmployees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmployee userEmployee = db.UserEmployees.Find(id);
            if (userEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", userEmployee.EmployeeID);
            return View(userEmployee);
        }

        // POST: UserEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,EmployeeID")] UserEmployee userEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", userEmployee.EmployeeID);
            return View(userEmployee);
        }

        // GET: UserEmployees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEmployee userEmployee = db.UserEmployees.Find(id);
            if (userEmployee == null)
            {
                return HttpNotFound();
            }
            return View(userEmployee);
        }

        // POST: UserEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserEmployee userEmployee = db.UserEmployees.Find(id);
            db.UserEmployees.Remove(userEmployee);
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
