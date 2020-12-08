using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public int BusinessId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Name { get; set; }

        public ContactModel() { }

        public ContactModel(Contact contact)
        {
            ContactId = contact.ContactId;
            BusinessId = contact.BusinessId;
            ContactName = contact.ContactName;
            ContactPhone = contact.ContactPhone;
            ContactEmail = contact.ContactEmail;
        }

        public Contact ConvertToContact()
        {
            return new Contact
            {
                ContactId = ContactId,
                BusinessId = BusinessId,
                ContactName = ContactName,
                ContactPhone = ContactPhone,
                ContactEmail = ContactEmail
            };
        }
    }
}
