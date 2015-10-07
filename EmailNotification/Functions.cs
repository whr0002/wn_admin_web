using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SendGrid;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System;
using Twilio;

namespace EmailNotification
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        // This function will enqueue a message on an Azure Queue called queue
        [NoAutomaticTrigger]
        public static async Task sendNotification()
        {
            await sendEmail();
            //sendSMS();
        }

        public static async Task sendEmail()
        {

            var message = new SendGridMessage();

            message.From = new MailAddress("Woodlandsnorth99@gmail.com");

            List<string> recipients = getEmailList();

            message.AddTo(recipients);

            message.Subject = "ASAP Timesheets & Review";

            message.Html = "<p>Hi Everyone, </p><br />" +
                            "<p>It’s a gentle reminder that please complete and review (if supervisor) your timesheets by 11 pm tonight (Sunday).<br />" +
                            "You can manage your timesheets by logging in to <a href='https://wnasap.azurewebsites.net/'>https://wnasap.azurewebsites.net/</a>.<br />" +
                            "If you have any difficulty in accessing your account, please contact the office.</p><br />" +
                            "<p>Thanks,<br />Woodlands North Admin</p>";

            var sendUsername = EmailNotification.Properties.Settings.Default.SENDGRID_USER;
            var sendPass = EmailNotification.Properties.Settings.Default.SENDGRID_PASS;

            var credentials = new NetworkCredential(sendUsername, sendPass);

            var transport = new Web(credentials);

            await transport.DeliverAsync(message);
        }

        public static void sendSMS() {

            var phoneList = getPhoneList();

            if (phoneList != null && phoneList.Count() > 0) {

                // Find your Account Sid and Auth Token at twilio.com/user/account 
                string AccountSid = EmailNotification.Properties.Settings.Default.TWILIO_ID;
                string AuthToken = EmailNotification.Properties.Settings.Default.TWILIO_TOKEN;
                var twilio = new TwilioRestClient(AccountSid, AuthToken);

                foreach (var phone in phoneList) {
                    twilio.SendMessage("+12017759599", "+1" + phone, "Please do not forget to review your time sheets for this week.");
                }
            }
        }

        private static List<string> getEmailList()
        {
            List<string> emails = new List<string>();
            string connectionString = EmailNotification.Properties.Settings.Default.DefaultConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open connection
                con.Open();

                using(SqlCommand command = new SqlCommand("SELECT Email FROM dbo.Employees WHERE Status != 0", con))
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0)) {
                            //Console.WriteLine("{0}", reader.GetString(0));
                            emails.Add(reader.GetString(0));
                        }
                    }                
                }
            }

            return emails;
        }

        private static List<string> getPhoneList()
        {
            List<string> phones = new List<string>();

            string connectionString = EmailNotification.Properties.Settings.Default.DefaultConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open connection
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT Phone FROM dbo.Employees WHERE Status != 0", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            //Console.WriteLine("{0}", reader.GetString(0));
                            phones.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return phones;
        }

        
    }
}
