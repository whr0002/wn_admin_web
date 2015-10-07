namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueklds : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmployeeRoles", "FK_ERE");
            DropIndex("dbo.EmployeeRoles", "FK_ERR");
            DropIndex("dbo.WorkingSupervisors", "FK_WSW");
            DropIndex("dbo.WorkingSupervisors", "FK_WSS");
            CreateIndex("dbo.EmployeeRoles", "EmployeeID");
            CreateIndex("dbo.WorkingSupervisors", "WorkingID");
            CreateIndex("dbo.WorkingSupervisors", "EmployeeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.WorkingSupervisors", new[] { "EmployeeID" });
            DropIndex("dbo.WorkingSupervisors", new[] { "WorkingID" });
            DropIndex("dbo.EmployeeRoles", new[] { "EmployeeID" });
            CreateIndex("dbo.WorkingSupervisors", "EmployeeID", unique: true, name: "FK_WSS");
            CreateIndex("dbo.WorkingSupervisors", "WorkingID", unique: true, name: "FK_WSW");
            CreateIndex("dbo.EmployeeRoles", "RoleID", unique: true, name: "FK_ERR");
            CreateIndex("dbo.EmployeeRoles", "EmployeeID", unique: true, name: "FK_ERE");
        }
    }
}
