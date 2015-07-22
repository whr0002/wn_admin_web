namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kmdiff2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Workings", "KmDiff");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workings", "KmDiff", c => c.Int(nullable: false));
        }
    }
}
