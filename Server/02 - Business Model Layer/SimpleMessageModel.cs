using System.Collections.Generic;

namespace Tomedia
{
    public class SimpleMessageModel
    {
        public string MessageBody { get; set; }
        public List<string> Clients { get; set; }

        public SimpleMessageModel() { }

        public SimpleMessageModel(string messageBody, List<string> clients)
        {
            MessageBody = messageBody;
            clients = Clients;
        }
    }
}
