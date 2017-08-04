using Ttifa.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ttifa.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ApiErrorAttribute());
            filters.Add(new IllegalCharsFilter());
            //filters.Add(new ModelValidateAttribute());//全局注册验证参数
        }
    }
}