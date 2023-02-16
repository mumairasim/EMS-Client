using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("ClassStudentDiary")]
    public partial class ClassStudentDiary : RequestBase
    {

        public Guid? StudentDiaryId { get; set; }

        public Guid? ClassId { get; set; }

    }
}
