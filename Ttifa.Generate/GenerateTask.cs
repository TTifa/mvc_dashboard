using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Generate
{
    public class GenerateTask
    {
        private Dictionary<string, object> mViewBag = null;

        public GenerateTask(Dictionary<string, object> _viewData)
        {
            mViewBag = _viewData;
        }

        public void GenerateAll()
        {
            var views = GetViews();
            var count = views.Values.Sum(m => m.Count);
            if (count > 0)
            {
                RazorHelper.InitRazor();
                foreach (var view in views)
                {
                    if (view.Value == null || view.Value.Count == 0)
                        continue;

                    foreach (var name in view.Value)
                    {
                        var html = RazorHelper.Generate(view.Key, name, mViewBag);
                        SaveHtml(view.Key, name, html);
                    }
                }
            }
        }

        public void Generate(string module, string view)
        {
            RazorHelper.InitRazor();
            var html = RazorHelper.Generate(module, view, mViewBag);
            SaveHtml(module, view, html);
        }

        private Dictionary<string, List<string>> GetViews()
        {
            var views = new Dictionary<string, List<string>>();
            string path = Common.GetViewFolder();

            var di = new DirectoryInfo(path);
            views[" "] = GetViewNames(di);

            var modules = di.GetDirectories();
            foreach (var item in modules)
            {
                if (Common.IgnoreModules.Contains(item.Name))
                    continue;

                views[item.Name] = GetViewNames(item);
            }

            return views;
        }

        /// <summary>
        /// 获取模块下视图
        /// </summary>
        private List<string> GetViewNames(DirectoryInfo direcory)
        {
            var names = new List<string>();

            var files = direcory.GetFiles("*" + Common.ViewSuffix);
            foreach (var file in files)
            {
                if (file.Name.StartsWith("_"))
                    continue;

                if (!names.Contains(file.Name))
                {
                    names.Add(file.Name.Substring(0, file.Name.Length - file.Extension.Length));
                }
            }

            return names;
        }

        /// <summary>
        /// 保存生成的HTML
        /// </summary>
        public void SaveHtml(string module, string viewName, string html)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Common.GetViewRelativePath(module, viewName) + ".html");

            //创建存放静态页面目录
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            //删除已有的静态页面
            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, html);
        }
    }
}
