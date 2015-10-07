using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize()]
    public class AdminDataController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo mUserInfo = new UserInfo();

        // GET: AdminData
        public ActionResult Workings()
        {
            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);
            EventService eventService = new EventService(employee,db);

            List<EventViewModel> evm = eventService.getEventViewModel(db.Workings);

            return Json(evm, JsonRequestBehavior.AllowGet);
        }
    }
}