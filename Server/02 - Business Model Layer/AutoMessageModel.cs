using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class AutoMessageModel
    {
        public int AutoMessageId { get; set; }
        public int MessageId { get; set; }
        public int ScheduleId { get; set; }

        public AutoMessageModel()
        {

        }

        public AutoMessageModel(AutoMessage autoMessage)
        {
            AutoMessageId = autoMessage.AutoMessageId;
            MessageId = autoMessage.MessageId;
            ScheduleId = autoMessage.ScheduleId;
        }

        public AutoMessage ConvertToAutoMessage()
        {
            return new AutoMessage
            {
                AutoMessageId = AutoMessageId,
                MessageId = MessageId,
                ScheduleId = ScheduleId
            };
        }
    }


}
