using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Infrastructure
{
    public static class UnityContainerExtensions
    {
        public static void RegisterInheritedTypes(this IUnityContainer container, Assembly assembly, Type baseType)
        {
            var allTypes = assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();
            foreach (var _type in allTypes)
            {
                if (_type.BaseType != null && _type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = _type.GetInterfaces().Where(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
                    foreach (var _interface in typeInterface)
                    {
                        if (_interface == null)
                        {
                            continue;
                        }
                        container.RegisterType(_interface, _type);
                    }
                }
            }
        }
    }
}
