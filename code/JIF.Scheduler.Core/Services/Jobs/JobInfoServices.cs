using JIF.Scheduler.Core.Data;
using JIF.Scheduler.Core.Domain;
using JIF.Scheduler.Core.Services.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Services.Jobs
{
    public class JobInfoServices
    {

        private readonly IRepository<JobInfo> _jobInfoRepository;
        private readonly SchedulerContainer _schedulerContainer;


        public JobInfoServices(IRepository<JobInfo> jobInfoRepository,
            SchedulerContainer schedulerContainer)
        {
            _jobInfoRepository = jobInfoRepository;
            _schedulerContainer = schedulerContainer;
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
        /// 新增Job
        /// </summary>
        /// <param name="model"></param>
        public void Add(JobInfoUpdateInputModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Name)
                || string.IsNullOrWhiteSpace(model.ServiceUrl)
                || string.IsNullOrWhiteSpace(model.CronString))
                throw new JIFException("Job 信息不完整");

            var job = new JobInfo();

            job.Name = model.Name;
            job.Description = model.Description;
            job.ServiceUrl = model.ServiceUrl;
            job.CronString = model.CronString;
            job.Enabled = false;

            _jobInfoRepository.Insert(job);
        }

        /// <summary>
        /// 修改Job
        /// </summary>
        public void Update(int id, JobInfoUpdateInputModel model)
        {
            if (id <= 0)
                throw new JIFException("Job 编号不正确");

            var job = Get(id);
            if (job == null)
                throw new JIFException("Job 信息不存在");


            if (string.IsNullOrWhiteSpace(model.Name)
                || string.IsNullOrWhiteSpace(model.ServiceUrl)
                || string.IsNullOrWhiteSpace(model.CronString))
                throw new JIFException("Job 信息不完整");


            job.Name = model.Name;
            job.Description = model.Description;
            job.ServiceUrl = model.ServiceUrl;
            job.CronString = model.CronString;

            _jobInfoRepository.Update(job);
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
                _schedulerContainer.ResumeJob(id);

                var job = _jobInfoRepository.Get(id);
                if (job != null)
                    job.Enabled = true;

                _jobInfoRepository.Update(job);
            }
            catch (Exception ex)
            {
                throw new JIFException("任务唤醒失败 - " + ex.Message);
            }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        public void PauseJob(int id)
        {
            try
            {
                _schedulerContainer.PauseJob(id);

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
