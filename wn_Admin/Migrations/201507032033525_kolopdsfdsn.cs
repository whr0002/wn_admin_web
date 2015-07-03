namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolopdsfdsn : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SafetyItemValues", new[] { "Category" });
            CreateTable(
                "dbo.SafetyCategories",
                c => new
                    {
                        SafetyCategoryID = c.Int(nullable: false, identity: true),
                        SafetyCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.SafetyCategoryID);
            
            AddColumn("dbo.SafetyItems", "SafetyCategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.SafetyItemValues", "SafetyCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.SafetyItems", "SafetyCategoryID");
            CreateIndex("dbo.SafetyItemValues", "SafetyCategoryID");
            AddForeignKey("dbo.SafetyItems", "SafetyCategoryID", "dbo.SafetyCategories", "SafetyCategoryID");
            AddForeignKey("dbo.SafetyItemValues", "SafetyCategoryID", "dbo.SafetyCategories", "SafetyCategoryID");
            DropColumn("dbo.SafetyItemValues", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SafetyItemValues", "Category", c => c.String(maxLength: 100));
            DropForeignKey("dbo.SafetyItemValues", "SafetyCategoryID", "dbo.SafetyCategories");
            DropForeignKey("dbo.SafetyItems", "SafetyCategoryID", "dbo.SafetyCategories");
            DropIndex("dbo.SafetyItemValues", new[] { "SafetyCategoryID" });
            DropIndex("dbo.SafetyItems", new[] { "SafetyCategoryID" });
            DropColumn("dbo.SafetyItemValues", "SafetyCategoryID");
            DropColumn("dbo.SafetyItems", "SafetyCategoryID");
            DropTable("dbo.SafetyCategories");
            CreateIndex("dbo.SafetyItemValues", "Category", unique: true);
        }
    }
}
