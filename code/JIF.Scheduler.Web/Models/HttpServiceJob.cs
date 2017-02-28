using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using NLog;

namespace JIF.Scheduler.Web.Models
{
    public class HttpServiceJob : IJob
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }

        public string JobName { get; set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public async void Execute(IJobExecutionContext context)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(ServiceUrl);

                    response.EnsureSuccessStatusCode();

                    string resultStr = await response.Content.ReadAsStringAsync();

                    logger.Info("ID:[{0}-{1}], Result - {2}", context.JobDetail.Key.Name, JobName, resultStr);
                };

            }
            catch (Exception ex)
            {
                logger.Error("ID:[{0}-{1}], Result - {2}", context.JobDetail.Key.Name, JobName, ex.Message);
            }
        }
    }
}