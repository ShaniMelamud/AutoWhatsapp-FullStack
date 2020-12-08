using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Nancy.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Tomedia
{
    public class PostingRestSharp
    {
        public void Post(string messageContent, List<ContactModel> contacts)
        {
            RestClient client = new RestClient();

            NameValueCollection clients = new NameValueCollection();
            foreach (var item in contacts)
            {
                clients.Add(item.ContactPhone, "clients");
            }
            

            string message_body = messageContent;


            ModelToSendToRemoteServer modelToSendToRemoteServer = new ModelToSendToRemoteServer(message_body, clients);

            var obj = new JavaScriptSerializer();

            string jsonResult = obj.Serialize(modelToSendToRemoteServer);

            Console.WriteLine(jsonResult);

            RestRequest request = new RestRequest("http://127.0.0.1:5000//api/send/scheduledMessage", DataFormat.Json);

            request.AddJsonBody(jsonResult);

            IRestResponse response = client.Post(request);
        }


    }
}
