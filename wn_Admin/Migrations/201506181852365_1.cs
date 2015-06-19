namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.FieldAccesses",
                c => new
                    {
                        FieldAccessID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FieldAccessID);
            
            CreateTable(
                "dbo.OffReasons",
                c => new
                    {
                        OffReasonID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.OffReasonID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.String(nullable: false, maxLength: 128),
                        ProjectName = c.String(),
                        Client = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Clients", t => t.Client)
                .Index(t => t.Client);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TaskID);
            
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        TimesheetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        PPyr = c.Int(),
                        PP = c.Int(),
                        Client = c.String(maxLength: 128),
                        Project = c.String(),
                        ProjectID = c.String(),
                        Task = c.String(maxLength: 128),
                        Identifier = c.String(),
                        Veh = c.String(maxLength: 128),
                        Crew = c.String(),
                        StartKm = c.Int(),
                        EndKm = c.Int(),
                        GPS = c.Boolean(nullable: false),
                        Field = c.String(maxLength: 128),
                        PD = c.Boolean(nullable: false),
                        JobDescription = c.String(),
                        Off = c.String(maxLength: 128),
                        Hours = c.Int(nullable: false),
                        Bank = c.Int(),
                        OT = c.Int(),
                    })
                .PrimaryKey(t => t.TimesheetID)
                .ForeignKey("dbo.Clients", t => t.Client)
                .ForeignKey("dbo.FieldAccesses", t => t.Field)
                .ForeignKey("dbo.Tasks", t => t.Off)
                .ForeignKey("dbo.Tasks", t => t.Task)
                .ForeignKey("dbo.Vehicles", t => t.Veh)
                .Index(t => t.Client)
                .Index(t => t.Task)
                .Index(t => t.Veh)
                .Index(t => t.Field)
                .Index(t => t.Off);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.VehicleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheets", "Veh", "dbo.Vehicles");
            DropForeignKey("dbo.Timesheets", "Task", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Off", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Field", "dbo.FieldAccesses");
            DropForeignKey("dbo.Timesheets", "Client", "dbo.Clients");
            DropForeignKey("dbo.Projects", "Client", "dbo.Clients");
            DropIndex("dbo.Timesheets", new[] { "Off" });
            DropIndex("dbo.Timesheets", new[] { "Field" });
            DropIndex("dbo.Timesheets", new[] { "Veh" });
            DropIndex("dbo.Timesheets", new[] { "Task" });
            DropIndex("dbo.Timesheets", new[] { "Client" });
            DropIndex("dbo.Projects", new[] { "Client" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.OffReasons");
            DropTable("dbo.FieldAccesses");
            DropTable("dbo.Clients");
        }
    }
}
