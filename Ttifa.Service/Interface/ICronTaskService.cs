using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;

namespace Ttifa.Service
{
    public interface ICronTaskService
    {
        CronTask Get(int id);
        bool Create(CronTask model);
        bool Update(CronTask model);
        bool Delete(int taskId);
        List<CronTask> All(TaskState state);
        List<CronTask> Page(System.Linq.Expressions.Expression<Func<CronTask, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total);
    }
}
