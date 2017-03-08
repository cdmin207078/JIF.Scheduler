using JIF.Scheduler.Core.Infrastructure;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Services.Jobs
{
    public class SchedulerContainer
    {
        private IScheduler _scheduler;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Initialize()
        {
            if (Singleton<SchedulerContainer>.Instance == null)
            {
                Singleton<SchedulerContainer>.Instance = this;

                var jobService = EngineContext.Current.Resolve<JobInfoServices>();

                var jobs = jobService.GetAllJobs();

                if (jobs == null)
                    return;

                ISchedulerFactory _schedFact = new StdSchedulerFactory();

                // 创建 scheduler(调度) 对象, 并启动
                _scheduler = _schedFact.GetScheduler();
                _scheduler.Start();

                foreach (var j in jobs)
                {
                    IJobDetail job = JobBuilder.Create<HttpServiceJob>()
                        .WithIdentity(j.Id.ToString(), "httpservice-job")
                        .UsingJobData("ServiceUrl", j.ServiceUrl)
                        .UsingJobData("JobName", j.Name)
                        .Build();

                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity(j.Id.ToString(), "httpservice-trigger")
                        .WithCronSchedule(j.CronString, x => x
                             .WithMisfireHandlingInstructionDoNothing())
                        .Build();

                    _scheduler.ScheduleJob(job, trigger);

                    if (!j.Enabled)
                    {
                        _scheduler.PauseJob(new JobKey(j.Id.ToString(), "httpservice-job"));
                        _scheduler.PauseTrigger(new TriggerKey(j.Id.ToString(), "httpservice-trigger"));
                    }
                }

                // setting recycling control
                //addGCRecyclingJob();

            }
        }

        /// <summary>
        /// GC-Recycling Job
        /// </summary>
        private void addGCRecyclingJob()
        {

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

        /// <summary>
        /// 判断job 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistJob(int id)
        {
            return _scheduler.CheckExists(new JobKey(id.ToString(), "httpservice-job"));
        }

        /// <summary>
        /// 重启暂停的 Job
        /// </summary>
        /// <param name="id">任务编号</param>
        public void ResumeJob(int id)
        {
            _scheduler.ResumeJob(new JobKey(id.ToString(), "httpservice-job"));
            _scheduler.ResumeTrigger(new TriggerKey(id.ToString(), "httpservice-trigger"));
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        public void PauseJob(int id)
        {
            //// http://stackoverflow.com/questions/1933676/quartz-java-resuming-a-job-excecutes-it-many-times
            //// 恢复之后多次触发原因, 未解决
            _scheduler.PauseJob(new JobKey(id.ToString(), "httpservice-job"));
            _scheduler.PauseTrigger(new TriggerKey(id.ToString(), "httpservice-trigger"));
        }

        /// <summary>
        /// 启动所有任务
        /// </summary>
        public void StartJobs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停止调度程序
        /// </summary>
        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停止调度程序
        /// </summary>
        /// <param name="waitForJobsToComplete"></param>
        public void Shuedown(bool waitForJobsToComplete)
        {
            throw new NotImplementedException();
        }
    }
}
