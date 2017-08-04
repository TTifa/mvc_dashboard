using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ttifa.Infrastructure.Utils
{
    public class IPHelper
    {
        /// <summary>
        /// 获取本地以太网卡IP地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalEthernetIP()
        {
            List<string> strIPs = new List<string>();
            try
            {
                var nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var adapter in nics)
                {
                    //判断是否为以太网卡
                    if (adapter.NetworkInterfaceType != NetworkInterfaceType.Ethernet)
                        continue;

                    var ip = adapter.GetIPProperties();
                    var ipCollection = ip.UnicastAddresses;
                    foreach (var ipadd in ipCollection)
                    {
                        // 判断是否为 IPV4 地址
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            strIPs.Add(ipadd.Address.ToString());
                        }
                    }
                }
            }
            catch
            {
            }

            return strIPs;
        }

        /// <summary>
        /// 获取终端IP
        /// </summary>
        public static string GetRequestIP()
        {
            var ip = string.Empty;

            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                ip = HttpContext.Current.Request.UserHostAddress;
                if (IsIP(ip)) ip = string.Empty;

                if (string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                    ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);

                if (string.IsNullOrEmpty(ip))
                    ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }

            if (string.IsNullOrEmpty(ip) || !IsIP(ip))
                ip = "127.0.0.1";

            return ip.Trim();
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
