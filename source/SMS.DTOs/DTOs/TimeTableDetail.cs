using System;
using System.Collections.Generic;

namespace SMS.DTOs.DTOs
{
    public class TimeTableDetail : DtoBaseEntity
    {
        public string Day { get; set; }

        public Guid? TimeTableId { get; set; }

        public TimeTable TimeTable { get; set; }
        public List<Period> Periods { get; set; }
    }
}
