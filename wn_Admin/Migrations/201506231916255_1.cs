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
                "dbo.Controls",
                c => new
                    {
                        ProjectID = c.String(nullable: false, maxLength: 128),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.ProjectID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
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
                "dbo.Workings",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        ProjectID = c.String(nullable: false, maxLength: 128),
                        Hours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.ProjectID })
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstMidName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
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
                "dbo.PayPeriods",
                c => new
                    {
                        PayPeriodID = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PayPeriodID);
            
            CreateTable(
                "dbo.Supervisions",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        SupervisorID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.SupervisorID })
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.SupervisorID)
                .Index(t => t.EmployeeID)
                .Index(t => t.SupervisorID);
            
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
            DropForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees");
            DropForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Controls", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Workings", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Client", "dbo.Clients");
            DropForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Timesheets", new[] { "Off" });
            DropIndex("dbo.Timesheets", new[] { "Field" });
            DropIndex("dbo.Timesheets", new[] { "Veh" });
            DropIndex("dbo.Timesheets", new[] { "Task" });
            DropIndex("dbo.Timesheets", new[] { "Client" });
            DropIndex("dbo.Supervisions", new[] { "SupervisorID" });
            DropIndex("dbo.Supervisions", new[] { "EmployeeID" });
            DropIndex("dbo.Workings", new[] { "ProjectID" });
            DropIndex("dbo.Workings", new[] { "EmployeeID" });
            DropIndex("dbo.Projects", new[] { "Client" });
            DropIndex("dbo.Controls", new[] { "DepartmentID" });
            DropIndex("dbo.Controls", new[] { "ProjectID" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Tasks");
            DropTable("dbo.Supervisions");
            DropTable("dbo.PayPeriods");
            DropTable("dbo.OffReasons");
            DropTable("dbo.FieldAccesses");
            DropTable("dbo.Employees");
            DropTable("dbo.Workings");
            DropTable("dbo.Projects");
            DropTable("dbo.Departments");
            DropTable("dbo.Controls");
            DropTable("dbo.Clients");
        }
    }
}
