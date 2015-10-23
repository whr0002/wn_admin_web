using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wn_Admin.Models;
using wn_Admin.Models.UtilityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace wn_Admin.Controllers.CControllers
{
    [Authorize()]
    public class EventDataController : Controller
    {
        private wn_admin_db db = new wn_admin_db();
        private UserInfo mUserInfo = new UserInfo();

        // GET: EventData
        public ActionResult Index(int[] employeeId, string[] tasks, int? page, DateTime? startDate = null, DateTime? endDate = null, int ppYear = -1, int pp = -1, int clientId = -1, string projectId = null, string isReviewed = null, Boolean? exportToExcel = null, Boolean? isGroupBy = null)
        {
            string userId = User.Identity.GetUserId();
            var employee = mUserInfo.getEmployee(userId);

            DataFilterService dfs = new DataFilterService();
            var dataFilterModel = dfs.filter(db, employeeId, tasks, page, startDate, endDate, ppYear, pp, clientId, projectId, isReviewed);
            var workings = dataFilterModel.Workings;

            EmployeeFormViewModel efvm = new EmployeeFormService().getEmployeeFormViewModel(db, userId, mUserInfo, employee, workings);

            EventService eventService = new EventService(employee,db, startDate, endDate, ppYear, pp);
            List<EventViewModel> evm = eventService.getEventViewModel(efvm.workings);


            return Json(evm, JsonRequestBehavior.AllowGet);
        }
    }
}