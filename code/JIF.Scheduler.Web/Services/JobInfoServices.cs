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
                Id = Guid.NewGuid().ToString(),
                Name = "调用 Info 方法",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/info",
                CronString = "0/10 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "调用 Error 方法",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/error",
                CronString = "0/5 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "调用 Debug 方法",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/debug",
                CronString = "0/4 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "调用 Trace 方法",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/trace",
                CronString = "0/3 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "调用 Warn 方法",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/warn",
                CronString = "0/2 * 8-20 * * ?"
            });



            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "童话镇 - Info",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/info",
                CronString = "0/10 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "童话镇 - Error",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/error",
                CronString = "0/5 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "童话镇 - Debug",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/debug",
                CronString = "0/4 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "童话镇 - Trace",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/trace",
                CronString = "0/3 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "童话镇 - Warn",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/warn",
                CronString = "0/2 * 8-20 * * ?"
            });




            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "满场飞圆舞曲 - Info",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/info",
                CronString = "0/13 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "满场飞圆舞曲 - Error",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/error",
                CronString = "0/9 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "满场飞圆舞曲 - Debug",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/debug",
                CronString = "0/19 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "满场飞圆舞曲 - Trace",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/trace",
                CronString = "0/1 * 8-20 * * ?"
            });

            result.Add(new JobInfo
            {
                Id = Guid.NewGuid().ToString(),
                Name = "满场飞圆舞曲 - Warn",
                Description = "无",
                ServiceUrl = "http://192.168.1.76:60004/home/warn",
                CronString = "0/22 * 8-20 * * ?"
            });


            return result;
        }
    }
}