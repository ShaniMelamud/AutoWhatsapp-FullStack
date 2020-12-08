using Quartz;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tomedia.Scheduling
{
    public class messagingJobListener : IJobListener
    {
        string Name { get; }

        string IJobListener.Name => throw new NotImplementedException();

        Task IJobListener.JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IJobListener.JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IJobListener.JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
