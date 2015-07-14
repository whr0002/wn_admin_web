namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class esm : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID" });
            //DropIndex("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeetingID" });
            //DropIndex("dbo.EmployeeSafetyMeetings", new[] { "Employee_EmployeeID" });
            //DropIndex("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeeting_SafetyMeetingID" });
            //DropColumn("dbo.EmployeeSafetyMeetings", "EmployeeID");
            //DropColumn("dbo.EmployeeSafetyMeetings", "SafetyMeetingID");
            //RenameColumn(table: "dbo.EmployeeSafetyMeetings", name: "Employee_EmployeeID", newName: "EmployeeID");
            //RenameColumn(table: "dbo.EmployeeSafetyMeetings", name: "SafetyMeeting_SafetyMeetingID", newName: "SafetyMeetingID");
            //DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            //AlterColumn("dbo.EmployeeSafetyMeetings", "EmployeeID", c => c.Int(nullable: false));
            //AlterColumn("dbo.EmployeeSafetyMeetings", "SafetyMeetingID", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID" });
            //CreateIndex("dbo.EmployeeSafetyMeetings", "EmployeeID");
            //CreateIndex("dbo.EmployeeSafetyMeetings", "SafetyMeetingID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeetingID" });
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID" });
            DropPrimaryKey("dbo.EmployeeSafetyMeetings");
            AlterColumn("dbo.EmployeeSafetyMeetings", "SafetyMeetingID", c => c.Int());
            AlterColumn("dbo.EmployeeSafetyMeetings", "EmployeeID", c => c.Int());
            AddPrimaryKey("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID", "SafetyMeetingID" });
            RenameColumn(table: "dbo.EmployeeSafetyMeetings", name: "SafetyMeetingID", newName: "SafetyMeeting_SafetyMeetingID");
            RenameColumn(table: "dbo.EmployeeSafetyMeetings", name: "EmployeeID", newName: "Employee_EmployeeID");
            AddColumn("dbo.EmployeeSafetyMeetings", "SafetyMeetingID", c => c.Int(nullable: false));
            AddColumn("dbo.EmployeeSafetyMeetings", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeSafetyMeetings", "SafetyMeeting_SafetyMeetingID");
            CreateIndex("dbo.EmployeeSafetyMeetings", "Employee_EmployeeID");
            CreateIndex("dbo.EmployeeSafetyMeetings", "SafetyMeetingID");
            CreateIndex("dbo.EmployeeSafetyMeetings", "EmployeeID");
        }
    }
}
