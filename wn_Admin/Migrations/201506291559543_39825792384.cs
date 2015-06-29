namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _39825792384 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Workings", new[] { "ProjectID" });
            AlterColumn("dbo.Workings", "ProjectID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Workings", "ProjectID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Workings", new[] { "ProjectID" });
            AlterColumn("dbo.Workings", "ProjectID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Workings", "ProjectID");
        }
    }
}
