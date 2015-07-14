namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MinorAccidentForms",
                c => new
                    {
                        MinorAccidentFormID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        Position = c.String(),
                        DateReported = c.DateTime(nullable: false),
                        LocationOfEvent = c.String(),
                        TaskConducted = c.String(),
                        AccidentType = c.String(),
                        RelatingTo = c.String(),
                        EventDesc = c.String(),
                        CauseAnalysis = c.String(),
                        KeyStates = c.String(),
                        CriticalErrors = c.String(),
                        FreqExpo = c.Int(nullable: false),
                        HazardProb = c.Int(nullable: false),
                        FirstAid = c.Boolean(nullable: false),
                        FirstAidDesc = c.String(),
                        CorrAction = c.String(),
                        PersonRespCorrAct = c.String(),
                        CorrActCompDate = c.DateTime(nullable: false),
                        FurtherActReq = c.String(),
                        isReviewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MinorAccidentFormID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinorAccidentForms", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.MinorAccidentForms", new[] { "EmployeeID" });
            DropTable("dbo.MinorAccidentForms");
        }
    }
}
