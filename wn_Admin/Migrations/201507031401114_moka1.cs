namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moka1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SafetyMeetings", "ProjectID", "dbo.Projects");
            DropIndex("dbo.SafetyMeetings", new[] { "ProjectID" });
            CreateTable(
                "dbo.SafetyLeavingItems",
                c => new
                    {
                        SafetyLeavingItemID = c.Int(nullable: false, identity: true),
                        SafetyLeavingItemName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SafetyLeavingItemID)
                .Index(t => t.SafetyLeavingItemName, unique: true);
            
            CreateTable(
                "dbo.YesNoNAs",
                c => new
                    {
                        YesNoNAID = c.Int(nullable: false, identity: true),
                        YesNoNAName = c.String(),
                    })
                .PrimaryKey(t => t.YesNoNAID);
            
            DropColumn("dbo.SafetyMeetings", "ProjectID");
            DropColumn("dbo.SafetyLeavings", "ItemName");
            DropColumn("dbo.SafetyLeavings", "ItemStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyLeavings", "ItemStatus", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.SafetyLeavings", "ItemName", c => c.String(maxLength: 50));
            AddColumn("dbo.SafetyMeetings", "ProjectID", c => c.String(maxLength: 128));
            DropIndex("dbo.SafetyLeavingItems", new[] { "SafetyLeavingItemName" });
            DropTable("dbo.YesNoNAs");
            DropTable("dbo.SafetyLeavingItems");
            CreateIndex("dbo.SafetyMeetings", "ProjectID");
            AddForeignKey("dbo.SafetyMeetings", "ProjectID", "dbo.Projects", "ProjectID");
        }
    }
}
