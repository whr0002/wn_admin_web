namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dskalfj : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SafetyLeavings");
            AddColumn("dbo.SafetyLeavings", "SafetyLeavingID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.SafetyLeavings", "ItemName", c => c.String(maxLength: 50));
            AddColumn("dbo.SafetyLeavings", "ItemStatus", c => c.String(nullable: false, maxLength: 15));
            AddPrimaryKey("dbo.SafetyLeavings", "SafetyLeavingID");
            DropColumn("dbo.SafetyLeavings", "AllReqFE");
            DropColumn("dbo.SafetyLeavings", "AllReqPPE");
            DropColumn("dbo.SafetyLeavings", "TallySheets");
            DropColumn("dbo.SafetyLeavings", "CommE");
            DropColumn("dbo.SafetyLeavings", "TruckKit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyLeavings", "TruckKit", c => c.String(maxLength: 15));
            AddColumn("dbo.SafetyLeavings", "CommE", c => c.String(maxLength: 15));
            AddColumn("dbo.SafetyLeavings", "TallySheets", c => c.String(maxLength: 15));
            AddColumn("dbo.SafetyLeavings", "AllReqPPE", c => c.String(maxLength: 15));
            AddColumn("dbo.SafetyLeavings", "AllReqFE", c => c.String(maxLength: 15));
            DropPrimaryKey("dbo.SafetyLeavings");
            DropColumn("dbo.SafetyLeavings", "ItemStatus");
            DropColumn("dbo.SafetyLeavings", "ItemName");
            DropColumn("dbo.SafetyLeavings", "SafetyLeavingID");
            AddPrimaryKey("dbo.SafetyLeavings", "SafetyMeetingID");
        }
    }
}
