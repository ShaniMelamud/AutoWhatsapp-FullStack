using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class MailingListModel
    {
        public int MailingListId { get; set; }
        public int? BusinessId { get; set; }
        public string MailingListName { get; set; }

        public MailingListModel()
        {

        }

        public MailingListModel(MailingList mailingList)
        {
            MailingListId = mailingList.MailingListId;
            BusinessId = mailingList.BusinessId;
            MailingListName = mailingList.MailingListName;
        }

        public MailingList ConvertToMailingList()
        {
            return new MailingList
            {
                MailingListId = MailingListId,
                BusinessId = BusinessId,
                MailingListName = MailingListName
            };
        }
    }
}
