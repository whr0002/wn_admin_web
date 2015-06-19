using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;

namespace wn_Admin.Controllers.CControllers
{
    public class APIController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        public ActionResult Projects(string client)
        {
            var projects = db.Projects.Where(w => w.Client.Equals(client)).Select(s => new { s.ProjectID, s.ProjectName}).ToList();

            return Content(JsonConvert.SerializeObject(projects), "application/json");
        }
    }
}