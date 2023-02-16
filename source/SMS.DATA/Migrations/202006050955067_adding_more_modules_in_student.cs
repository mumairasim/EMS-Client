namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_more_modules_in_student : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Person", "ParentName", c => c.String());
            AddColumn("dbo.Person", "ParentCnic", c => c.String());
            AddColumn("dbo.Person", "DOB", c => c.DateTime());
            AddColumn("dbo.Person", "ParentOccupation", c => c.String());
            AddColumn("dbo.Person", "ParentRelation", c => c.String());
            AddColumn("dbo.Person", "ParentHighestEducation", c => c.String());
            AddColumn("dbo.Person", "ParentNationality", c => c.String());
            AddColumn("dbo.Person", "ParentEmail", c => c.String());
            AddColumn("dbo.Person", "ParentOfficeAddress", c => c.String());
            AddColumn("dbo.Person", "ParentCity", c => c.String());
            AddColumn("dbo.Person", "ParentMobile1", c => c.String());
            AddColumn("dbo.Person", "ParentMobile2", c => c.String());
            AddColumn("dbo.Person", "ParentEmergencyName", c => c.String());
            AddColumn("dbo.Person", "ParentEmergencyRelation", c => c.String());
            AddColumn("dbo.Person", "ParentEmergencyMobile", c => c.String());
            AddColumn("dbo.Student", "PreviousSchoolName", c => c.String());
            AddColumn("dbo.Student", "ReasonForLeaving", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "ReasonForLeaving");
            DropColumn("dbo.Student", "PreviousSchoolName");
            DropColumn("dbo.Person", "ParentEmergencyMobile");
            DropColumn("dbo.Person", "ParentEmergencyRelation");
            DropColumn("dbo.Person", "ParentEmergencyName");
            DropColumn("dbo.Person", "ParentMobile2");
            DropColumn("dbo.Person", "ParentMobile1");
            DropColumn("dbo.Person", "ParentCity");
            DropColumn("dbo.Person", "ParentOfficeAddress");
            DropColumn("dbo.Person", "ParentEmail");
            DropColumn("dbo.Person", "ParentNationality");
            DropColumn("dbo.Person", "ParentHighestEducation");
            DropColumn("dbo.Person", "ParentRelation");
            DropColumn("dbo.Person", "ParentOccupation");
            DropColumn("dbo.Person", "DOB");
            DropColumn("dbo.Person", "ParentCnic");
            DropColumn("dbo.Person", "ParentName");
            DropColumn("dbo.Person", "Age");
        }
    }
}
