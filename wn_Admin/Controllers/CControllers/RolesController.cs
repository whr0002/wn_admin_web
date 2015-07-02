using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;

namespace wn_Admin.Controllers.CControllers
{
    //[Authorize(Roles = "SUPERADMIN,Accountant")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();

            return View(roles);
        }


        public ActionResult Create(){

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name)
        {
            try
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { 
                    Name = name
                });
                db.SaveChanges();
                ViewBag.message = "Role created successfully!";
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string name)
        {
            var role = db.Roles.Where(w => w.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

            
        }

        public ActionResult Delete(string name)
        {

            var role = db.Roles.Where(w => w.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db.Roles.Remove(role);
            db.SaveChanges();

            return RedirectToAction("Index");
            

        }

        public ActionResult ManageUserRoles()
        {
            setDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string username, string roleName)
        {
            ApplicationUser user = db.Users.Where(w => w.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.AddToRole(user.Id, roleName);

            setDropdowns();
            return RedirectToAction("ManageUserRoles");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);

                if (user.Id != null)
                {
                    ViewBag.UsernameForThisUser = UserName; 
                    ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);
                }
                // prepopulate usernames and roles for the view dropdown
                setDropdowns();


            }
            return View("ManageUserRoles");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string username, string roleName)
        {
            ApplicationUser user = db.Users.Where(w => w.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var us = new UserStore<ApplicationUser>(db);
            var um = new UserManager<ApplicationUser>(us);

            if (um.IsInRole(user.Id, roleName))
            {
                um.RemoveFromRole(user.Id, roleName);
            }

            if (user.Id != null)
            {
                ViewBag.RolesForThisUser = um.GetRoles(user.Id);
            }

            setDropdowns();

            return View("ManageUserRoles");
        }

        private void setDropdowns()
        {
            ViewBag.Users = db.Users.OrderBy(o => o.UserName)
                .Select(s => new SelectListItem { Value = s.UserName.ToString(), Text = s.UserName }).ToList();

            ViewBag.Roles = db.Roles.OrderBy(o => o.Name)
                .Select(s => new SelectListItem { Value = s.Name.ToString(), Text = s.Name }).ToList();
        }
    }

   
}