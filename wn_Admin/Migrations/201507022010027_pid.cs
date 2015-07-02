namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            AddColumn("dbo.EmployeeSafetyMeetings", "ProjectID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID", "ProjectID" });
            CreateIndex("dbo.EmployeeSafetyMeetings", "ProjectID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "ProjectID", "dbo.Projects", "ProjectID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeSafetyMeetings", "ProjectID", "dbo.Projects");
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "ProjectID" });
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            DropColumn("dbo.EmployeeSafetyMeetings", "ProjectID");
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID" });
        }
    }
}
