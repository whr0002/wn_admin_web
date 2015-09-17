using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using Microsoft.AspNet.Identity;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant")]
    public class ProjectsController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Projects
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            UserInfo userInfo = new UserInfo();
            if (userInfo.isInRole(userId, "SUPERADMIN") || userInfo.isInRole(userId, "Accountant"))
            {
                ViewBag.hasControl = true;
            }
            else
            {
                ViewBag.hasControl = false;
            }

            var projects = db.Projects.Include(p => p.FK_Client);
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Create()
        {
            ViewBag.Client = new SelectList(db.Clients, "ClientID", "ClientName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,Client,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client = new SelectList(db.Clients, "ClientID", "ClientName", project.Client);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client = new SelectList(db.Clients, "ClientID", "ClientName", project.Client);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,Client,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client = new SelectList(db.Clients, "ClientID", "ClientName", project.Client);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SUPERADMIN, Accountant")]
        public ActionResult DeleteConfirmed(string id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SUPERADMIN, Accountant")]
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
