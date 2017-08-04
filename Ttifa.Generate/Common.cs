using System;
using System.IO;
using System.Text;

namespace Ttifa.Generate
{
    /// <summary>
    /// 公共函数
    /// </summary>
    internal class Common
    {
        /// <summary>
        /// 视图后缀
        /// </summary>
        public const string ViewSuffix = ".cshtml";

        /// <summary>
        /// Layout视图后缀
        /// </summary>
        public const string ViewSuffix4Layout = "Layout.cshtml";

        /// <summary>
        /// Layout视图前缀
        /// </summary>
        //public const string ViewPreffix4Layout = "_Layout";

        /// <summary>
        /// Layout定义视图路径
        /// </summary>
        public const string ViewPath4Layout = "_ViewStart4Generate.cshtml";

        /// <summary>
        /// 视图所在文件夹
        /// </summary>
        public const string ViewFolder = "Views";

        /// <summary>
        /// 忽略生成的模块
        /// </summary>
        public static readonly string[] IgnoreModules = new string[] { "Generate", "Error", "Test" };

        /// <summary>
        /// 获取视图相对路径
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="viewName">视图名称</param>
        /// <returns></returns>
        public static string GetViewRelativePath(string module, string viewName) => string.IsNullOrEmpty(module?.Trim()) ? viewName : $"{module}\\{viewName}";

        /// <summary>
        /// 获取视图完整路径地址
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="viewName">视图名称</param>
        /// <returns></returns>
        public static string GetViewPath(string module, string viewName) => GetViewPath(GetViewRelativePath(module, viewName));

        /// <summary>
        /// 获取视图完整路径地址
        /// </summary>
        /// <param name="relativePath">视图相对路径（模块名称+"\\"+视图名称）</param>
        /// <returns></returns>
        public static string GetViewPath(string relativePath)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ViewFolder, relativePath);
            if (!path.ToLower().EndsWith(ViewSuffix))
                path += ViewSuffix;

            return path;
        }

        /// <summary>
        /// 获取视图所在目录
        /// </summary>
        /// <returns></returns>
        public static string GetViewFolder()
        {
            return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, ViewFolder);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }

        /// <summary>
        /// 根据视图后缀判断视图是否为Layout视图
        /// </summary>
        /// <param name="viewPath">视图路径</param>
        /// <returns></returns>
        public static bool IsLayoutView(string viewPath)
        {
            if (string.IsNullOrEmpty(viewPath))
                return false;

            var path = viewPath.ToLower();
            if (!path.EndsWith(ViewSuffix))
                path += ViewSuffix;

            //if (path.Substring(path.LastIndexOf('\\')).Trim('\\').StartsWith(ViewPreffix4Layout.ToLower()))
            //    return true;

            if (path.EndsWith(ViewSuffix4Layout.ToLower()))
                return true;

            return false;
        }
    }
}
