namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_field_in_different_tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "EmployeeNumber", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TeacherDiary", "Name", c => c.String());
            AddColumn("dbo.LessonPlan", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LessonPlan", "Name");
            DropColumn("dbo.TeacherDiary", "Name");
            DropColumn("dbo.Employee", "EmployeeNumber");
        }
    }
}
