namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lota : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SafetyLeavings", "SafetyLeavingItemID", c => c.Int(nullable: false));
            AddColumn("dbo.SafetyLeavings", "YesNoNAID", c => c.Int(nullable: false));
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
            CreateIndex("dbo.SafetyLeavings", "YesNoNAID");
            AddForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems", "SafetyLeavingItemID");
            AddForeignKey("dbo.SafetyLeavings", "YesNoNAID", "dbo.YesNoNAs", "YesNoNAID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SafetyLeavings", "YesNoNAID", "dbo.YesNoNAs");
            DropForeignKey("dbo.SafetyLeavings", "SafetyLeavingItemID", "dbo.SafetyLeavingItems");
            DropIndex("dbo.SafetyLeavings", new[] { "YesNoNAID" });
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            DropColumn("dbo.SafetyLeavings", "YesNoNAID");
            DropColumn("dbo.SafetyLeavings", "SafetyLeavingItemID");
        }
    }
}
