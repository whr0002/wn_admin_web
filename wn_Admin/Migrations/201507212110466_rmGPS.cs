namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmGPS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "Equipment", c => c.String());
            DropColumn("dbo.Workings", "GPS");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workings", "GPS", c => c.Boolean(nullable: false));
            DropColumn("dbo.Workings", "Equipment");
        }
    }
}
