using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Infrastructure
{
    public class Unity
    {
        private static readonly UnityContainer _container;

        static Unity()
        {
            _container = new UnityContainer();
        }

        public static void Register<TInterface, TImpmentation>() where TImpmentation : TInterface
        {
            _container.RegisterType<TInterface, TImpmentation>();
        }

        public static void RegisterInheritedTypes(Assembly assembly, Type baseType)
        {
            _container.RegisterInheritedTypes(assembly, baseType);
        }

        public static T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static void Teardown(object obj)
        {
            _container.Teardown(obj);
        }
    }
}
