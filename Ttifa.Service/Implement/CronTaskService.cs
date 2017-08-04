using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;

namespace Ttifa.Service
{
    public class CronTaskService : BaseService, ICronTaskService
    {
        public CronTask Get(int id)
        {
            using (var db = NewDB())
            {
                var task = db.CronTasks.Where(o => o.Id == id).AsNoTracking().FirstOrDefault();
                return task;
            }
        }

        public bool Create(CronTask model)
        {
            using (var db = NewDB())
            {
                db.CronTasks.Add(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int taskId)
        {
            throw new NotImplementedException();
        }

        public bool Update(CronTask model)
        {
            using (var db = NewDB())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        public List<CronTask> All(TaskState state)
        {
            using (var db = base.NewDB())
            {
                return db.CronTasks.Where(o => o.Status == state).ToList();
            }
        }

        public List<CronTask> Page(Expression<Func<CronTask, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total)
        {
            using (var db = base.NewDB())
            {
                var source = db.CronTasks.OrderBy(o => o.Id);
                if (predicate == null)
                {
                    return base.GetPage<CronTask>(source, pageSize, pageIndex, out pageCount, out total);
                }
                return base.GetPage<CronTask>(source, predicate, pageSize, pageIndex, out pageCount, out total);
            }
        }
    }
}
