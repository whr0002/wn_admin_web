namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SafetyItems",
                c => new
                    {
                        SafetyItemID = c.Int(nullable: false, identity: true),
                        SafetyMeetingID = c.Int(nullable: false),
                        SafetyItemName = c.String(),
                        YesNoNAID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SafetyItemID)
                .ForeignKey("dbo.SafetyMeetings", t => t.SafetyMeetingID)
                .ForeignKey("dbo.YesNoNAs", t => t.YesNoNAID)
                .Index(t => t.SafetyMeetingID)
                .Index(t => t.YesNoNAID);
            
            CreateTable(
                "dbo.SafetyItemValues",
                c => new
                    {
                        SafetyItemValueID = c.Int(nullable: false, identity: true),
                        SafetyItemValueName = c.String(maxLength: 100),
                        Category = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SafetyItemValueID)
                .Index(t => t.SafetyItemValueName, unique: true)
                .Index(t => t.Category, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SafetyItems", "YesNoNAID", "dbo.YesNoNAs");
            DropForeignKey("dbo.SafetyItems", "SafetyMeetingID", "dbo.SafetyMeetings");
            DropIndex("dbo.SafetyItemValues", new[] { "Category" });
            DropIndex("dbo.SafetyItemValues", new[] { "SafetyItemValueName" });
            DropIndex("dbo.SafetyItems", new[] { "YesNoNAID" });
            DropIndex("dbo.SafetyItems", new[] { "SafetyMeetingID" });
            DropTable("dbo.SafetyItemValues");
            DropTable("dbo.SafetyItems");
        }
    }
}
