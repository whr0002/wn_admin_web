namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmaccess : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workings", "Field", "dbo.FieldAccesses");
            DropForeignKey("dbo.Workings", "Veh", "dbo.Vehicles");
            DropIndex("dbo.Workings", new[] { "Veh" });
            DropIndex("dbo.Workings", new[] { "Field" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Workings", "Field");
            CreateIndex("dbo.Workings", "Veh");
            AddForeignKey("dbo.Workings", "Veh", "dbo.Vehicles", "VehicleID");
            AddForeignKey("dbo.Workings", "Field", "dbo.FieldAccesses", "FieldAccessID");
        }
    }
}
