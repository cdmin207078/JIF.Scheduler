using Common.Logging;
using JIF.Scheduler.Core.Infrastructure;
using Quartz;
using System;

namespace JIF.Scheduler.Core.Services.Jobs
{
    public class GCRecyclingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var _log = EngineContext.Current.Resolve<ILog>();

            _log.Info("GC.Collect");

            GC.Collect();

        }
    }
}
