using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Configuration;
using System.Collections;

namespace wn_Admin.Models.UtilityModels
{
    public class EmailNotificationService
    {
        private wn_admin_db db = new wn_admin_db();
        private ApplicationDbContext userDb = new ApplicationDbContext();
        public void sendReviewNotification()
        {
            var message = new SendGridMessage();

            message.From = new MailAddress("woodlandsnorth@hotmail.com");

            List<String> recipients = getEmailList();

            message.AddTo("whr0002@gmail.com");

            message.Subject = "Reminder for Reviewing Time Sheets";

            message.Text = "Please review time sheets for this week.";

            var credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["mailAccount"],
               ConfigurationManager.AppSettings["mailPassword"]);

            var transport = new Web(credentials);

            transport.DeliverAsync(message);


        }

        private List<String> getEmailList()
        {
            var userIds = from er in db.Employees
                          join ue in db.UserEmployees
                          on er.EmployeeID equals ue.EmployeeID
                          where er.Status == 1
                          select ue.UserID;

            var userList = userDb.Users.ToList();
            var hashtable = new Hashtable();
            var emailList = new List<String>();

            foreach (var user in userList)
            {
                hashtable[user.Id] = user.Email;
            }

            foreach (var uid in userIds)
            {
                var email = hashtable[uid];
                if (email != null)
                {
                    emailList.Add(email.ToString());
                }
            }

            return emailList;
        }
    }
  
}