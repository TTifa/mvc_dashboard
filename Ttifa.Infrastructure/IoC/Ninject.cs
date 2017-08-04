using Ninject;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Infrastructure
{
    public class Ninject
    {
        private static readonly StandardKernel _kernel;

        static Ninject()
        {
            _kernel = new StandardKernel();
        }

        public static void Register<TInterface, TImpmentation>() where TImpmentation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImpmentation>();
        }

        public static void RegisterInheritedTypes(Assembly assembly, Type baseType)
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

                        _kernel.Bind(_interface).To(_type);
                    }
                }
            }
        }

        public static T GetService<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
