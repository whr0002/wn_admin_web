namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "Task", c => c.String(maxLength: 128));
            AddColumn("dbo.Workings", "Identifier", c => c.String());
            AddColumn("dbo.Workings", "Veh", c => c.String(maxLength: 128));
            AddColumn("dbo.Workings", "Crew", c => c.String());
            AddColumn("dbo.Workings", "StartKm", c => c.Int());
            AddColumn("dbo.Workings", "EndKm", c => c.Int());
            AddColumn("dbo.Workings", "GPS", c => c.Boolean(nullable: false));
            AddColumn("dbo.Workings", "Field", c => c.String(maxLength: 128));
            AddColumn("dbo.Workings", "PD", c => c.Boolean(nullable: false));
            AddColumn("dbo.Workings", "JobDescription", c => c.String());
            AddColumn("dbo.Workings", "Off", c => c.String(maxLength: 128));
            AddColumn("dbo.Workings", "Bank", c => c.Int());
            AddColumn("dbo.Workings", "OT", c => c.Int());
            CreateIndex("dbo.Workings", "Task");
            CreateIndex("dbo.Workings", "Veh");
            CreateIndex("dbo.Workings", "Field");
            CreateIndex("dbo.Workings", "Off");
            AddForeignKey("dbo.Workings", "Field", "dbo.FieldAccesses", "FieldAccessID");
            AddForeignKey("dbo.Workings", "Off", "dbo.Tasks", "TaskID");
            AddForeignKey("dbo.Workings", "Task", "dbo.Tasks", "TaskID");
            AddForeignKey("dbo.Workings", "Veh", "dbo.Vehicles", "VehicleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workings", "Veh", "dbo.Vehicles");
            DropForeignKey("dbo.Workings", "Task", "dbo.Tasks");
            DropForeignKey("dbo.Workings", "Off", "dbo.Tasks");
            DropForeignKey("dbo.Workings", "Field", "dbo.FieldAccesses");
            DropIndex("dbo.Workings", new[] { "Off" });
            DropIndex("dbo.Workings", new[] { "Field" });
            DropIndex("dbo.Workings", new[] { "Veh" });
            DropIndex("dbo.Workings", new[] { "Task" });
            DropColumn("dbo.Workings", "OT");
            DropColumn("dbo.Workings", "Bank");
            DropColumn("dbo.Workings", "Off");
            DropColumn("dbo.Workings", "JobDescription");
            DropColumn("dbo.Workings", "PD");
            DropColumn("dbo.Workings", "Field");
            DropColumn("dbo.Workings", "GPS");
            DropColumn("dbo.Workings", "EndKm");
            DropColumn("dbo.Workings", "StartKm");
            DropColumn("dbo.Workings", "Crew");
            DropColumn("dbo.Workings", "Veh");
            DropColumn("dbo.Workings", "Identifier");
            DropColumn("dbo.Workings", "Task");
        }
    }
}
