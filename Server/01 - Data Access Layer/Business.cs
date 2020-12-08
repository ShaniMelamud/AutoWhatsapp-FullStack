using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Business
    {
        public Business()
        {
            Contacts = new HashSet<Contact>();
            Messages = new HashSet<Message>();
            Questions = new HashSet<Question>();
        }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessEmail { get; set; }
        public string CustomerName { get; set; }
        public string ContactsBookFileName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
