using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;
using System.Data;
using System.Data.Entity;

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
                model.finishedSections = new List<int>();


                model.steps.Add(new SafetyStep { Name = "Start Meeting", link = "SafetyMeetings/Create", editLink = "SafetyMeetings/Edit?", StepNumber = 0 });

                int i = 1;
                foreach (var c in categories)
                {
                    model.steps.Add(new SafetyStep { Name = c.SafetyCategoryName, link = "SafetyItems/CreateMultiple?category=" + c.SafetyCategoryID +"&currentStep="+i, editLink = "SafetyItems/EditMultiple?category=" + c.SafetyCategoryID + "&", StepNumber =  i});
                    i++;
                }

                model.steps.Add(new SafetyStep { Name = "Attendees", link = "EmployeeSafetyMeetings/Create"+"?currentStep="+i, editLink = "EmployeeSafetyMeetings/Edit?", StepNumber = i });

                Session["SafetyViewModel"] = model;
            }
            else
            {
                model = modelSession;
            }
            return View(model);
        }

        public ActionResult ClearMeetingSession()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult Summary(int mid = -1) 
        {

            if (mid == -1)
            {
                return View();
            }

            var safetyMeetings = db.SafetyMeetings.Include(s => s.SafetyItems).Include(s => s.Employee).Include(s => s.EmployeeSafetyMeetings);
            var theMeeting =  safetyMeetings.Where(w => w.SafetyMeetingID == mid).FirstOrDefault();
            var ids = theMeeting.EmployeeSafetyMeetings.Select(s => s.EmployeeID).ToList();
            var es = db.Employees.Where(w => ids.Contains(w.EmployeeID)).ToList();
            ViewBag.Attendees = es;
            return View(theMeeting);
           
        }
    }
}