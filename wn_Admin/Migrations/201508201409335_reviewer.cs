namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workings", "Reviewer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workings", "Reviewer");
        }
    }
}
