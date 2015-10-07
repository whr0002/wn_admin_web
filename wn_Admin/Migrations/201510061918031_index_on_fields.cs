namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index_on_fields : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Workings", "Date");
            CreateIndex("dbo.Workings", "EndDate");
            CreateIndex("dbo.Workings", "isReviewed");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Workings", new[] { "isReviewed" });
            DropIndex("dbo.Workings", new[] { "EndDate" });
            DropIndex("dbo.Workings", new[] { "Date" });
        }
    }
}
