namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayPeriods",
                c => new
                    {
                        PayPeriodID = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PayPeriodID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PayPeriods");
        }
    }
}
