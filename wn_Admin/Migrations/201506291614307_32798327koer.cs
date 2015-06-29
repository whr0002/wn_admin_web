namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _32798327koer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workings", "JobDescription", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workings", "JobDescription", c => c.String());
        }
    }
}
