using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Collections.Generic;

namespace Ttifa.Generate
{
    public class RazorHelper
    {
        public static void InitRazor()
        {
            var templateConfig = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(TemplateBaseExtensions<>),
                TemplateManager = new TemplateManager()
            };

            Engine.Razor = RazorEngineService.Create(templateConfig);
        }

        /// <summary>
        /// 视图模板生成文本
        /// </summary>
        /// <param name="module">视图所属模块</param>
        /// <param name="viewName">视图名称</param>
        /// <param name="viewBag">动态视图数据字典</param>
        /// <returns></returns>
        public static string Generate(string module, string viewName, Dictionary<string, object> viewBag = null)
        {
            var dynamicData = new DynamicViewBag();
            if (viewBag != null)
            {
                foreach (var item in viewBag)
                {
                    dynamicData.AddValue(item.Key, item.Value);
                }
            }

            var name = Common.GetViewRelativePath(module, viewName);
            return Engine.Razor.RunCompile(new NameOnlyTemplateKey(name, ResolveType.Global, null), viewBag: dynamicData);
        }
    }
}
