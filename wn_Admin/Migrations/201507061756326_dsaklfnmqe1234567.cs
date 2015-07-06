namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsaklfnmqe1234567 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeetingID", "EmployeeID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID" });
        }
    }
}
