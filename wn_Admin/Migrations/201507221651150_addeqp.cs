namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeqp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        EquipmentID = c.Int(nullable: false, identity: true),
                        EquipmentName = c.String(),
                    })
                .PrimaryKey(t => t.EquipmentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Equipments");
        }
    }
}
