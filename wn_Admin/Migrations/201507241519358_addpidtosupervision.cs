namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpidtosupervision : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Supervisions");
            AddColumn("dbo.Supervisions", "ProjectID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Supervisions", new[] { "EmployeeID", "SupervisorID", "ProjectID" });
            CreateIndex("dbo.Supervisions", "ProjectID");
            AddForeignKey("dbo.Supervisions", "ProjectID", "dbo.Projects", "ProjectID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supervisions", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Supervisions", new[] { "ProjectID" });
            DropPrimaryKey("dbo.Supervisions");
            DropColumn("dbo.Supervisions", "ProjectID");
            AddPrimaryKey("dbo.Supervisions", new[] { "EmployeeID", "SupervisorID" });
        }
    }
}
