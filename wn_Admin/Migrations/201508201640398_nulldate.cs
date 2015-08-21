namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulldate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supervisions", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Supervisions", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supervisions", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Supervisions", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
