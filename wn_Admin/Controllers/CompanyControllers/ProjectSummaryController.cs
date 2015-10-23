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
    [Authorize(Roles="SUPERADMIN,Accountant")]
    public class ProjectSummaryController : Controller
    {
        private wn_admin_db  db = new wn_admin_db();

        // GET: ProjectSummary
        public ActionResult Index(int[] employeeId, int[] ClientID, string[] ProjectID, DateTime? startDate = null, DateTime? endDate = null)
        {
            SummaryModel model = new SummaryModel();
            var workings = db.Workings.Include(w => w.Employee).Include(w => w.Project);
            var expenses = db.Expenses.Include(e => e.AccountType).Include(e => e.Employee).Include(e => e.Project);

            if (employeeId != null && employeeId.Count() > 0)
            {
                workings = workings.Where(w => employeeId.Contains(w.EmployeeID));
            }

            if (ClientID == null && ProjectID == null && startDate == null && endDate == null)
            {
                workings = Enumerable.Empty<Working>().AsQueryable();
                expenses = Enumerable.Empty<Expense>().AsQueryable();
            }

            if (ClientID != null && ClientID.Count() > 0)
            {
                workings = workings.Where(w => ClientID.Contains(w.Project.Client));
                expenses = expenses.Where(w => ClientID.Contains(w.Project.Client));
            }

            if (ProjectID != null && ProjectID.Count() > 0)
            {
                workings = workings.Where(w => ProjectID.Contains(w.ProjectID));
                expenses = expenses.Where(w => ProjectID.Contains(w.ProjectID));
            }

            if (startDate != null)
            {
                workings = workings.Where(w => w.Date >= startDate);
                expenses = expenses.Where(w => w.DateSubmitted >= startDate);
            }

            if (endDate != null)
            {
                workings = workings.Where(w => w.Date <= endDate);
                expenses = expenses.Where(w => w.DateSubmitted <= endDate);
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

            ViewBag.EmployeeID = new MultiSelectList(db.Employees.OrderBy(o => o.FirstMidName), "EmployeeID", "FullName");
            ViewBag.ClientID = new MultiSelectList(db.Clients.OrderBy(o => o.ClientName), "ClientID", "ClientName");
            ViewBag.ProjectID = new MultiSelectList(db.Projects.OrderBy(o => o.ProjectName), "ProjectID", "ProjectName");
            

            return View(model);
        }
    }
}