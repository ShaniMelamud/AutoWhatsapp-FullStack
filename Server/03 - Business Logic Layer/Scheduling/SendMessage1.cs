using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tomedia
{
    public class SendMessage : IJob
    {

        

        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string messageContent = dataMap.GetString("messageContent");
            List<ContactModel> contacts = (List<ContactModel>)dataMap["contacts"];
            
            List<ContactModel> contacts1 = new List<ContactModel>{ new ContactModel { ContactPhone="+972584848464"}};
            


            PostingRestSharp postingRestSharp = new PostingRestSharp();

            postingRestSharp.Post(messageContent, contacts);

            await Console.Out.WriteLineAsync("message sent");
        }
    }
}
