namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dlksjfsdfjlkwessaklfnddssadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SafetyLeavings", "SafetyLeavingItemID", c => c.Int());
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
            AddForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems", "SafetyLeavingItemID");
            DropColumn("dbo.SafetyLeavings", "SafetyItemName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyLeavings", "SafetyItemName", c => c.String());
            DropForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems");
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            DropColumn("dbo.SafetyLeavings", "SafetyLeavingItemID");
        }
    }
}
