namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsaklfnmqe12345 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SafetyMeetings", "SafetyLeavingID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyMeetings", "SafetyLeavingID", c => c.Int());
        }
    }
}
