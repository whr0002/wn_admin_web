namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsaklfnmqe12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SafetyMeetings", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.SafetyMeetings", "IsDone", c => c.Boolean(nullable: false));
            CreateIndex("dbo.SafetyMeetings", "EmployeeID");
            AddForeignKey("dbo.SafetyMeetings", "EmployeeID", "dbo.Employees", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SafetyMeetings", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.SafetyMeetings", new[] { "EmployeeID" });
            DropColumn("dbo.SafetyMeetings", "IsDone");
            DropColumn("dbo.SafetyMeetings", "EmployeeID");
        }
    }
}
