namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _389257 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workings", "ClientName", "dbo.Clients");
            DropForeignKey("dbo.Timesheets", "Client", "dbo.Clients");
            DropForeignKey("dbo.Timesheets", "Field", "dbo.FieldAccesses");
            DropForeignKey("dbo.Timesheets", "Off", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Task", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Veh", "dbo.Vehicles");
            DropIndex("dbo.Workings", new[] { "ClientName" });
            DropIndex("dbo.Timesheets", new[] { "Client" });
            DropIndex("dbo.Timesheets", new[] { "Task" });
            DropIndex("dbo.Timesheets", new[] { "Veh" });
            DropIndex("dbo.Timesheets", new[] { "Field" });
            DropIndex("dbo.Timesheets", new[] { "Off" });
            DropColumn("dbo.Workings", "ClientName");
            DropTable("dbo.Timesheets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        TimesheetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        PPyr = c.Int(),
                        PP = c.Int(),
                        Client = c.Int(nullable: false),
                        Project = c.String(),
                        ProjectID = c.String(),
                        Task = c.Int(nullable: false),
                        Identifier = c.String(),
                        Veh = c.Int(nullable: false),
                        Crew = c.String(),
                        StartKm = c.Int(),
                        EndKm = c.Int(),
                        GPS = c.Boolean(nullable: false),
                        Field = c.Int(nullable: false),
                        PD = c.Boolean(nullable: false),
                        JobDescription = c.String(),
                        Off = c.Int(nullable: false),
                        Hours = c.Int(nullable: false),
                        Bank = c.Int(),
                        OT = c.Int(),
                    })
                .PrimaryKey(t => t.TimesheetID);
            
            AddColumn("dbo.Workings", "ClientName", c => c.Int(nullable: false));
            CreateIndex("dbo.Timesheets", "Off");
            CreateIndex("dbo.Timesheets", "Field");
            CreateIndex("dbo.Timesheets", "Veh");
            CreateIndex("dbo.Timesheets", "Task");
            CreateIndex("dbo.Timesheets", "Client");
            CreateIndex("dbo.Workings", "ClientName");
            AddForeignKey("dbo.Timesheets", "Veh", "dbo.Vehicles", "VehicleID");
            AddForeignKey("dbo.Timesheets", "Task", "dbo.Tasks", "TaskID");
            AddForeignKey("dbo.Timesheets", "Off", "dbo.Tasks", "TaskID");
            AddForeignKey("dbo.Timesheets", "Field", "dbo.FieldAccesses", "FieldAccessID");
            AddForeignKey("dbo.Timesheets", "Client", "dbo.Clients", "ClientID");
            AddForeignKey("dbo.Workings", "ClientName", "dbo.Clients", "ClientID");
        }
    }
}
