namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "PPYr", c => c.Int(nullable: false));
            AddColumn("dbo.Workings", "PP", c => c.Int(nullable: false));
            AddColumn("dbo.Workings", "ClientName", c => c.Int(nullable: false));
            CreateIndex("dbo.Workings", "ClientName");
            AddForeignKey("dbo.Workings", "ClientName", "dbo.Clients", "ClientID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workings", "ClientName", "dbo.Clients");
            DropIndex("dbo.Workings", new[] { "ClientName" });
            DropColumn("dbo.Workings", "ClientName");
            DropColumn("dbo.Workings", "PP");
            DropColumn("dbo.Workings", "PPYr");
        }
    }
}
