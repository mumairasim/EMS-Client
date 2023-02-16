using System.Data.Entity;
using SMS.REQUESTDATA.Infrastructure;

namespace SMS.REQUESTDATA.RequestModels
{
    public partial class SMSRequest : DbContext,IRequestDbContext
    {
        public SMSRequest()
            : base("name=SMSRequest")
        {
        }
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassAssignement> ClassAssignements { get; set; }
        public virtual DbSet<ClassStudentDiary> ClassStudentDiaries { get; set; }
        public virtual DbSet<ClassTeacherDiary> ClassTeacherDiaries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseClass> CourseClasses { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeFinanceDetail> EmployeeFinanceDetails { get; set; }
        public virtual DbSet<EmployeeFinance> EmployeeFinances { get; set; }
        public virtual DbSet<FinanceType> FinanceTypes { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Finances> Student_Finances { get; set; }
        public virtual DbSet<StudentAssignment> StudentAssignments { get; set; }
        public virtual DbSet<StudentDiary> StudentDiaries { get; set; }
        public virtual DbSet<StudentFinanceDetail> StudentFinanceDetails { get; set; }
        public virtual DbSet<StudentStudentDiary> StudentStudentDiaries { get; set; }
        public virtual DbSet<TeacherDiary> TeacherDiaries { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<TimeTableDetail> TimeTableDetails { get; set; }
        public virtual DbSet<Worksheet> Worksheets { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<AttendanceStatus> AttendanceStatuses{get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances{ get; set; }
        public virtual DbSet<StudentAttendanceDetail> StudentAttendanceDetails{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeFinanceDetail>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EmployeeFinanceDetail>()
                .HasMany(e => e.EmployeeFinances)
                .WithOptional(e => e.EmployeeFinanceDetail)
                .HasForeignKey(e => e.EmployeeFinanceDetailsId);

            modelBuilder.Entity<StudentFinanceDetail>()
                .Property(e => e.Fee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StudentFinanceDetail>()
                .HasMany(e => e.Student_Finances)
                .WithOptional(e => e.StudentFinanceDetail)
                .HasForeignKey(e => e.StudentFinanceDetailsId);
        }
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int result;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using (var transaction = Database.BeginTransaction())
                {
                    result = Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = Database.ExecuteSqlCommand(sql, parameters);

            return result;
        }
    }
}
