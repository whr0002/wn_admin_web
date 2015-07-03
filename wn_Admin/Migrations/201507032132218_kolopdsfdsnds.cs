namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolopdsfdsnds : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SafetyItems", new[] { "YesNoNAID" });
            AlterColumn("dbo.SafetyItems", "YesNoNAID", c => c.Int());
            CreateIndex("dbo.SafetyItems", "YesNoNAID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SafetyItems", new[] { "YesNoNAID" });
            AlterColumn("dbo.SafetyItems", "YesNoNAID", c => c.Int(nullable: false));
            CreateIndex("dbo.SafetyItems", "YesNoNAID");
        }
    }
}
