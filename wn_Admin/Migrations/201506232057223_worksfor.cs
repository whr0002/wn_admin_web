namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class worksfor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorksFors",
                c => new
                    {
                        WorksForID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorksForID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.WorksFors", new[] { "DepartmentID" });
            DropIndex("dbo.WorksFors", new[] { "EmployeeID" });
            DropTable("dbo.WorksFors");
        }
    }
}
