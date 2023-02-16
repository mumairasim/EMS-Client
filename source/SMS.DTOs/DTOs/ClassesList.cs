using System.Collections.Generic;

namespace SMS.DTOs.DTOs
{
    public class ClassesList
    {
        public List<Class> Classes { get; set; }
        public int classesCount { get; set; }
    }

    public class CoursesList
    {
        public List<Course> Courses { get; set; }
        public int Count { get; set; }
    }

    public class TeacherDiaryList
    {
        public List<TeacherDiary> Courses { get; set; }
        public int Count { get; set; }
    }

    public class ServiceResponse<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }

}
