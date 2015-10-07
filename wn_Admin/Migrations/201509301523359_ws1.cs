namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ws1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkingSupervisors",
                c => new
                    {
                        WorkingSupervisorID = c.Int(nullable: false, identity: true),
                        WorkingID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkingSupervisorID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Workings", t => t.WorkingID)
                .Index(t => t.WorkingID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkingSupervisors", "WorkingID", "dbo.Workings");
            DropForeignKey("dbo.WorkingSupervisors", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.WorkingSupervisors", new[] { "EmployeeID" });
            DropIndex("dbo.WorkingSupervisors", new[] { "WorkingID" });
            DropTable("dbo.WorkingSupervisors");
        }
    }
}
