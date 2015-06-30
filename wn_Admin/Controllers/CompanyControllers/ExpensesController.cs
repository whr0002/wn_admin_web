using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.CompanyModels;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PagedList;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.CompanyControllers
{
    [Authorize(Roles = "SUPERADMIN, Accountant, Employee")]
    public class ExpensesController : Controller
    {
        private wn_admin_db db = new wn_admin_db();

        // GET: Expenses
        public ActionResult Index(int? page)
        {

            var expenses = db.Expenses.Include(e => e.AccountType).Include(e => e.Employee).Include(e => e.Project);
            

            UserInfo ui = new UserInfo();
            var role = ui.getFirstRole(User.Identity.GetUserId());
            

            

            if (role.Equals("Accountant") || role.Equals("SUPERADMIN"))
            {
                ViewBag.hasFullControl = true;
            }
            else
            {
                var employee = ui.getEmployee(User.Identity.GetUserId());
                if (employee != null) { 
                    var eid = employee.EmployeeID;
                    expenses = expenses.Where(w => w.EmployeeID == eid);
                }
                else
                {
                    return View(new List<Expense>());
                }
                ViewBag.hasFullControl = false;
            }


            expenses = expenses.OrderByDescending(o => o.DateSubmitted);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(expenses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Expenses/Details/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {


            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "AccountName");
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName");
            setEmployeeDropdowns();
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");


            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseID,EmployeeID,ProjectID,AccountTypeID,DateSubmitted,Item,ReceiptDate,Amount,ReceiptLink,isApproved")] Expense expense, HttpPostedFileBase attachment)
        {
            if (ModelState.IsValid)
            {
                string filePath = storeFileToAzure(attachment);
                if (filePath != null)
                {
                    expense.ReceiptLink = filePath;
                }

                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "AccountName", expense.AccountTypeID);
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", expense.EmployeeID);
            setEmployeeDropdowns();
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", expense.ProjectID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "AccountName", expense.AccountTypeID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", expense.EmployeeID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", expense.ProjectID);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseID,EmployeeID,ProjectID,AccountTypeID,DateSubmitted,Item,ReceiptDate,Amount,ReceiptLink,isApproved")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "AccountName", expense.AccountTypeID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstMidName", expense.EmployeeID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", expense.ProjectID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [Authorize(Roles = "Accountant, SUPERADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string storeFileToAzure(HttpPostedFileBase file)
        {
            

            if (file != null && file.ContentLength > 0)
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient client = account.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference("receipts");

                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                DateTime date = DateTime.Now;
                string timestamp = date.ToString("yyyy_MM_dd_HH_mm_ss");
                CloudBlockBlob blob = container.GetBlockBlobReference(timestamp+Path.GetExtension(file.FileName));
                blob.UploadFromStream(file.InputStream);

                return blob.Uri.ToString();
            }

            return null;

        }

        [Authorize(Roles = "Accountant, SUPERADMIN")]
        public ActionResult review(string ids)
        {

            string[] sids = ids.Split(',');
            UserInfo ui = new UserInfo();
            var role = ui.getFirstRole(User.Identity.GetUserId());

           
            if (role.Equals("Accountant") || role.Equals("SUPERADMIN"))
            {

                List<int> idList = new List<int>();

                foreach(var s in sids){
                    try{

                        int id = Convert.ToInt32(s);
                        idList.Add(id);

                    }catch{}
                }

                //Response.Write("List Size: " + idList.Count() + "<br />");

                // Get expenses based on IDs
                foreach (int mid in idList)
                {
                    //Response.Write("ID: " + mid + "<br />");
                    var expense = db.Expenses.Where(w => w.ExpenseID == mid).FirstOrDefault();
                    expense.isApproved = true;
                    db.SaveChanges();
                }
                

            }

            return RedirectToAction("Index");
        }

        private void setEmployeeDropdowns()
        {
            UserInfo ui = new UserInfo();
            string userId = User.Identity.GetUserId();
            var employee = ui.getEmployee(userId);
            var role = ui.getFirstRole(userId);

            if (employee != null)
            {
                IQueryable<Employee> es = db.Employees;

                if (!role.Equals("Accountant") && !role.Equals("SUPERADMIN"))
                {
                    es = es.Where(w => w.EmployeeID == employee.EmployeeID);
                }

                ViewBag.EmployeeID = new SelectList(es, "EmployeeID", "FullName");

            }
            else
            {
                ViewBag.EmployeeID = new SelectList(new List<Employee>(), "EmployeeID", "FullName");
            }
        }
    }
}
