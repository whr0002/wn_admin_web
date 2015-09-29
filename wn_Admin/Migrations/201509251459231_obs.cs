namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class obs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Observations",
                c => new
                    {
                        ObservationID = c.Int(nullable: false, identity: true),
                        ObservationName = c.String(),
                    })
                .PrimaryKey(t => t.ObservationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Observations");
        }
    }
}
