using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class ScheduledMessageToMailingListModel
    {
        public DateTime DateTime { get; set; }
        public List<int> MailingListIds { get; set; }
        public int MessageId { get; set; }
        public int BusinessId { get; set; }

        public ScheduledMessageToMailingListModel()
        {

        }
    }
}
