using RazorEngine.Templating;
using System;

namespace Ttifa.Generate
{
    public class TemplateManager : ITemplateManager
    {
        /// <summary>
        /// Layout定义
        /// </summary>
        private static string sLayout = string.Empty;

        static TemplateManager()
        {
            var path = Common.GetViewPath(Common.ViewPath4Layout);
            sLayout = Common.ReadAllText(path);
        }

        public ITemplateSource Resolve(ITemplateKey key)
        {
            var name = key.Name;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("ITemplateKey.Name");

            var path = Common.GetViewPath(name);
            var template = Common.ReadAllText(path);

            if (key.TemplateType != ResolveType.Layout && !Common.IsLayoutView(path))
            {
                var layout = sLayout;
                var reg = new System.Text.RegularExpressions.Regex(
                    "[\\n]+\\s*Layout\\s*=\\s*\"~/Views/Shared/_[a-zA-Z]*\\.cshtml\";\\s*[\\n]+",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                var rm = reg.Match(template);
                if (rm != null && rm.Success)
                {
                    layout = $"@{{{rm.Value.Replace("\"~/Views/Shared/_", "\"Shared/_")}}}";
                    template = reg.Replace(template, " ");
                }

                template = layout + template;
            }

            // Resolve your template here (ie read from disk)
            // if the same templates are often read from disk you propably want to do some caching here.
            //string template = "Hello @Model.Name, welcome to RazorEngine!";
            // Provide a non-null file to improve debugging
            return new LoadedTemplateSource(template, null);
        }

        public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
        {
            // If you can have different templates with the same name depending on the 
            // context or the resolveType you need your own implementation here!
            // Otherwise you can just use NameOnlyTemplateKey.
            return new NameOnlyTemplateKey(name, resolveType, context);
            // template is specified by full path
            //return new FullPathTemplateKey(name, fullPath, resolveType, context);
        }

        public void AddDynamic(ITemplateKey key, ITemplateSource source)
        {
            // You can disable dynamic templates completely. 
            // This just means all convenience methods (Compile and RunCompile) with
            // a TemplateSource will no longer work (they are not really needed anyway).
            //throw new NotImplementedException("dynamic templates are not supported!");
        }
    }
}
