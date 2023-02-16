using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("ClassTeacherDiary")]
    public partial class ClassTeacherDiary : RequestBase
    {

        public Guid? TeacherDiaryId { get; set; }

        public Guid? ClassId { get; set; }

    }
}
