namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsaklfnmqe123456 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.SafetyMeetings");
            AddColumn("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID", c => c.Int());
            AddColumn("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID", c => c.Int());
            CreateIndex("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID");
            CreateIndex("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID", "dbo.SafetyMeetings", "SafetyMeetingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID", "dbo.SafetyMeetings");
            DropForeignKey("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeeting_SafetyMeetingID" });
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "Employee_EmployeeID" });
            DropColumn("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID");
            DropColumn("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.SafetyMeetings", "SafetyMeetingID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.Employees", "EmployeeID");
        }
    }
}
