namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ClientID)
                .Index(t => t.ClientName, unique: true);
            
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
                        Client = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Clients", t => t.Client)
                .Index(t => t.Client);
            
            CreateTable(
                "dbo.Workings",
                c => new
                    {
                        WorkingID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        ProjectID = c.String(maxLength: 128),
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
                        Hours = c.Double(nullable: false),
                        Bank = c.Int(),
                        OT = c.Int(),
                    })
                .PrimaryKey(t => t.WorkingID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.FieldAccesses", t => t.Field)
                .ForeignKey("dbo.Tasks", t => t.Off)
                .ForeignKey("dbo.Tasks", t => t.Task)
                .ForeignKey("dbo.Vehicles", t => t.Veh)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectID)
                .Index(t => t.Task)
                .Index(t => t.Veh)
                .Index(t => t.Field)
                .Index(t => t.Off);
            
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
                        FieldAccessID = c.Int(nullable: false, identity: true),
                        FieldAccessName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.FieldAccessID)
                .Index(t => t.FieldAccessName, unique: true);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.TaskID)
                .Index(t => t.TaskName, unique: true);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.VehicleID)
                .Index(t => t.VehicleName, unique: true);
            
            CreateTable(
                "dbo.OffReasons",
                c => new
                    {
                        OffReasonID = c.Int(nullable: false, identity: true),
                        OffReasonName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.OffReasonID)
                .Index(t => t.OffReasonName, unique: true);
            
            CreateTable(
                "dbo.PayPeriods",
                c => new
                    {
                        PayPeriodID = c.Int(nullable: false),
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
                "dbo.WorksFors",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeID, t.DepartmentID })
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Timesheets", "Veh", "dbo.Vehicles");
            DropForeignKey("dbo.Timesheets", "Task", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Off", "dbo.Tasks");
            DropForeignKey("dbo.Timesheets", "Field", "dbo.FieldAccesses");
            DropForeignKey("dbo.Timesheets", "Client", "dbo.Clients");
            DropForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees");
            DropForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Controls", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Workings", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Workings", "Veh", "dbo.Vehicles");
            DropForeignKey("dbo.Workings", "Task", "dbo.Tasks");
            DropForeignKey("dbo.Workings", "Off", "dbo.Tasks");
            DropForeignKey("dbo.Workings", "Field", "dbo.FieldAccesses");
            DropForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Client", "dbo.Clients");
            DropForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.WorksFors", new[] { "DepartmentID" });
            DropIndex("dbo.WorksFors", new[] { "EmployeeID" });
            DropIndex("dbo.Timesheets", new[] { "Off" });
            DropIndex("dbo.Timesheets", new[] { "Field" });
            DropIndex("dbo.Timesheets", new[] { "Veh" });
            DropIndex("dbo.Timesheets", new[] { "Task" });
            DropIndex("dbo.Timesheets", new[] { "Client" });
            DropIndex("dbo.Supervisions", new[] { "SupervisorID" });
            DropIndex("dbo.Supervisions", new[] { "EmployeeID" });
            DropIndex("dbo.OffReasons", new[] { "OffReasonName" });
            DropIndex("dbo.Vehicles", new[] { "VehicleName" });
            DropIndex("dbo.Tasks", new[] { "TaskName" });
            DropIndex("dbo.FieldAccesses", new[] { "FieldAccessName" });
            DropIndex("dbo.Workings", new[] { "Off" });
            DropIndex("dbo.Workings", new[] { "Field" });
            DropIndex("dbo.Workings", new[] { "Veh" });
            DropIndex("dbo.Workings", new[] { "Task" });
            DropIndex("dbo.Workings", new[] { "ProjectID" });
            DropIndex("dbo.Workings", new[] { "EmployeeID" });
            DropIndex("dbo.Projects", new[] { "Client" });
            DropIndex("dbo.Controls", new[] { "DepartmentID" });
            DropIndex("dbo.Controls", new[] { "ProjectID" });
            DropIndex("dbo.Clients", new[] { "ClientName" });
            DropTable("dbo.WorksFors");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Supervisions");
            DropTable("dbo.PayPeriods");
            DropTable("dbo.OffReasons");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Tasks");
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
