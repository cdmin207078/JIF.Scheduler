using JIF.Scheduler.Core.Infrastructure;
using JIF.Scheduler.Web.Models;
using JIF.Scheduler.Web.Services;
using Quartz;
using Quartz.Impl;

namespace JIF.Scheduler.Web
{
    public class SchedulerContext
    {
        private JobInfoServices _jobInfoService;

        private IScheduler _scheduler;

        public SchedulerContext(JobInfoServices jobInfoService)
        {
            _jobInfoService = jobInfoService;

            Initialize();
        }

        private void Initialize()
        {
            if (Singleton<SchedulerContext>.Instance != null)
            {
                return;
            }
            else
            {
                Singleton<SchedulerContext>.Instance = this;
            }

            var jobs = _jobInfoService.GetJobs();

            if (jobs == null)
                return;

            ISchedulerFactory _schedFact = new StdSchedulerFactory();

            // get a scheduler
            _scheduler = _schedFact.GetScheduler();
            _scheduler.Start();

            foreach (var j in jobs)
            {
                IJobDetail job = JobBuilder.Create<HttpServiceJob>()
                    .WithIdentity(j.Id, "httpservice-job")
                    .UsingJobData("ServiceUrl", j.ServiceUrl)
                    .UsingJobData("JobName", j.Name)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(j.Id, "httpservice-trigger")
                    .WithCronSchedule(j.CronString)
                    .Build();

                _scheduler.ScheduleJob(job, trigger);
            }

            // setting recycling control
            IJobDetail RecyclingJob = JobBuilder.Create<GCRecyclingJob>()
                .WithIdentity("GC.Collect", "recycling-job")
                .Build();

            ITrigger RecyclingTrigger = TriggerBuilder.Create()
                .WithIdentity("GC.Collect", "recycling-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10))
                .Build();

            _scheduler.ScheduleJob(RecyclingJob, RecyclingTrigger);

        }

        public IScheduler Scheduler
        {
            get
            {
                return _scheduler;
            }
        }
    }
}