using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class ScheduleModel
    {
        public int ScheduleId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public ScheduleModel()
        {

        }

        public ScheduleModel(Schedule schedule)
        {
            ScheduleId = schedule.ScheduleId;
            FromTime = schedule.FromTime;
            ToTime = schedule.ToTime;
        }

        public Schedule ConvertToSchedule()
        {
            return new Schedule
            {
                ScheduleId = ScheduleId,
                FromTime = FromTime,
                ToTime = ToTime
            };
        }
    }
}
