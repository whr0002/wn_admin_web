namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class so_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SafetyObservations", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.SafetyObservations", "Signed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyObservations", "Signed", c => c.Boolean(nullable: false));
            DropColumn("dbo.SafetyObservations", "Status");
        }
    }
}
