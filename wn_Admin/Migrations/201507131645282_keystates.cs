namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keystates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImmeCauses",
                c => new
                    {
                        ImmeCauseID = c.Int(nullable: false, identity: true),
                        ImmeCauseName = c.String(),
                    })
                .PrimaryKey(t => t.ImmeCauseID);
            
            CreateTable(
                "dbo.KeyStates",
                c => new
                    {
                        KeyStateID = c.Int(nullable: false, identity: true),
                        KeyStateName = c.String(),
                    })
                .PrimaryKey(t => t.KeyStateID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeyStates");
            DropTable("dbo.ImmeCauses");
        }
    }
}
