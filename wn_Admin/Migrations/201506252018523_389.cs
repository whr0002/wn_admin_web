namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _389 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "isReviewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workings", "isReviewed");
        }
    }
}
