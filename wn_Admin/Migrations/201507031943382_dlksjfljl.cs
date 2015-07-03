namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dlksjfljl : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            AlterColumn("dbo.SafetyLeavings", "SafetyLeavingItemID", c => c.Int());
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SafetyLeavings", new[] { "SafetyLeavingItemID" });
            AlterColumn("dbo.SafetyLeavings", "SafetyLeavingItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.SafetyLeavings", "SafetyLeavingItemID");
        }
    }
}
