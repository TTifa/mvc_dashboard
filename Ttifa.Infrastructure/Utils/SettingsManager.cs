using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttifa.Infrastructure.Utils
{
    public class SettingsManager
    {
        public static string Get(string type, string name)
        {
            var key = $"{type}:{name}";

            return key;
        }

        public static T Get<T>(string type, string name)
        {
            var value = Get(type, name);
            if (string.IsNullOrEmpty(value))
                return default(T);

            return value.To<T>();
        }
    }
}
