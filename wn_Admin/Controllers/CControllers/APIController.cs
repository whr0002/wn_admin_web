using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize()]
    public class APIController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        public ActionResult Projects(string client)
        {
            var projects = db.Projects.Where(w => w.FK_Client.ClientName.Equals(client)).Select(s => new { s.ProjectID, s.ProjectName}).ToList();

            return Content(JsonConvert.SerializeObject(projects), "application/json");
        }

        public ActionResult PayPeriods(DateTime date)
        {
            PayPeriodCalculator calc = new PayPeriodCalculator();
            var model = calc.getPayPeriod(date);

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }
    }
}