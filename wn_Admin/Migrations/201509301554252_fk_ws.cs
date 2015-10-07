namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_ws : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.WorkingSupervisors", new[] { "WorkingID" });
            DropIndex("dbo.WorkingSupervisors", new[] { "EmployeeID" });
            CreateIndex("dbo.WorkingSupervisors", "WorkingID", unique: true, name: "FK_WSW");
            CreateIndex("dbo.WorkingSupervisors", "EmployeeID", unique: true, name: "FK_WSS");
        }
        
        public override void Down()
        {
            DropIndex("dbo.WorkingSupervisors", "FK_WSS");
            DropIndex("dbo.WorkingSupervisors", "FK_WSW");
            CreateIndex("dbo.WorkingSupervisors", "EmployeeID");
            CreateIndex("dbo.WorkingSupervisors", "WorkingID");
        }
    }
}
