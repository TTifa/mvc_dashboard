using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Ttifa.Web.Filters
{
    /// <summary>
    /// 非法字符过滤
    /// </summary>
    public class IllegalCharsFilter : ActionFilterAttribute
    {
        //过滤跨脚本攻击漏洞
        private const string ApiRegexScript = @"<\s*script\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FilterIllegalChar(filterContext);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="filterContext"></param>
        private void FilterIllegalChar(ActionExecutingContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
            if (string.IsNullOrEmpty(url))
                return;

            //过滤跨脚本攻击漏洞
            if (Regex.IsMatch(url, ApiRegexScript, RegexOptions.IgnoreCase))
            {
                filterContext.HttpContext.Response.Write(new ApiResult(ApiStatus.IllegalRequest, "非法请求").ToString());
                return;
            }
        }
    }
}