namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ldknioweru1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccidentTypeValues",
                c => new
                    {
                        AccidentTypeValueID = c.Int(nullable: false, identity: true),
                        AccidentTypeValueName = c.String(),
                    })
                .PrimaryKey(t => t.AccidentTypeValueID);
            
            DropColumn("dbo.AccidentTypes", "AccidentTypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccidentTypes", "AccidentTypeName", c => c.String());
            DropTable("dbo.AccidentTypeValues");
        }
    }
}
