using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class MailingListsContact
    {
        public int Id { get; set; }
        public int? MailingListId { get; set; }
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual MailingList MailingList { get; set; }
    }
}
