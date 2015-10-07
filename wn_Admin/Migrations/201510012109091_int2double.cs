namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int2double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workings", "Bank", c => c.Double());
            AlterColumn("dbo.Workings", "OT", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workings", "OT", c => c.Int());
            AlterColumn("dbo.Workings", "Bank", c => c.Int());
        }
    }
}
