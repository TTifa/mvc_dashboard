using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Entity
{
    /// <summary>
    /// 任务实体
    /// </summary>
    public class CronTask
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务执行参数
        /// </summary>
        public string TaskParam { get; set; }

        /// <summary>
        /// 运行频率设置
        /// </summary>
        public string CronExpressionString { get; set; }

        /// <summary>
        /// 任务接口地址
        /// </summary>
        public string ApiUri { get; set; }


        public TaskState Status { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 任务修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 任务最近运行时间
        /// </summary>
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 任务下次运行时间
        /// </summary>
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 任务状态枚举
    /// </summary>
    public enum TaskState
    {
        /// <summary>
        /// 删除
        /// </summary>
        DELETE = -1,
        /// <summary>
        /// 停止状态
        /// </summary>
        STOP = 0,
        /// <summary>
        /// 运行状态
        /// </summary>
        RUN = 1
    }

    /// <summary>
    /// 任务配置的方式
    /// </summary>
    public enum TaskStore
    {
        DB = 1,
        XML = 2
    }
}
