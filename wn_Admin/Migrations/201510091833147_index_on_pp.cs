namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index_on_pp : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Workings", "PPYr");
            CreateIndex("dbo.Workings", "PP");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Workings", new[] { "PP" });
            DropIndex("dbo.Workings", new[] { "PPYr" });
        }
    }
}
