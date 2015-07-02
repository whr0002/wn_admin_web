namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class safetyik : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeSafetyMeetings",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        SafetyMeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.SafetyMeetingID })
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.SafetyMeetings", t => t.SafetyMeetingID)
                .ForeignKey("dbo.SafetyMeetings", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.SafetyMeetingID);
            
            CreateTable(
                "dbo.SafetyMeetings",
                c => new
                    {
                        SafetyMeetingID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProjectID = c.String(maxLength: 128),
                        FieldLocation = c.String(),
                        SafeWorkPermitNum = c.String(),
                        ScopeOfWork = c.String(),
                        SafetyLeavingID = c.Int(),
                        IsReviewedBySafetyManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SafetyMeetingID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.SafetyLeavings",
                c => new
                    {
                        SafetyMeetingID = c.Int(nullable: false),
                        AllReqFE = c.String(maxLength: 15),
                        AllReqPPE = c.String(maxLength: 15),
                        TallySheets = c.String(maxLength: 15),
                        CommE = c.String(maxLength: 15),
                        TruckKit = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.SafetyMeetingID)
                .ForeignKey("dbo.SafetyMeetings", t => t.SafetyMeetingID)
                .Index(t => t.SafetyMeetingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.SafetyMeetings");
            DropForeignKey("dbo.SafetyLeavings", "SafetyMeetingID", "dbo.SafetyMeetings");
            DropForeignKey("dbo.SafetyMeetings", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.EmployeeSafetyMeetings", "SafetyMeetingID", "dbo.SafetyMeetings");
            DropForeignKey("dbo.EmployeeSafetyMeetings", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyMeetingID" });
            DropIndex("dbo.SafetyMeetings", new[] { "ProjectID" });
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "SafetyMeetingID" });
            DropIndex("dbo.EmployeeSafetyMeetings", new[] { "EmployeeID" });
            DropTable("dbo.SafetyLeavings");
            DropTable("dbo.SafetyMeetings");
            DropTable("dbo.EmployeeSafetyMeetings");
        }
    }
}
