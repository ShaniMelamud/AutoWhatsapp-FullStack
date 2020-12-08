using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Contact
    {
        public Contact()
        {
            MailingListsContacts = new HashSet<MailingListsContact>();
        }

        public int ContactId { get; set; }
        public int BusinessId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<MailingListsContact> MailingListsContacts { get; set; }
    }
}
