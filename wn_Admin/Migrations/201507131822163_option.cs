namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class option : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReoccurOptions",
                c => new
                    {
                        ReoccurOptionID = c.Int(nullable: false, identity: true),
                        ReoccurOptionName = c.String(),
                    })
                .PrimaryKey(t => t.ReoccurOptionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReoccurOptions");
        }
    }
}
