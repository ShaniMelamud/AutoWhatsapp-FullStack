using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class MailingListsContactModel
    {
        public int Id { get; set; }
        public int? MailingListId { get; set; }
        public int? ContactId { get; set; }

        public MailingListsContactModel()
        {

        }

        public MailingListsContactModel(MailingListsContact mailingListsContact)
        {
            Id = mailingListsContact.Id;
            MailingListId = mailingListsContact.MailingListId;
            ContactId = mailingListsContact.ContactId;
        }

        public MailingListsContact ConvertToMailingListContact()
        {
            return new MailingListsContact
            {
                Id = Id,
                MailingListId = MailingListId,
                ContactId = ContactId
            };
        }
    }
}
