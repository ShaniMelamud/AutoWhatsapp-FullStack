using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class ChatBotModel
    {
        public int ChatBotId { get; set; }
        public int MessageQuestionId { get; set; }
        public int MessageAnswerId { get; set; }

        public ChatBotModel()
        {

        }

        public ChatBotModel(ChatBot chatBot)
        {
            ChatBotId = chatBot.ChatBotId;
            MessageQuestionId = chatBot.MessageQuestionId;
            MessageAnswerId = chatBot.MessageAnswerId;
        }

        public ChatBot ConvertToChatBot()
        {
            return new ChatBot
            {
                ChatBotId = ChatBotId,
                MessageQuestionId = MessageQuestionId,
                MessageAnswerId = MessageAnswerId
            };
        }
    }
}
