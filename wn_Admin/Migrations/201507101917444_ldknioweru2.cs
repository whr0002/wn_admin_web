namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ldknioweru2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccidentTypes", "AccidentTypeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccidentTypes", "AccidentTypeName");
        }
    }
}
