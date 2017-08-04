using System;
using System.Text;

namespace Ttifa.Infrastructure.Utils
{
    public class CodeHelper
    {
        /// <summary>
        /// 随机字符
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="digit">含数字</param>
        /// <param name="lowerWord">含小写字母</param>
        /// <param name="upperWord">含大写字母</param>
        public static string Random(int length, bool digit = true, bool lowerWord = false, bool upperWord = false)
        {
            //48-57   65-90   97-122  
            var random = new System.Random();
            StringBuilder sb = new StringBuilder();
            while (sb.Length < length)
            {
                int i = 0;
                while (!((digit && i >= 48 && i <= 57) || (lowerWord && i >= 97 && i <= 122) || (upperWord && i >= 65 && i <= 90)))
                    i = random.Next(48, 123);
                sb.Append((char)i);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns> 
        public static string OrderNo(CodeType type = CodeType.Null, string prefix = "")
        {
            if (type != CodeType.Null)
                prefix += type.ToString();
            return prefix + DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public enum CodeType
        {
            Null,
            /// <summary>
            /// 订单
            /// </summary>
            D,
            /// <summary>
            /// 付款单
            /// </summary>
            P,
            /// <summary>
            /// 提现订单
            /// </summary>
            W,
            /// <summary>
            /// 分润提现
            /// </summary>
            PF,
            /// <summary>
            /// 代理商分润提现
            /// </summary>
            AP
        }
    }
}
