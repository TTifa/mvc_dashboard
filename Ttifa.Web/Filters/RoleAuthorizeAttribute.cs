using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ttifa.Web.Filters
{
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        public new string[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            //Action未设置权限时默认允许访问
            if (Roles == null || Roles.Length == 0)
            {
                return true;
            }
            if (Roles.Any(httpContext.User.IsInRole))
            {
                return true;
            }
            
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionName = filterContext.ActionDescriptor.ActionName;
            //var service = Infrastructure.Ioc.GetService<IMenuRoleService>();
            //var roles = service.RoleByUrl(string.Format("/{0}/{1}", controllerName, actionName));
            //if (roles.Count > 0)
            //{
            //    this.Roles = roles.Select(o => o.ToString()).ToArray();
            //}
            //else
            //{
            //    this.Roles = null;
            //}
            //this.Roles = null;
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new ApiResult(ApiStatus.IllegalRequest);
            }
        }
    }
}