using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Domain
{
    public class JobInfo : BaseEntity
    {
        ///// <summary>
        ///// 任务主键编号 - guid
        ///// </summary>
        //public string JobKey { get; set; }

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

        /// <summary>
        /// 是否启用. 启用 - true, 停用 - false
        /// </summary>
        public bool Enabled { get; set; }

    }
}
