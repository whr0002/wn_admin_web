namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsaklfnmqe1234 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems");
            DropForeignKey("dbo.SafetyLeavings", "SafetyMeetingID", "dbo.SafetyMeetings");
            DropForeignKey("dbo.SafetyLeavings", "YesNoNAID", "dbo.YesNoNAs");
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyMeetingID" });
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            DropIndex("dbo.SafetyLeavings", new[] { "YesNoNAID" });
            DropIndex("dbo.SafetyLeavingItems", new[] { "SafetyLeavingItemName" });
            DropTable("dbo.SafetyLeavings");
            DropTable("dbo.SafetyLeavingItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SafetyLeavingItems",
                c => new
                    {
                        SafetyLeavingItemID = c.Int(nullable: false, identity: true),
                        SafetyLeavingItemName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SafetyLeavingItemID);
            
            CreateTable(
                "dbo.SafetyLeavings",
                c => new
                    {
                        SafetyLeavingID = c.Int(nullable: false, identity: true),
                        SafetyMeetingID = c.Int(nullable: false),
                        SafetyLeavingItemID = c.Int(),
                        YesNoNAID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SafetyLeavingID);
            
            CreateIndex("dbo.SafetyLeavingItems", "SafetyLeavingItemName", unique: true);
            CreateIndex("dbo.SafetyLeavings", "YesNoNAID");
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
            CreateIndex("dbo.SafetyLeavings", "SafetyMeetingID");
            AddForeignKey("dbo.SafetyLeavings", "YesNoNAID", "dbo.YesNoNAs", "YesNoNAID");
            AddForeignKey("dbo.SafetyLeavings", "SafetyMeetingID", "dbo.SafetyMeetings", "SafetyMeetingID");
            AddForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems", "SafetyLeavingItemID");
        }
    }
}
