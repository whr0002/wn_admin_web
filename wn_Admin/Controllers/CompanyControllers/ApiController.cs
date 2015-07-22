using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize()]
    public class ApiController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo ui = new UserInfo();

        //public JsonResult events()
        //{

        //}
    }
}