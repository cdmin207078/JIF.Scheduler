using Common.Logging;
using JIF.Scheduler.Core.Infrastructure;
using Quartz;
using System;
using System.Net.Http;

namespace JIF.Scheduler.Core.Services.Jobs
{
    [DisallowConcurrentExecution]
    public class HttpServiceJob : IJob
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }

        public string JobName { get; set; }

        public async void Execute(IJobExecutionContext context)
        {
            var _log = EngineContext.Current.Resolve<ILog>();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(ServiceUrl);

                    response.EnsureSuccessStatusCode();

                    string resultStr = await response.Content.ReadAsStringAsync();

                    _log.InfoFormat("ID:[{0}-{1}], Result - {2}", context.JobDetail.Key.Name, JobName, resultStr);
                };

            }
            catch (Exception ex)
            {
                _log.InfoFormat("ID:[{0}-{1}], Result - {2}", context.JobDetail.Key.Name, JobName, ex.Message);
            }
        }
    }
}
