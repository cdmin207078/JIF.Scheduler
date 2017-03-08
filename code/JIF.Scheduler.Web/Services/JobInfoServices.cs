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
        /// 唤醒任务
        /// </summary>
        /// <param name="id"></param>
        public void ResumeJob(int id)
        {
            try
            {
                // 唤醒任务
                _schedulerContext.ResumeJob(id);

                var job = _jobInfoRepository.Get(id);
                if (job != null)
                    job.Enabled = true;

                _jobInfoRepository.Update(job);
            }
            catch { }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        public void PauseJob(int id)
        {
            try
            {
                _schedulerContext.PauseJob(id);

                var job = _jobInfoRepository.Get(id);
                if (job != null)
                    job.Enabled = false;

                _jobInfoRepository.Update(job);
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