namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dlksagm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                    })
                .PrimaryKey(t => t.AccountTypeID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ProjectID = c.String(nullable: false, maxLength: 128),
                        AccountTypeID = c.Int(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        Item = c.String(),
                        ReceiptDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceiptLink = c.String(),
                        isApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseID)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectID)
                .Index(t => t.AccountTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Expenses", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Expenses", "AccountTypeID", "dbo.AccountTypes");
            DropIndex("dbo.Expenses", new[] { "AccountTypeID" });
            DropIndex("dbo.Expenses", new[] { "ProjectID" });
            DropIndex("dbo.Expenses", new[] { "EmployeeID" });
            DropTable("dbo.Expenses");
            DropTable("dbo.AccountTypes");
        }
    }
}
