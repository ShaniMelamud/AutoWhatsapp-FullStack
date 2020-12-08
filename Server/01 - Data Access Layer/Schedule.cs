using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Schedule
    {
        public Schedule()
        {
            AutoMessages = new HashSet<AutoMessage>();
        }

        public int ScheduleId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public virtual ICollection<AutoMessage> AutoMessages { get; set; }
    }
}
