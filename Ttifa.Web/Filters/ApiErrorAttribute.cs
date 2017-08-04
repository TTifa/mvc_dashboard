using System.Web.Mvc;
using Ttifa.Infrastructure.Log;

namespace Ttifa.Web.Filters
{
    public class ApiErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                LogFactory.GetLogger().Error(filterContext.HttpContext.Request.Url.LocalPath, filterContext.Exception);
                //返回异常JSON
                filterContext.Result = new ApiResult
                {
                    Status = ApiStatus.Fail,
                    Message = filterContext.Exception.Message
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}