using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    class MailingListsMessageModel
    {
        public int MailingListMessageId { get; set; }
        public int? MailingListId { get; set; }
        public int? MessageId { get; set; }

        public MailingListsMessageModel()
        {

        }

        public MailingListsMessageModel(MailingListsMessage mailingListsMessage)
        {
            MailingListMessageId = mailingListsMessage.MailingListMessageId;
            MailingListId = mailingListsMessage.MailingListId;
            MessageId = mailingListsMessage.MessageId;
        }

        public MailingListsMessage ConvertToMailingListsMessage()
        {
            return new MailingListsMessage
            {
                MailingListMessageId = MailingListMessageId,
                MailingListId = MailingListId,
                MessageId = MessageId
            };
        }
    }
}
