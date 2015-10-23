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
            var projects = db.Projects.Where(w => w.FK_Client.ClientName.Equals(client) && (w.Status == 1)).Select(s => new { s.ProjectID, s.ProjectName}).ToList();

            return Content(JsonConvert.SerializeObject(projects), "application/json");
        }

        [Authorize(Roles="SUPERADMIN")]
        public ActionResult Projects2(int[] clients)
        {

            if (clients != null && clients.Count() > 0)
            {
              var projects = db.Projects.Where(w => clients.Contains(w.Client) && (w.Status == 1)).OrderBy(o => o.ProjectName).Select(s => new { s.ProjectID, s.ProjectName }).ToList();
              return Content(JsonConvert.SerializeObject(projects), "application/json");
            }
            else
            {
                return Content(null, "application/json");
            }

            
        }

        [HttpPost]
        public ActionResult PayPeriods(DateTime date)
        {
            PayPeriodCalculator calc = new PayPeriodCalculator();
            var model = calc.getPayPeriod(date);

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        public ActionResult Vehicle(string name)
        {
            var vehicle = db.Vehicles.Where(w => w.VehicleName.Equals(name)).FirstOrDefault();

            return Json(vehicle, JsonRequestBehavior.AllowGet);
        }
    }
}