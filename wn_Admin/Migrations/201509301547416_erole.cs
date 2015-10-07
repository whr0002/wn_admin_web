namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeRoles", "RoleID", "dbo.Roles");
            CreateTable(
                "dbo.ERoles",
                c => new
                    {
                        ERoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ERoleID);
            
            AddColumn("dbo.EmployeeRoles", "Role_ERoleID", c => c.Int());
            CreateIndex("dbo.EmployeeRoles", "Role_ERoleID");
            AddForeignKey("dbo.EmployeeRoles", "Role_ERoleID", "dbo.ERoles", "ERoleID");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            DropForeignKey("dbo.EmployeeRoles", "Role_ERoleID", "dbo.ERoles");
            DropIndex("dbo.EmployeeRoles", new[] { "Role_ERoleID" });
            DropColumn("dbo.EmployeeRoles", "Role_ERoleID");
            DropTable("dbo.ERoles");
            AddForeignKey("dbo.EmployeeRoles", "RoleID", "dbo.Roles", "RoleID");
        }
    }
}
