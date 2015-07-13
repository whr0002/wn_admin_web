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
            
            LinkGroupViewModel lModel = new LinkGroupViewModel();

            
            string role = ui.getFirstRole(User.Identity.GetUserId());
            if (role != null)
            {
                if (role.Equals("Accountant") || role.Equals("SUPERADMIN"))
                {
                    // People Section
                    ListLinkViewModel people = new ListLinkViewModel();
                    people.ListName = "People";
                    people.color = "Orange";
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Create an Account", Link = "/account/register" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Manage Employees", Link = "/employees" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Manage User Roles", Link = "/roles" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Change an Account's password", Link = "/users/changepassword" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Remove an Account", Link = "/users/removeaccount" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Assign an Account to an Employee", Link = "/useremployees" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Assign an Employee to a Department", Link = "/worksfors" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Assign a Supervisor to an Employee", Link = "/supervisions" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Safety", Link = "/SafetyInterface" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Major Accident Form", Link = "/MajorAccidentForms" });
                    people.ListLinks.Add(new LinkViewModel { LinkName = "Minor Accident Form", Link = "/MinorAccidentForms" });

                    // Projects
                    ListLinkViewModel projects = new ListLinkViewModel();
                    projects.ListName = "Projects";
                    projects.color = "#428bca";
                    projects.ListLinks.Add(new LinkViewModel { LinkName = "Manage Clients", Link = "/clients" });
                    projects.ListLinks.Add(new LinkViewModel { LinkName = "Manage Projects", Link = "/projects" });

                    projects.ListLinks.Add(new LinkViewModel { LinkName = "Assign a Project to a Department", Link = "/controls" });
                    projects.ListLinks.Add(new LinkViewModel { LinkName = "Project Summaries", Link = "/projectsummary" });

                    // Time
                    ListLinkViewModel time = new ListLinkViewModel();
                    time.ListName = "Time";
                    time.color = "Green";
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Daily Time Ticket", Link = "/workings/create" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Timesheets", Link = "/workings" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Pay Periods", Link = "/payperiods" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Tasks", Link = "/tasks" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Field Accesses", Link = "/fieldaccesses" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Off Details", Link = "/offreasons" });
                    

                    // Resource
                    ListLinkViewModel resource = new ListLinkViewModel();
                    resource.ListName = "Resource";
                    resource.color = "Grey";
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Manage Vehicles", Link = "/vehicles" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Manage Departments", Link = "/departments" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Manage Equipments", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Manage Account Types", Link = "/accounttypes" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Manage Time Off Request", Link = "/timeoffrequests" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Costs", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Payments", Link = "/expenses" });


                    lModel.sections.Add(people);
                    lModel.sections.Add(projects);
                    lModel.sections.Add(time);
                    lModel.sections.Add(resource);

                }
                else
                {
                    ListLinkViewModel time = new ListLinkViewModel();
                    time.ListName = "Time";
                    time.color = "Green";
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Daily Time Ticket", Link = "/workings/create" });
                    time.ListLinks.Add(new LinkViewModel { LinkName = "Manage Timesheets", Link = "/workings" });

                    // Time
                    ListLinkViewModel resource = new ListLinkViewModel();
                    resource.ListName = "Resource";
                    resource.color = "Grey";
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Expense Form", Link = "/expenses" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Time Off Request", Link = "/timeoffrequests" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Daily Tailgate Safety Meeting", Link = "/SafetyInterface" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Vehicle Inspection Form", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Major Incident Report Form", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Minor Incident Report Form", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Employee Handbook", Link = "/#" });
                    resource.ListLinks.Add(new LinkViewModel { LinkName = "Health & Safety Manual", Link = "/#" });

                    lModel.sections.Add(time);
                    lModel.sections.Add(resource);
                }
            }
            
           

            return View(lModel);
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