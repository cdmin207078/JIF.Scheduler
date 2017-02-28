using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using JIF.Scheduler.Web.Services;
using JIF.Scheduler.Web.Models;

namespace JIF.Scheduler.Web
{
    public static class SchedulerConfig
    {
        private static ISchedulerFactory _schedFact;

        public static void Init()
        {
            var jobs = new JobInfoServices().GetJobs();

            if (jobs == null)
                return;

            if (_schedFact != null)
                return;

            _schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = _schedFact.GetScheduler();

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


                sched.ScheduleJob(job, trigger);
            }


            // setting recycling control
            IJobDetail RecyclingJob = JobBuilder.Create<HttpServiceJob>()
                .WithIdentity("GC.Collect", "recycling-job")
                .Build();

            ITrigger RecyclingTrigger = TriggerBuilder.Create()
                .WithIdentity("GC.Collect", "recycling-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(10))
                .Build();

            sched.Start();
        }
    }
}