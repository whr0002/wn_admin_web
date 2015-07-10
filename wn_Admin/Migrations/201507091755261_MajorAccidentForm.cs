namespace wn_Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajorAccidentForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccidentPhotoes",
                c => new
                    {
                        AccidentPhotoID = c.Int(nullable: false, identity: true),
                        MajorAccidentFormID = c.Int(nullable: false),
                        Url = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AccidentPhotoID)
                .ForeignKey("dbo.MajorAccidentForms", t => t.MajorAccidentFormID)
                .Index(t => t.MajorAccidentFormID);
            
            CreateTable(
                "dbo.MajorAccidentForms",
                c => new
                    {
                        MajorAccidentFormID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TypeInjury = c.String(),
                        BodyLeft = c.Boolean(nullable: false),
                        BodyRight = c.Boolean(nullable: false),
                        HospitalName = c.String(),
                        DoctorName = c.String(),
                        FirstAidAttendent = c.String(),
                        EquipmentInvolved = c.String(),
                        WCBFormCompSubm = c.String(),
                        OHSNotified = c.String(),
                        OHSNameContact = c.String(),
                        DescLoss = c.String(),
                        InjuriesDueToIncident = c.String(),
                        LostTime = c.String(),
                        EquipOut = c.String(),
                        OtherLosses = c.String(),
                        WitnessesOccur = c.String(),
                        DateOccur = c.DateTime(nullable: false),
                        TimeOccur = c.String(),
                        LocationOccur = c.String(),
                        AccidentPPEOption = c.String(),
                        ListPPE = c.String(),
                        WasPersonTrained = c.String(),
                        PotReoccur = c.String(),
                        LossPotEval = c.String(),
                        LocationSpill = c.String(),
                        SourceSpill = c.String(),
                        TypeSubstance = c.String(),
                        AmountSubstance = c.String(),
                        AdvEffOn = c.String(),
                        AccidentDesc = c.String(),
                        PhotoTaken = c.Boolean(nullable: false),
                        WitnessStatement = c.String(),
                        ImmediateCauses = c.String(),
                        KeyStates = c.String(),
                        SubstandardActions = c.String(),
                        SubstandardConditions = c.String(),
                        BasicCauses = c.String(),
                        PersonalFactors = c.String(),
                        JobFactors = c.String(),
                        RiskAssess = c.String(),
                        TempActionTaken = c.String(),
                        SuggestedCorrActions = c.String(),
                        PreparedBy = c.String(),
                        PrepareDate = c.DateTime(nullable: false),
                        ApprovedBy = c.String(),
                        ApprovedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MajorAccidentFormID);
            
            CreateTable(
                "dbo.AccidentTypes",
                c => new
                    {
                        AccidentTypeID = c.Int(nullable: false, identity: true),
                        AccidentTypeName = c.String(),
                        MajorAccidentForm_MajorAccidentFormID = c.Int(),
                    })
                .PrimaryKey(t => t.AccidentTypeID)
                .ForeignKey("dbo.MajorAccidentForms", t => t.MajorAccidentForm_MajorAccidentFormID)
                .Index(t => t.MajorAccidentForm_MajorAccidentFormID);
            
            CreateTable(
                "dbo.AccidentPPEOptions",
                c => new
                    {
                        AccidentPPEOptionID = c.Int(nullable: false, identity: true),
                        AccidentPPEOptionName = c.String(),
                    })
                .PrimaryKey(t => t.AccidentPPEOptionID);
            
            CreateTable(
                "dbo.AdvEffs",
                c => new
                    {
                        AdvEffID = c.Int(nullable: false, identity: true),
                        AdvEffName = c.String(),
                    })
                .PrimaryKey(t => t.AdvEffID);
            
            CreateTable(
                "dbo.PotReoccurs",
                c => new
                    {
                        PotReoccurID = c.Int(nullable: false, identity: true),
                        PotReoccurName = c.String(),
                    })
                .PrimaryKey(t => t.PotReoccurID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccidentTypes", "MajorAccidentForm_MajorAccidentFormID", "dbo.MajorAccidentForms");
            DropForeignKey("dbo.AccidentPhotoes", "MajorAccidentFormID", "dbo.MajorAccidentForms");
            DropIndex("dbo.AccidentTypes", new[] { "MajorAccidentForm_MajorAccidentFormID" });
            DropIndex("dbo.AccidentPhotoes", new[] { "MajorAccidentFormID" });
            DropTable("dbo.PotReoccurs");
            DropTable("dbo.AdvEffs");
            DropTable("dbo.AccidentPPEOptions");
            DropTable("dbo.AccidentTypes");
            DropTable("dbo.MajorAccidentForms");
            DropTable("dbo.AccidentPhotoes");
        }
    }
}
