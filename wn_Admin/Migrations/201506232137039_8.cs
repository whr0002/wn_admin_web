namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees");
            DropForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees");
            DropPrimaryKey("dbo.WorksFors");
            AddPrimaryKey("dbo.WorksFors", new[] { "EmployeeID", "DepartmentID" });
            AddForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
            AddForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
            AddForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees", "EmployeeID", cascadeDelete: false);
            AddForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
            DropColumn("dbo.WorksFors", "WorksForID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorksFors", "WorksForID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees");
            DropForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments");
            DropPrimaryKey("dbo.WorksFors");
            AddPrimaryKey("dbo.WorksFors", "WorksForID");
            AddForeignKey("dbo.WorksFors", "EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.WorksFors", "DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.Supervisions", "SupervisorID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.Supervisions", "EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.Workings", "EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.Controls", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
    }
}
