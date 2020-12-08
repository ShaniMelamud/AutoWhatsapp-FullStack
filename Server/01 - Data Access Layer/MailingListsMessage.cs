using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class MailingListsMessage
    {
        public int MailingListMessageId { get; set; }
        public int? MailingListId { get; set; }
        public int? MessageId { get; set; }

        public virtual MailingList MailingList { get; set; }
        public virtual Message Message { get; set; }
    }
}
