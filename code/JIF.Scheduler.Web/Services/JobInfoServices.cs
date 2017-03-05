using JIF.Scheduler.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.Scheduler.Web.Services
{
    public class JobInfoServices
    {
        public List<JobInfo> GetJobs()
        {
            var result = new List<JobInfo>();

            result.Add(new JobInfo
            {
                Id = "A",
                Name = "调用 Info 方法",
                Description = "无",
                ServiceUrl = "http://localhost:60004/home/info",
                CronString = "0/10 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = "B",
                Name = "调用 Error 方法",
                Description = "无",
                ServiceUrl = "http://localhost:60004/home/error",
                CronString = "0/5 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = "C",
                Name = "调用 Debug 方法",
                Description = "无",
                ServiceUrl = "http://localhost:60004/home/debug",
                CronString = "0/4 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = "D",
                Name = "调用 Trace 方法",
                Description = "无",
                ServiceUrl = "http://localhost:60004/home/trace",
                CronString = "0/3 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = "E",
                Name = "调用 Warn 方法",
                Description = "无",
                ServiceUrl = "http://localhost:60004/home/warn",
                CronString = "0/2 * 8-20 * * ?"
            });

            return result;
        }

        public JobInfo Get(string id)
        {
            return GetJobs().FirstOrDefault(d => d.Id == id);
        }
    }
}