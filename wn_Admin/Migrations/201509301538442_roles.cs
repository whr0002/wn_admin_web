namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                    {
                        EmployeeRoleID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeRoleID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.EmployeeID, unique: true, name: "FK_ERE")
                .Index(t => t.RoleID, unique: true, name: "FK_ERR");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.EmployeeRoles", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeRoles", "FK_ERR");
            DropIndex("dbo.EmployeeRoles", "FK_ERE");
            DropTable("dbo.Roles");
            DropTable("dbo.EmployeeRoles");
        }
    }
}
