using JIF.Scheduler.Core.Infrastructure;
using JIF.Scheduler.Core.Log;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
