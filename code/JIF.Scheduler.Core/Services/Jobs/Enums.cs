using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Services.Jobs
{
    public enum JobStatus
    {
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Run = 1,
    }
}
