using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Tomedia
{
    public class ModelToSendToRemoteServer
    {
        public string Message_body { get; set; }
        public NameValueCollection Clients { get; set; }

        public ModelToSendToRemoteServer() { }


        public ModelToSendToRemoteServer(string message_body, NameValueCollection clients)
        {
            Message_body = message_body;
            Clients = clients;
        }
    }
}
