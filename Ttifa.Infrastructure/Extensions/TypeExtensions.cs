using System;

namespace Ttifa.Infrastructure
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 类型相同
        /// </summary>
        /// <param name="type"></param>
        /// <param name="toCompare"></param>
        /// <returns></returns>
        public static bool GenericEq(this Type type, Type toCompare)
        {
            return type.Namespace == toCompare.Namespace && type.Name == toCompare.Name;
        }

        /// 获取类的属性、方法  
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        /// <param name="className">类名</param>  
        //private static Type GetClassInfo(string assemblyName, string className)
        //{
        //    try
        //    {
        //        assemblyName = FileHelper.GetAbsolutePath(assemblyName + ".dll");
        //        Assembly assembly = null;
        //        if (!AssemblyDict.TryGetValue(assemblyName, out assembly))
        //        {
        //            assembly = Assembly.LoadFrom(assemblyName);
        //            AssemblyDict[assemblyName] = assembly;
        //        }
        //        Type type = assembly.GetType(className, true, true);
        //        return type;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
