using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class SafetyInterfaceController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: SafetyInterface
        public ActionResult Index()
        {
            // Get model from session first to check existence
            SafetyViewModel modelSession = Session["SafetyViewModel"] as SafetyViewModel;
            SafetyViewModel model = new SafetyViewModel();
            
            if (modelSession == null) 
            {
                // model does not exist, initialize one
                var categories = db.SafetyCategories.ToList();


                model.currentStep = 0;
                model.steps = new List<SafetyStep>();

                
                model.steps.Add(new SafetyStep { Name = "Start Meeting", link = "SafetyMeetings/Create" });

                foreach (var c in categories)
                {
                    model.steps.Add(new SafetyStep { Name = c.SafetyCategoryName, link = "SafetyItems/CreateMultiple?category="+c.SafetyCategoryID });
                }
                

                Session["SafetyViewModel"] = model;
            }
            else
            {
                model = modelSession;
            }
            return View(model);
        }
    }
}