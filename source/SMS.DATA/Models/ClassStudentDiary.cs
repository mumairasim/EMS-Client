using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("ClassStudentDiary")]
    public partial class ClassStudentDiary : DomainBaseEnitity
    {

        public Guid? StudentDiaryId { get; set; }

        public Guid? ClassId { get; set; }

        public virtual Class Class { get; set; }

        public virtual StudentDiary StudentDiary { get; set; }
    }
}
