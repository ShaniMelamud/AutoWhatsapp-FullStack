using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tomedia.Scheduling;

namespace Tomedia
{
    class Program
    {

        private static async Task Main(string[] args)
        {

            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SendMessage1>()
                .WithIdentity("job1", "group1")
                .Build();

            var d = DateTimeOffset.Now;
            var d1 = new DateTimeOffset(2020, 11, 2, 21, 23, 0, new TimeSpan(2, 0, 0));
            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(d1)
                .WithSimpleSchedule()
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);

            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(60));

            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();

            
        }

    }
}
