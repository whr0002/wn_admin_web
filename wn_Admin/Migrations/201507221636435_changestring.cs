namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workings", "Veh", c => c.String());
            AlterColumn("dbo.Workings", "Field", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workings", "Field", c => c.Int(nullable: false));
            AlterColumn("dbo.Workings", "Veh", c => c.Int(nullable: false));
        }
    }
}
