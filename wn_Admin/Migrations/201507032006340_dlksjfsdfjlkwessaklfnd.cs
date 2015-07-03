namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dlksjfsdfjlkwessaklfnd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems");
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            AddColumn("dbo.SafetyLeavings", "SafetyItemName", c => c.String());
            DropColumn("dbo.SafetyLeavings", "SafetyLeavingItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyLeavings", "SafetyLeavingItemID", c => c.Int());
            DropColumn("dbo.SafetyLeavings", "SafetyItemName");
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
            AddForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems", "SafetyLeavingItemID");
        }
    }
}
