namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dlkjsaflkn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeSafetyMeetings", "ProjectID", "dbo.Projects");
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "ProjectID" });
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            AddColumn("dbo.SafetyMeetings", "ProjectID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID" });
            CreateIndex("dbo.SafetyMeetings", "ProjectID");
            AddForeignKey("dbo.SafetyMeetings", "ProjectID", "dbo.Projects", "ProjectID");
            DropColumn("dbo.EmployeeSafetyMeetings", "ProjectID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeSafetyMeetings", "ProjectID", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.SafetyMeetings", "ProjectID", "dbo.Projects");
            DropIndex("dbo.SafetyMeetings", new[] { "ProjectID" });
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            DropColumn("dbo.SafetyMeetings", "ProjectID");
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID", "ProjectID" });
            CreateIndex("dbo.EmployeeSafetyMeetings", "ProjectID");
            AddForeignKey("dbo.EmployeeSafetyMeetings", "ProjectID", "dbo.Projects", "ProjectID");
        }
    }
}
