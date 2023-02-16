namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_more_fields_in_student : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Age", c => c.Int(nullable: false));
        }
    }
}
