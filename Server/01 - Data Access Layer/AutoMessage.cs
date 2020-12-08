using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class AutoMessage
    {
        public int AutoMessageId { get; set; }
        public int MessageId { get; set; }
        public int ScheduleId { get; set; }

        public virtual Message Message { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
