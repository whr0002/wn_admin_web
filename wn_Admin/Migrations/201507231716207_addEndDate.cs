namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workings", "EndDate");
        }
    }
}
