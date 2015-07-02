namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeOff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeOffRequests",
                c => new
                    {
                        TimeOffRequestID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ReturnToWorkDate = c.DateTime(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                        OffReasonID = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.TimeOffRequestID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.OffReasons", t => t.OffReasonID)
                .Index(t => t.EmployeeID)
                .Index(t => t.OffReasonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeOffRequests", "OffReasonID", "dbo.OffReasons");
            DropForeignKey("dbo.TimeOffRequests", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.TimeOffRequests", new[] { "OffReasonID" });
            DropIndex("dbo.TimeOffRequests", new[] { "EmployeeID" });
            DropTable("dbo.TimeOffRequests");
        }
    }
}
