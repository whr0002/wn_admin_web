namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ldknioweru : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AccidentTypes", new[] { "MajorAccidentForm_MajorAccidentFormID" });
            RenameColumn(table: "dbo.AccidentTypes", name: "MajorAccidentForm_MajorAccidentFormID", newName: "MajorAccidentFormID");
            AlterColumn("dbo.AccidentTypes", "MajorAccidentFormID", c => c.Int(nullable: false));
            CreateIndex("dbo.AccidentTypes", "MajorAccidentFormID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AccidentTypes", new[] { "MajorAccidentFormID" });
            AlterColumn("dbo.AccidentTypes", "MajorAccidentFormID", c => c.Int());
            RenameColumn(table: "dbo.AccidentTypes", name: "MajorAccidentFormID", newName: "MajorAccidentForm_MajorAccidentFormID");
            CreateIndex("dbo.AccidentTypes", "MajorAccidentForm_MajorAccidentFormID");
        }
    }
}
