using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;


namespace wn_Admin.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserInfo ui = new UserInfo();
            List<LinkViewModel> links = new List<LinkViewModel>();

            
            string role = ui.getFirstRole(User.Identity.GetUserId());
            if (role != null)
            {
                if (role.Equals("Accountant"))
                {
                    links.Add(new LinkViewModel { LinkName = "Manage Timesheets", Link = "/workings" });
                    links.Add(new LinkViewModel { LinkName = "Manage Employees", Link = "/employees" });
                    links.Add(new LinkViewModel { LinkName = "Manage Departments", Link = "/departments" });
                    links.Add(new LinkViewModel { LinkName = "Manage Projects", Link = "/projects" });
                    links.Add(new LinkViewModel { LinkName = "Manage Clients", Link = "/clients" });

                    links.Add(new LinkViewModel { LinkName = "Manage Pay Periods", Link = "/payperiods" });
                    links.Add(new LinkViewModel { LinkName = "Manage Vehicles", Link = "/vehicles" });
                    links.Add(new LinkViewModel { LinkName = "Manage Tasks", Link = "/tasks" });

                    links.Add(new LinkViewModel { LinkName = "Manage Field Accesses", Link = "/fieldaccesses" });
                    links.Add(new LinkViewModel { LinkName = "Manage Off Details", Link = "/offreasons" });




                    links.Add(new LinkViewModel { LinkName = "Create an Account", Link = "/account/register" });

                    links.Add(new LinkViewModel { LinkName = "Assign an Account to an Employee", Link = "/useremployees" });
                    links.Add(new LinkViewModel { LinkName = "Assign a Supervisor to an Employee", Link = "/supervisions" });
                    links.Add(new LinkViewModel { LinkName = "Assign an Employee to a Department", Link = "/worksfors" });
                    links.Add(new LinkViewModel { LinkName = "Assign a Project to a Department", Link = "/controls" });


                }
                else
                {
                    links.Add(new LinkViewModel { LinkName = "Manage Timesheets", Link = "/workings" });
                    
                }
            }
            
           

            return View(links);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}