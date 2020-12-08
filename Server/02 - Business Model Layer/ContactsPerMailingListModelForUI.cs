using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class ContactsPerMailingListModelForUI
    {
        public MailingListModel MailingList { get; set; }
        public List<ContactModel> Contacts { get; set; }

        public ContactsPerMailingListModelForUI() { }

        public ContactsPerMailingListModelForUI(MailingListModel mailingListModel, List<ContactModel> contacts)
        {
            MailingList = MailingList;
            Contacts = contacts;
        }
    }
}
