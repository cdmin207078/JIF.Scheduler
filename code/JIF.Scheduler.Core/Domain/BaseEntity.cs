using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Scheduler.Core.Domain
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// 主键 Guid
        /// </summary>
        public string Id { get; set; }
    }
}
