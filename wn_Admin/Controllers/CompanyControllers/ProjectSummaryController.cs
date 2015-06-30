using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;
using System.Data;
using System.Data.Entity;
using wn_Admin.Models.CompanyModels;



namespace wn_Admin.Controllers.CompanyControllers
{
    public class ProjectSummaryController : Controller
    {
        private wn_admin_db  db = new wn_admin_db();

        // GET: ProjectSummary
        public ActionResult Index(int ClientID = -1, string ProjectID = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            SummaryModel model = new SummaryModel();
            var workings = db.Workings.Include(w => w.Employee).Include(w => w.FK_FieldAccess).Include(w => w.FK_OffReason).Include(w => w.FK_Task).Include(w => w.FK_Vehicle).Include(w => w.Project);
            var expenses = db.Expenses.Include(e => e.AccountType).Include(e => e.Employee).Include(e => e.Project);

            if (ClientID != -1)
            {
                workings = workings.Where(w => w.Project.FK_Client.ClientID == ClientID);
                expenses = expenses.Where(w => w.Project.FK_Client.ClientID == ClientID);
            }

            if (ProjectID != null)
            {
                workings = workings.Where(w => w.ProjectID.Equals(ProjectID));
                expenses = expenses.Where(w => w.ProjectID.Equals(ProjectID));
            }


            model.MixedWorkings = workings
                .OrderByDescending(o => o.Date);


            model.IndividualWorkings = model.MixedWorkings
                .GroupBy(g => g.EmployeeID)
                .Select(s => new SummaryWorkingModel
                {
                    Name = s.FirstOrDefault().Employee.FullName,
                    Hours = s.Sum(c => c.Hours),
                    Bank = s.Sum(c => c.Bank),
                    OverTime = s.Sum(c => c.OT),
                }).ToList();

            model.ProjectExpenses = expenses.ToList();

 



            var hourSum = model.IndividualWorkings.Sum(s => s.Hours);
            var bankSum = model.IndividualWorkings.Sum(s => s.Bank);
            var otSum = model.IndividualWorkings.Sum(s => s.OverTime);
            var amountSum = model.ProjectExpenses.Sum(s => s.Amount);

            ViewBag.HourSum = hourSum;
            ViewBag.BankSum = bankSum;
            ViewBag.OTSum = otSum;
            ViewBag.AmountSum = amountSum;
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            

            return View(model);
        }
    }
}