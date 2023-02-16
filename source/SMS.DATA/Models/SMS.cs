using SMS.DATA.Infrastructure;
using System.Data.Entity;
namespace SMS.DATA.Models
{
    public partial class SMS : DbContext, IDbContext
    {
        public SMS()
            : base("name=SmsConnection")
        {
        }
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassAssignement> ClassAssignements { get; set; }
        public virtual DbSet<ClassStudentDiary> ClassStudentDiaries { get; set; }
        public virtual DbSet<ClassTeacherDiary> ClassTeacherDiaries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeFinanceDetail> EmployeeFinanceDetails { get; set; }
        public virtual DbSet<EmployeeFinance> EmployeeFinances { get; set; }
        public virtual DbSet<FinanceType> FinanceTypes { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Finances> Student_Finances { get; set; }
        public virtual DbSet<StudentAssignment> StudentAssignments { get; set; }
        public virtual DbSet<StudentDiary> StudentDiaries { get; set; }
        public virtual DbSet<StudentFinanceDetail> StudentFinanceDetails { get; set; }
        public virtual DbSet<StudentStudentDiary> StudentStudentDiaries { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TeacherDiary> TeacherDiaries { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<TimeTableDetail> TimeTableDetails { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Worksheet> Worksheets { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<AttendanceStatus> AttendanceStatus { get; set; }
        public virtual DbSet<StudentAttendanceDetail> StudentAttendance { get; set; }
        public virtual DbSet<RequestMeta> RequestMeta { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.RegistrationNumber).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeNumber).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Assignments)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.InstructorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StudentDiaries)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.InstructorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TeacherDiaries)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.InstructorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Periods)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.TeacherId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Worksheets)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.InstructorId);

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

            //modelBuilder.Types<DomainBaseEnitity>()
            //    .Where(t => t.BaseType == typeof(School))
            //    .Configure(x => x.Ignore(prop => prop.SchoolId));
            //modelBuilder.Types<DomainBaseEnitity>()
            //    .Where(t => t.BaseType == typeof(School))
            //    .Configure(x => x.Ignore(prop => prop.School));
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
