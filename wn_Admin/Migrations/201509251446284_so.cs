namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class so : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SafetyObservations",
                c => new
                    {
                        SafetyObservationID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        Crew = c.String(),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(),
                        Observation = c.String(),
                        Description = c.String(),
                        Investigation = c.String(),
                        KeyState = c.String(),
                        CriticalError = c.String(),
                        Solution = c.String(),
                        DateAction = c.DateTime(nullable: false),
                        Signed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SafetyObservationID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SafetyObservations", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.SafetyObservations", new[] { "EmployeeID" });
            DropTable("dbo.SafetyObservations");
        }
    }
}
