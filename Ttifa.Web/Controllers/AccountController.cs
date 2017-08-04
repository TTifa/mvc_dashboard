using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ttifa.Entity;
using Ttifa.Infrastructure;
using Ttifa.Infrastructure.Utils;
using Ttifa.Service;
using Ttifa.Web.Filters;

namespace Ttifa.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionLog]
        [AllowAnonymous]
        public ApiResult SignIn(LoginModel model)
        {
            var service = Unity.GetService<IUserService>();
            var state = service.CanLogin(model.UserName, model.Password);
            var result = new ApiResult();
            switch (state)
            {
                case SignInStatus.Success:
                    {
                        var theUser = service.Get(model.UserName);
                        var currentUser = new LoginUser
                        {
                            UserId = theUser.Id,
                            UserName = theUser.UserName,
                            NickName = theUser.NickName
                        };
                        var expires = DateTime.Now.AddHours(2);//默认保存一小时
                        if (model.RememberMe)
                            expires = DateTime.Now.AddDays(7);//记住我保存七天
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                            model.UserName,
                            DateTime.Now,
                            expires,
                            true,//cookie是否为持久性
                            JsonConvert.SerializeObject(currentUser),//UserData
                            "/");
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        cookie.HttpOnly = true;
                        HttpContext.Response.Cookies.Add(cookie);
                        HttpContext.Response.Cookies.Add(new HttpCookie("CurrentUser", currentUser.UserName));
                        //更新登录信息
                        theUser.LastLoginTime = DateTime.Now;
                        theUser.LastLoginIP = IPHelper.GetRequestIP();
                        service.Update(theUser);
                    }; break;
                case SignInStatus.LockedOut:
                    {
                        result.Status = ApiStatus.Fail;
                        result.Message = "user is locked";
                    }; break;
                case SignInStatus.Failure:
                default:
                    result.Status = ApiStatus.Fail;
                    result.Message = "username or password error";
                    break;
            }

            return result;
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Response.Cookies.Clear();
            return RedirectToLocal("/Home/Index");
        }

        public ApiResult Users(int pagesize, int pageindex)
        {
            var service = Unity.GetService<IUserService>();
            var result = new ApiResult(ApiStatus.OK, "success");
            result.Page = new ApiResultPage();
            var list = service.Page(null, pagesize, pageindex, out result.Page.Count, out result.Page.Total);
            result.Data = list.Select(u => new
            {
                u.Id,
                u.UserName,
                u.NickName,
                u.LastLoginTime,
                u.LastLoginIP,
                u.UserStatus
            }).ToList();
            return result;
        }

        [ModelValidate]
        public ApiResult Register(RegisterUser model)
        {
            var service = Unity.GetService<IUserService>();
            if (service.Exist(model.UserName))
                return new ApiResult(ApiStatus.Fail, "user is existed");

            User newuser = new User()
            {
                UserName = model.UserName,
                NickName = model.NickName,
                Password = CryptoHelper.MD5_Encrypt(model.Password),
                UserStatus = 1
            };
            if (!service.Create(newuser))
                return new ApiResult(ApiStatus.Fail, "save fail");

            return new ApiResult();
        }

        public ApiResult UpdatePassword(int id, string pwd)
        {
            var pwd_md5 = CryptoHelper.MD5_Encrypt(pwd);
            var service = Unity.GetService<IUserService>();
            var user = service.Get(id);
            if (user == null) return new ApiResult(ApiStatus.Fail, "用户不存在");
            user.Password = pwd_md5;
            service.Update(user);
            return new ApiResult();
        }

        public ApiResult SetStatus(int id, int state)
        {
            var service = Unity.GetService<IUserService>();
            var user = service.Get(id);
            if (user == null) return new ApiResult(ApiStatus.Fail, "用户不存在");
            user.UserStatus = state;
            service.Update(user);

            return new ApiResult();
        }

        public ApiResult Delete(int id)
        {
            var serivce = Unity.GetService<IUserService>();
            if (!serivce.Delete(id))
                return new ApiResult(ApiStatus.Fail, "delete fail");

            return new ApiResult();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}
