namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolopdsf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SafetyItems", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SafetyItems", "Description");
        }
    }
}
