using Nancy.Json;
using Quartz;
using Quartz.Impl;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Tomedia
{
    public class MessagesLogic: BaseLogic
    {

        MailingListsLogic MailingListsLogic;
        public MessagesLogic(AutoWhatsappContext db, MailingListsLogic mailingListsLogic) : base(db) {
            MailingListsLogic = mailingListsLogic;
        }



        public List<MessageModel> GetAllMessages(int businessId)
        {
            return DB.Messages.Where(p=>p.BusinessId==businessId).Select(p => new MessageModel(p)).ToList();
        }

        public MessageModel GetSingleMessage(int businessId, int messageId)
        {
            return DB.Messages.Where(p => p.MessageId == messageId && p.BusinessId==businessId).Select(p => new MessageModel(p)).SingleOrDefault();
        }

        public MessageModel AddMessage(int businessId, MessageModel messageModel)
        {
            messageModel.BusinessId = businessId;
            Message message = messageModel.ConvertToMessage();
            DB.Messages.Add(message);
            DB.SaveChanges();
            messageModel.MessageId = message.MessageId;
            return messageModel;
        }

        public MessageModel UpdateFullMessage(int businessId, MessageModel messageModel)
        {
            Message message = DB.Messages.SingleOrDefault(p => p.MessageId == messageModel.MessageId && p.BusinessId == businessId);
            if (message == null)
                return null;
            message.MessageContent = messageModel.MessageContent;
            message.FilePath = messageModel.FilePath;
            DB.SaveChanges();
            return messageModel;
        }

        public MessageModel UpdatePartialMessage(int businessId, MessageModel messageModel)
        {
            Message message = DB.Messages.SingleOrDefault(p => p.MessageId == messageModel.MessageId && p.BusinessId == businessId);
            if (message == null)
                return null;

            if (message.MessageContent != null)
                message.MessageContent = messageModel.MessageContent;

            if (message.FilePath != null)
                message.FilePath = messageModel.FilePath;

            DB.SaveChanges();
            return messageModel;
        }

        public void DeleteMessage(int businessId, int messageId)
        {
            Message MessageToDelete = DB.Messages.SingleOrDefault(p => p.MessageId == messageId && p.BusinessId == businessId);
            if (MessageToDelete == null)
                return;
            DB.Messages.Remove(MessageToDelete);
            DB.SaveChanges();
        }

        public List<MessageModel> GetAllMessagesContainText(int businessId, string text)
        {
            return DB.Messages.Where(p => p.BusinessId==businessId && p.MessageContent.Contains(text)).Select(p => new MessageModel(p)).ToList();
        }

        public void DeleteAllMessages(int businessId)
        {
            foreach (var item in DB.Messages)
            {
                if (item.BusinessId == businessId)
                {
                    DB.Messages.Remove(item);
                }
            }
            DB.SaveChanges();
        }

        public List<MessageModel> GetAllMessagesByMailingListId(int busienessID, int mailingListId)
        {
            List<MailingListsMessage> mailingListMessages = DB.MailingListsMessages.Where(p => p.MailingListId == mailingListId).ToList();
            List<MessageModel> messagesByMailingListId = new List<MessageModel>();
            
            foreach (var item in mailingListMessages)
            {
                messagesByMailingListId.Add(new MessageModel(DB.Messages.SingleOrDefault(p => p.MessageId == item.MessageId && p.BusinessId == busienessID)));
            }
            return messagesByMailingListId;
        }

        public async Task SendMessageToSeveralMailingLists(ScheduledMessageToMailingListModel scheduledMessage)
        {
            MessageModel messageModel = new MessageModel(DB.Messages.SingleOrDefault(p => p.MessageId == scheduledMessage.MessageId && p.BusinessId == scheduledMessage.BusinessId));

            List<List<MailingListsContactModel>> severalMailingListsContactModels = new List<List<MailingListsContactModel>>();

            foreach (var item in scheduledMessage.MailingListIds)
            {
                List<MailingListsContactModel> singleMailingListContactModels = DB.MailingListsContacts
                    .Join(DB.Contacts, p=>p.ContactId, p=>p.BusinessId, (a, b)=>a)
                    .Where(p => p.MailingListId == item)
                    .Select(p => new MailingListsContactModel(p)).ToList();
                severalMailingListsContactModels.Add(singleMailingListContactModels);
            }

            List<ContactModel> contacts = new List<ContactModel>();
            foreach (var list in severalMailingListsContactModels)
            {
                foreach (var item in list)
                {
                    var contactToCheck = contacts.SingleOrDefault(p => p.ContactId == item.ContactId);
                    if(contactToCheck==null)
                        contacts.Add(new ContactModel(DB.Contacts.SingleOrDefault(p => p.ContactId == item.ContactId)));
                }
            }

            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendMessage>()
                .WithIdentity(Guid.NewGuid().ToString())
                .UsingJobData("messageContent", messageModel.MessageContent)
                .Build();
            job.JobDataMap["contacts"] = contacts;

            DateTimeOffset dateTimeOffset = new DateTimeOffset(scheduledMessage.DateTime);
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString())
                .StartAt(scheduledMessage.DateTime)
                .WithSimpleSchedule()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public IRestResponse sendSimpleMessage(SimpleMessageModel simpleMessageModel)
        {
            RestClient client = new RestClient();

            NameValueCollection clients = new NameValueCollection();
            foreach (var item in simpleMessageModel.Clients)
            {
                clients.Add(item, "clients");
            }

            string message_body = simpleMessageModel.MessageBody;

            ModelToSendToRemoteServer modelToSendToRemoteServer = new ModelToSendToRemoteServer(message_body, clients);

            var obj = new JavaScriptSerializer();

            string jsonResult = obj.Serialize(modelToSendToRemoteServer);

            Console.WriteLine(jsonResult);

            RestRequest request = new RestRequest("http://127.0.0.1:5000/api/send/scheduledMessage", DataFormat.Json);

            request.AddJsonBody(jsonResult);

            IRestResponse response = client.Post(request);

            return response;
        }
    }
}
