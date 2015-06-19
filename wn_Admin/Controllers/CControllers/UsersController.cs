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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            setDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string username, string password)
        {
            ApplicationUser user = db.Users.Where(w => w.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            manager.RemovePassword(user.Id);
            manager.AddPassword(user.Id, password);

            ViewBag.message = "Password changed successfully.";
            setDropdowns();
            return View();    
        }

        public ActionResult RemoveAccount()
        {

            setDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAccount(string username)
        {
            try { 
                ApplicationUser user = db.Users.Where(w => w.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                manager.Delete(user);
                ViewBag.message = "Removed the user successfully.";
            }
            catch
            {
                ViewBag.message = "Failed to remove this user.";
            }

            setDropdowns();
            return View();
        }

        private void setDropdowns()
        {
            ViewBag.Users = db.Users.OrderBy(o => o.UserName)
                .Select(s => new SelectListItem { Value = s.UserName.ToString(), Text = s.UserName }).ToList();
        }
    }

    
}