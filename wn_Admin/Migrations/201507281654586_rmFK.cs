namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeOffRequests", "OffReasonID", "dbo.OffReasons");
            DropForeignKey("dbo.Workings", "OffReason", "dbo.OffReasons");
            DropForeignKey("dbo.Workings", "Task", "dbo.Tasks");
            DropIndex("dbo.TimeOffRequests", new[] { "OffReasonID" });
            DropIndex("dbo.Workings", new[] { "Task" });
            DropIndex("dbo.Workings", new[] { "OffReason" });
            //AlterColumn("dbo.TimeOffRequests", "OffReasonID", c => c.String());
            //AlterColumn("dbo.Workings", "Task", c => c.String());
            //AlterColumn("dbo.Workings", "OffReason", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workings", "OffReason", c => c.Int(nullable: false));
            AlterColumn("dbo.Workings", "Task", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeOffRequests", "OffReasonID", c => c.Int(nullable: false));
            CreateIndex("dbo.Workings", "OffReason");
            CreateIndex("dbo.Workings", "Task");
            CreateIndex("dbo.TimeOffRequests", "OffReasonID");
            AddForeignKey("dbo.Workings", "Task", "dbo.Tasks", "TaskID");
            AddForeignKey("dbo.Workings", "OffReason", "dbo.OffReasons", "OffReasonID");
            AddForeignKey("dbo.TimeOffRequests", "OffReasonID", "dbo.OffReasons", "OffReasonID");
        }
    }
}
