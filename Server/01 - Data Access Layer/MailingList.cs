using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class MailingList
    {
        public MailingList()
        {
            MailingListsContacts = new HashSet<MailingListsContact>();
            MailingListsMessages = new HashSet<MailingListsMessage>();
        }

        public int MailingListId { get; set; }
        public string MailingListName { get; set; }
        public int? BusinessId { get; set; }

        public virtual ICollection<MailingListsContact> MailingListsContacts { get; set; }
        public virtual ICollection<MailingListsMessage> MailingListsMessages { get; set; }
    }
}
