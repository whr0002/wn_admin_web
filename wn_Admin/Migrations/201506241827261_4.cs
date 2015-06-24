namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEmployees",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEmployees", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.UserEmployees", new[] { "EmployeeID" });
            DropTable("dbo.UserEmployees");
        }
    }
}
