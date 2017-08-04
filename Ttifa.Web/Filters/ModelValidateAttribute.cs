using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ttifa.Web.Filters
{
    public class ModelValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //跳过验证
            var passby = filterContext.ActionDescriptor.GetCustomAttributes(typeof(PassValidateAttribute), false).Any() ||
                filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(PassValidateAttribute), false).Any();

            if (passby)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {
                var errorMessage = modelState.Values
                    .SelectMany(m => m.Errors)
                    .Select(m => m.ErrorMessage)
                    .First();

                //直接响应验证结果
                filterContext.Result = new ApiResult(ApiStatus.Fail, errorMessage);
            }
        }
    }
}