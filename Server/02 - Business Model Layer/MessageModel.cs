using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public int BusinessId { get; set; }
        public string FilePath { get; set; }

        public MessageModel()
        {

        }

        public MessageModel(Message message)
        {
            MessageId = message.MessageId;
            MessageContent = message.MessageContent;
            BusinessId = message.BusinessId;
            FilePath = message.FilePath;
        }

        public Message ConvertToMessage()
        {
            return new Message
            {
                MessageId = MessageId,
                MessageContent = MessageContent,
                BusinessId = BusinessId,
                FilePath = FilePath
            };
        }
    }
}
