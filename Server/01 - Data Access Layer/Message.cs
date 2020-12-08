using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Message
    {
        public Message()
        {
            AutoMessages = new HashSet<AutoMessage>();
            MailingListsMessages = new HashSet<MailingListsMessage>();
        }

        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public int BusinessId { get; set; }
        public string FilePath { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<AutoMessage> AutoMessages { get; set; }
        public virtual ICollection<MailingListsMessage> MailingListsMessages { get; set; }
    }
}
