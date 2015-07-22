namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kmdiff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "KmDiff", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workings", "KmDiff");
        }
    }
}
