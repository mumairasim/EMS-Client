namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolIdToBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassAssignement", "SchoolId", c => c.Guid());
            AddColumn("dbo.ClassStudentDiary", "SchoolId", c => c.Guid());
            AddColumn("dbo.EmployeeFinanceDetails", "SchoolId", c => c.Guid());
            AddColumn("dbo.EmployeeFinances", "SchoolId", c => c.Guid());
            AddColumn("dbo.Periods", "SchoolId", c => c.Guid());
            AddColumn("dbo.TimeTableDetails", "SchoolId", c => c.Guid());
            AddColumn("dbo.Person", "SchoolId", c => c.Guid());
            AddColumn("dbo.Files", "SchoolId", c => c.Guid());
            AddColumn("dbo.StudentAssignment", "SchoolId", c => c.Guid());
            AddColumn("dbo.StudentFinanceDetails", "SchoolId", c => c.Guid());
            AddColumn("dbo.FinanceTypes", "SchoolId", c => c.Guid());
            AddColumn("dbo.Student_Finances", "SchoolId", c => c.Guid());
            AddColumn("dbo.StudentStudentDiary", "SchoolId", c => c.Guid());
            AddColumn("dbo.ClassTeacherDiary", "SchoolId", c => c.Guid());
            AddColumn("dbo.AttendanceStatus", "SchoolId", c => c.Guid());
            AddColumn("dbo.StudentAttendanceDetail", "SchoolId", c => c.Guid());
            CreateIndex("dbo.ClassAssignement", "SchoolId");
            CreateIndex("dbo.ClassStudentDiary", "SchoolId");
            CreateIndex("dbo.EmployeeFinanceDetails", "SchoolId");
            CreateIndex("dbo.EmployeeFinances", "SchoolId");
            CreateIndex("dbo.Periods", "SchoolId");
            CreateIndex("dbo.TimeTableDetails", "SchoolId");
            CreateIndex("dbo.Person", "SchoolId");
            CreateIndex("dbo.Files", "SchoolId");
            CreateIndex("dbo.StudentAssignment", "SchoolId");
            CreateIndex("dbo.StudentFinanceDetails", "SchoolId");
            CreateIndex("dbo.FinanceTypes", "SchoolId");
            CreateIndex("dbo.Student_Finances", "SchoolId");
            CreateIndex("dbo.StudentStudentDiary", "SchoolId");
            CreateIndex("dbo.ClassTeacherDiary", "SchoolId");
            CreateIndex("dbo.AttendanceStatus", "SchoolId");
            CreateIndex("dbo.StudentAttendanceDetail", "SchoolId");
            AddForeignKey("dbo.ClassStudentDiary", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.EmployeeFinances", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.EmployeeFinanceDetails", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.Periods", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.TimeTableDetails", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.Files", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.Person", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentAssignment", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.FinanceTypes", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentFinanceDetails", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.Student_Finances", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentStudentDiary", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.ClassTeacherDiary", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.ClassAssignement", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.AttendanceStatus", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentAttendanceDetail", "SchoolId", "dbo.Schools", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAttendanceDetail", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.AttendanceStatus", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.ClassAssignement", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.ClassTeacherDiary", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.StudentStudentDiary", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Student_Finances", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.StudentFinanceDetails", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.FinanceTypes", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.StudentAssignment", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Person", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Files", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.TimeTableDetails", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Periods", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.EmployeeFinanceDetails", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.EmployeeFinances", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.ClassStudentDiary", "SchoolId", "dbo.Schools");
            DropIndex("dbo.StudentAttendanceDetail", new[] { "SchoolId" });
            DropIndex("dbo.AttendanceStatus", new[] { "SchoolId" });
            DropIndex("dbo.ClassTeacherDiary", new[] { "SchoolId" });
            DropIndex("dbo.StudentStudentDiary", new[] { "SchoolId" });
            DropIndex("dbo.Student_Finances", new[] { "SchoolId" });
            DropIndex("dbo.FinanceTypes", new[] { "SchoolId" });
            DropIndex("dbo.StudentFinanceDetails", new[] { "SchoolId" });
            DropIndex("dbo.StudentAssignment", new[] { "SchoolId" });
            DropIndex("dbo.Files", new[] { "SchoolId" });
            DropIndex("dbo.Person", new[] { "SchoolId" });
            DropIndex("dbo.TimeTableDetails", new[] { "SchoolId" });
            DropIndex("dbo.Periods", new[] { "SchoolId" });
            DropIndex("dbo.EmployeeFinances", new[] { "SchoolId" });
            DropIndex("dbo.EmployeeFinanceDetails", new[] { "SchoolId" });
            DropIndex("dbo.ClassStudentDiary", new[] { "SchoolId" });
            DropIndex("dbo.ClassAssignement", new[] { "SchoolId" });
            DropColumn("dbo.StudentAttendanceDetail", "SchoolId");
            DropColumn("dbo.AttendanceStatus", "SchoolId");
            DropColumn("dbo.ClassTeacherDiary", "SchoolId");
            DropColumn("dbo.StudentStudentDiary", "SchoolId");
            DropColumn("dbo.Student_Finances", "SchoolId");
            DropColumn("dbo.FinanceTypes", "SchoolId");
            DropColumn("dbo.StudentFinanceDetails", "SchoolId");
            DropColumn("dbo.StudentAssignment", "SchoolId");
            DropColumn("dbo.Files", "SchoolId");
            DropColumn("dbo.Person", "SchoolId");
            DropColumn("dbo.TimeTableDetails", "SchoolId");
            DropColumn("dbo.Periods", "SchoolId");
            DropColumn("dbo.EmployeeFinances", "SchoolId");
            DropColumn("dbo.EmployeeFinanceDetails", "SchoolId");
            DropColumn("dbo.ClassStudentDiary", "SchoolId");
            DropColumn("dbo.ClassAssignement", "SchoolId");
        }
    }
}
