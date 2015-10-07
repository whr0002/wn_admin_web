namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class off_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OffReasons", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OffReasons", "Status");
        }
    }
}
