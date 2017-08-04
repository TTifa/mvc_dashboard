using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using Ttifa.Infrastructure.Log;

namespace Ttifa.Web.Filters
{
    public class ActionLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var dict = new Dictionary<string, object>();
            var param = filterContext.ActionParameters;
            dict.Add("IP", filterContext.HttpContext.Request.UserHostAddress);
            dict.Add("Url", filterContext.HttpContext.Request.Url.AbsoluteUri);
            if (param.Count > 0)
            {
                dict.Add("Params", param);
            }
            filterContext.HttpContext.Request.Headers.Add("Log", JsonConvert.SerializeObject(dict));

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var log = filterContext.HttpContext.Request.Headers["Log"];
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(log);
            var result = string.Empty;
            var resultType = filterContext.Result?.GetType().Name;
            if (resultType == nameof(ApiResult))
                result = JsonConvert.SerializeObject(filterContext.Result);
            else
                result = resultType;

            dict.Add("Result", result);
            var action = filterContext.HttpContext.Request.Url.LocalPath;
            LogFactory.GetLogger().SetProperties(dict).Info(action);
            filterContext.HttpContext.Request.Headers.Remove("Log");

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}