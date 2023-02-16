using System;
using System.Collections.Generic;

namespace SMS.DTOs.DTOs
{
    public class TimeTable : DtoBaseEntity
    {
        public string TimeTableName { get; set; }
        public Guid? ClassId { get; set; }
        public Class Class { get; set; }
        public List<TimeTableDetail> TimeTableDetails { get; set; }
    }
}
