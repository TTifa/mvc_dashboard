using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttifa.Entity;
using Ttifa.Infrastructure;
using Ttifa.Service;

namespace Ttifa.Web.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ApiResult Add(CronTask model)
        {
            var service = Unity.GetService<ICronTaskService>();
            model.CreatedTime = DateTime.Now;
            model.Status = TaskState.STOP;
            service.Create(model);
            return new ApiResult();
        }

        public ApiResult Update(CronTask model)
        {
            var service = Unity.GetService<ICronTaskService>();
            var theTask = service.Get(model.Id);
            if (theTask == null)
                return new ApiResult(ApiStatus.Fail, "任务不存在");

            theTask.ModifyTime = DateTime.Now;
            theTask.TaskName = model.TaskName;
            theTask.TaskParam = model.TaskParam;
            theTask.CronExpressionString = model.CronExpressionString;
            theTask.ApiUri = model.ApiUri;
            theTask.Remark = model.Remark;
            service.Update(theTask);
            return new ApiResult();
        }

        public ApiResult SetStatus(int id, TaskState state)
        {
            var service = Unity.GetService<ICronTaskService>();
            var theTask = service.Get(id);
            if (theTask == null)
                return new ApiResult(ApiStatus.Fail, "任务不存在");

            theTask.Status = state;
            service.Update(theTask);
            return new ApiResult();
        }

        public ApiResult Page(int pagesize, int pageindex)
        {
            var service = Unity.GetService<ICronTaskService>();
            var result = new ApiResult(ApiStatus.OK, "success");
            result.Page = new ApiResultPage();
            var list = service.Page(t => t.Status != TaskState.DELETE, pagesize, pageindex, out result.Page.Count, out result.Page.Total);
            result.Data = list;

            return result;
        }

        public string Test(string param)
        {
            return param;
        }

        public string TimeSpan()
        {
            var start = new DateTime(2017, 9, 2);
            var startTime = new TimeSpan(start.Ticks);
            var endTime = new TimeSpan(DateTime.Now.Ticks);

            return endTime.Subtract(startTime).Days.ToString();
        }
    }
}