using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.Scheduler.Web.Models
{
    public class JobInfo
    {

        /// <summary>
        /// 任务主键编号 - guid
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }


        /// <summary>
        /// 排程表达式
        /// </summary>
        public string CronString { get; set; }
    }
}