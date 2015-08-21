using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize(Roles="SUPERADMIN,Accountant")]
    public class APITreeController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        public JsonResult root()
        {
            return Json(new { 
                id = -1, 
                parent="#", 
                text = "Clients", 
                children = true, 
                data = "/apitree/clients" }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult clients(string parent)
        {
            var clients = db.Clients
                .Select(s => new
            {
                id = "c"+s.ClientID,
                parent = parent,
                text = s.ClientName,
                children = true,
                data = "/apitree/projects"
            }); ;

            return Json(clients.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult projects(string parent)
        {
            int id = int.Parse(parent.Substring(1));
            var projects = db.Projects
                .Where(w => w.FK_Client.ClientID == id)
                .Select(s => new {
                    id = "p"+s.ProjectID,
                    parent = parent,
                    text = s.ProjectName,
                    children = true,
                    data = "/apitree/supervisors"
                });


            return Json(projects.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult supervisors(string parent)
        {
            var supervisors = db.Supervisions
                .Where(w => w.ProjectID.Equals(parent.Substring(1)))
                .Select(s => new {
                    id = "s"+s.SupervisorID,
                    parent = parent,
                    text = s.Supervisor.FullName,
                    children = true,
                    data = "/apitree/employees?projectID=" + parent
                }).Distinct();

            return Json(supervisors.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult employees(string parent, string projectID)
        {
            int id = int.Parse(parent.Substring(1));
            var employees = db.Supervisions
                .Where(w => w.SupervisorID == id && w.ProjectID.Equals(projectID.Substring(1)))
                .Select(s => new {
                    id = "e"+ s.EmployeeID,
                    parent = parent,
                    text = s.Employee.FullName,
                    children = false
            });

            return Json(employees.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}