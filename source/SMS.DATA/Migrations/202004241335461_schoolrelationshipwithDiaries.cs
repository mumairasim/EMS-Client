namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schoolrelationshipwithDiaries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentDiary", "SchoolId", c => c.Int());
            AddColumn("dbo.TeacherDiary", "SchoolId", c => c.Int());
            CreateIndex("dbo.StudentDiary", "SchoolId");
            CreateIndex("dbo.TeacherDiary", "SchoolId");
            AddForeignKey("dbo.TeacherDiary", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentDiary", "SchoolId", "dbo.Schools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDiary", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.TeacherDiary", "SchoolId", "dbo.Schools");
            DropIndex("dbo.TeacherDiary", new[] { "SchoolId" });
            DropIndex("dbo.StudentDiary", new[] { "SchoolId" });
            DropColumn("dbo.TeacherDiary", "SchoolId");
            DropColumn("dbo.StudentDiary", "SchoolId");
        }
    }
}
