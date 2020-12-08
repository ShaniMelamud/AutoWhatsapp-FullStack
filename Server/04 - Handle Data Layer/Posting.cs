//using Nancy.Json;
//using System;
//using System.Collections.Specialized;
//using System.Net;

//namespace Tomedia
//{
//    public class Posting
//    {
//        public void Post()
//        {
//            using (WebClient webClient = new WebClient())
//            {
//                NameValueCollection clients = new NameValueCollection();
//                clients.Add("+972545799650", "clients");
//                clients.Add("+972584848464", "clients");

                

//                string message_body = "Testing again";


//                ModelToSendToRemoteServer modelToSendToRemoteServer = new ModelToSendToRemoteServer(message_body, clients);

//                var obj = new JavaScriptSerializer();

//                var jsonResult = obj.Serialize(modelToSendToRemoteServer);

//                Console.WriteLine(jsonResult);

//                webClient.UploadString("https://waservice.tomedia.co.il//api/send/scheduledMessage", jsonResult);

//                Console.WriteLine(webClient.DownloadString("https://api.chucknorris.io/jokes/random"));


//            }
//        }
//    }
//}
