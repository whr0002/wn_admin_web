namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailphone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Phone");
            DropColumn("dbo.Employees", "Email");
        }
    }
}
