namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalStatusAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignment", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.ClassAssignement", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Class", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.ClassStudentDiary", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentDiary", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Employee", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Designation", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Schools", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.EmployeeFinanceDetails", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.EmployeeFinances", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Periods", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Course", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.TimeTableDetails", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.TimeTable", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Person", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Files", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Student", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentAssignment", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentFinanceDetails", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.FinanceTypes", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Student_Finances", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentStudentDiary", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.TeacherDiary", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.ClassTeacherDiary", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.Worksheet", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.AttendanceStatus", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.LessonPlan", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentAttendanceDetail", "ApprovalStatus", c => c.Int());
            AddColumn("dbo.StudentAttendance", "ApprovalStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentAttendance", "ApprovalStatus");
            DropColumn("dbo.StudentAttendanceDetail", "ApprovalStatus");
            DropColumn("dbo.LessonPlan", "ApprovalStatus");
            DropColumn("dbo.AttendanceStatus", "ApprovalStatus");
            DropColumn("dbo.Worksheet", "ApprovalStatus");
            DropColumn("dbo.ClassTeacherDiary", "ApprovalStatus");
            DropColumn("dbo.TeacherDiary", "ApprovalStatus");
            DropColumn("dbo.StudentStudentDiary", "ApprovalStatus");
            DropColumn("dbo.Student_Finances", "ApprovalStatus");
            DropColumn("dbo.FinanceTypes", "ApprovalStatus");
            DropColumn("dbo.StudentFinanceDetails", "ApprovalStatus");
            DropColumn("dbo.StudentAssignment", "ApprovalStatus");
            DropColumn("dbo.Student", "ApprovalStatus");
            DropColumn("dbo.Files", "ApprovalStatus");
            DropColumn("dbo.Person", "ApprovalStatus");
            DropColumn("dbo.TimeTable", "ApprovalStatus");
            DropColumn("dbo.TimeTableDetails", "ApprovalStatus");
            DropColumn("dbo.Course", "ApprovalStatus");
            DropColumn("dbo.Periods", "ApprovalStatus");
            DropColumn("dbo.EmployeeFinances", "ApprovalStatus");
            DropColumn("dbo.EmployeeFinanceDetails", "ApprovalStatus");
            DropColumn("dbo.Schools", "ApprovalStatus");
            DropColumn("dbo.Designation", "ApprovalStatus");
            DropColumn("dbo.Employee", "ApprovalStatus");
            DropColumn("dbo.StudentDiary", "ApprovalStatus");
            DropColumn("dbo.ClassStudentDiary", "ApprovalStatus");
            DropColumn("dbo.Class", "ApprovalStatus");
            DropColumn("dbo.ClassAssignement", "ApprovalStatus");
            DropColumn("dbo.Assignment", "ApprovalStatus");
        }
    }
}
