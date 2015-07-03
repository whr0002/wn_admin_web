using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models.UtilityModels;

namespace wn_Admin.Controllers.SafetyControllers
{
    public class SafetyInterfaceController : Controller
    {
        // GET: SafetyInterface
        public ActionResult Index()
        {
            // Get model from session first to check existence
            SafetyViewModel modelSession = Session["SafetyViewModel"] as SafetyViewModel;
            SafetyViewModel model = new SafetyViewModel();
            
            if (modelSession == null) 
            {
                // model does not exist, initialize one
                model.currentStep = 0;
                model.steps = new List<SafetyStep>();
                model.steps.Add(new SafetyStep { Name = "1.0 Start Meeting", link = "SafetyMeetings/Create" });
                model.steps.Add(new SafetyStep { Name = "2.0 Prior to leaving", link = "SafetyLeavings/CreateMultiple" });

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