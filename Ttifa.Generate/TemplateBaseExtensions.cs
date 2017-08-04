using RazorEngine.Templating;
using System.IO;

namespace Ttifa.Generate
{
    /// <summary>
    /// 视图扩展方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TemplateBaseExtensions<T> : TemplateBase<T>
    {
        /// <summary>
        /// 渲染分布视图
        /// </summary>
        /// <param name="module">分布视图所属模块</param>
        /// <param name="viewName">分布视图名称</param>
        /// <param name="model">分布视图Model</param>
        /// <param name="viewBag">动态视图数据字典</param>
        /// <example>
        ///     @Raw(RenderPart("Home", "Footer"))
        /// </example>
        /// <returns></returns>
        public string RenderPart(string module, string viewName, object model = null, DynamicViewBag viewBag = null)
        {
            var path = Common.GetViewPath(module, viewName);
            var source = File.ReadAllText(path);

            return Razor.RunCompile(source, $"{module}\\{viewName}", model?.GetType(), model, viewBag);
        }
    }
}
