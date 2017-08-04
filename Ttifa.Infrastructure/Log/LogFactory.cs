using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ttifa.Infrastructure.Log
{
    public class LogFactory
    {
        public static ILog GetLogger()
        {
            return new NLog();
        }
    }
}