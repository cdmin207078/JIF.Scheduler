using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.Scheduler.Web.Models
{
    public class GCRecyclingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            GC.Collect();
        }
    }
}