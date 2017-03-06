using JIF.Scheduler.Core.Data;
using JIF.Scheduler.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace JIF.Scheduler.Web.Services
{
    public class JobInfoServices
    {
        private readonly IRepository<JobInfo> _jobInfoRepository;
        private readonly SchedulerContext _schedulerContext;

        public JobInfoServices(IRepository<JobInfo> jobInfoRepository,
            SchedulerContext schedulerContext)
        {
            _jobInfoRepository = jobInfoRepository;
            _schedulerContext = schedulerContext;
        }


        /// <summary>
        /// 获取所有可用Job
        /// </summary>
        /// <returns></returns>
        public List<JobInfo> GetAllEnabledJobs()
        {
            return _jobInfoRepository.Table.Where(d => d.Enabled == true).ToList();
        }

        public List<JobInfo> GetAllJobs()
        {
            return _jobInfoRepository.Table.ToList();
        }

        public JobInfo Get(int id)
        {
            return _jobInfoRepository.Get(id);
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="id"></param>
        public void StartUpJob(int id)
        {
            try
            {
                _schedulerContext.Scheduler.ResumeJob(new Quartz.JobKey(id.ToString(), "httpservice-job"));
                _schedulerContext.Scheduler.ResumeTrigger(new Quartz.TriggerKey(id.ToString(), "httpservice-trigger"));
            }
            catch { }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        public void StopJob(int id)
        {
            try
            {

                // http://stackoverflow.com/questions/1933676/quartz-java-resuming-a-job-excecutes-it-many-times
                // 恢复之后多次触发原因
                _schedulerContext.Scheduler.PauseJob(new Quartz.JobKey(id.ToString(), "httpservice-job"));
                _schedulerContext.Scheduler.PauseTrigger(new Quartz.TriggerKey(id.ToString(), "httpservice-trigger"));
            }
            catch { }
        }


        //public List<JobInfo> GetJobs()
        //{
        //    var result = new List<JobInfo>();

        //    result.Add(new JobInfo
        //    {
        //        Id = "A",
        //        Name = "调用 Info 方法",
        //        Description = "无",
        //        ServiceUrl = "http://localhost:60004/home/info",
        //        CronString = "0/10 * 8-20 * * ?"
        //    });

        //    result.Add(new JobInfo
        //    {
        //        Id = "B",
        //        Name = "调用 Error 方法",
        //        Description = "无",
        //        ServiceUrl = "http://localhost:60004/home/error",
        //        CronString = "0/5 * 8-20 * * ?"
        //    });

        //    result.Add(new JobInfo
        //    {
        //        Id = "C",
        //        Name = "调用 Debug 方法",
        //        Description = "无",
        //        ServiceUrl = "http://localhost:60004/home/debug",
        //        CronString = "0/4 * 8-20 * * ?"
        //    });

        //    result.Add(new JobInfo
        //    {
        //        Id = "D",
        //        Name = "调用 Trace 方法",
        //        Description = "无",
        //        ServiceUrl = "http://localhost:60004/home/trace",
        //        CronString = "0/3 * 8-20 * * ?"
        //    });

        //    result.Add(new JobInfo
        //    {
        //        Id = "E",
        //        Name = "调用 Warn 方法",
        //        Description = "无",
        //        ServiceUrl = "http://localhost:60004/home/warn",
        //        CronString = "0/2 * 8-20 * * ?"
        //    });

        //    return result;
        //}

        //public JobInfo Get(string id)
        //{
        //    return GetJobs().FirstOrDefault(d => d.Id == id);
        //}
    }
}