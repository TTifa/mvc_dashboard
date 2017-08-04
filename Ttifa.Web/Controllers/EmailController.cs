using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.Http;
using Ttifa.Infrastructure.Utils;

namespace Ttifa.Web.Controllers
{
    public class EmailController : ApiController
    {
        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="sendto">收件人</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="filePaths">附件路径</param>
        /// <returns></returns>
        public ApiResult Send(string sendto, string title, string content, params string[] filePaths)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(sendto);
            var email = SettingsManager.Get("ExportSettings", "email");//发信地址
            var displayname = SettingsManager.Get("ExportSettings", "emaildisplayname");//昵称
            var pass = SettingsManager.Get("ExportSettings", "emailpassword");//发信邮箱密码
            var smtp = SettingsManager.Get<Dictionary<string, string>>("ExportSettings", "smtp");
            msg.From = new MailAddress(email, displayname, System.Text.Encoding.UTF8);

            //附件
            if (filePaths != null)
            {
                foreach (string file in filePaths)
                {
                    if (System.IO.File.Exists(file))
                    {
                        msg.Attachments.Add(new Attachment(file));
                    }
                }
            }

            msg.Subject = title;//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = content;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = false;//是否是HTML邮件 
            msg.Priority = MailPriority.Normal;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Host = smtp["Host"];
            client.Port = Convert.ToInt32(smtp["Port"]);
            client.EnableSsl = smtp["EnableSSL"] == "1";//是否使用ssl
            client.Credentials = new System.Net.NetworkCredential(email, pass);
            try
            {
                client.Send(msg);
                return new ApiResult();
            }
            catch (Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="sendto">收件人</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="files">附件</param>
        /// <returns></returns>
        private ApiResult Send(string sendto, string title, string content, Dictionary<string, Stream> files)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(sendto);
            var email = SettingsManager.Get("ExportSettings", "email");//发信地址
            var displayname = SettingsManager.Get("ExportSettings", "emaildisplayname");//昵称
            var pass = SettingsManager.Get("ExportSettings", "emailpassword");
            var smtp = SettingsManager.Get<Dictionary<string, string>>("ExportSettings", "smtp");
            msg.From = new MailAddress(email, displayname, System.Text.Encoding.UTF8);

            //附件
            if (files != null)
            {
                foreach (var file in files)
                {
                    msg.Attachments.Add(new Attachment(file.Value, file.Key));
                }
            }

            msg.Subject = title;//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = content;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = false;//是否是HTML邮件 
            msg.Priority = MailPriority.Normal;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Host = smtp["Host"];
            client.Port = Convert.ToInt32(smtp["Port"]);
            client.EnableSsl = smtp["EnableSSL"] == "1";
            client.Credentials = new System.Net.NetworkCredential(email, pass);
            client.Send(msg);

            return new ApiResult();
        }
    }
}
