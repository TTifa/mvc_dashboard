using System;
using System.Web.Mvc;
using Ttifa.Entity;
using Ttifa.Infrastructure;
using Ttifa.Service;
using Ttifa.Web.Filters;

namespace Ttifa.Web.Controllers
{
    public class ArticleController : BaseController
    {
        #region 视图
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        #endregion

        public ApiResult Get(int id)
        {
            var service = Unity.GetService<ArticleService>();
            var article = service.Get(id);
            if (article == null)
                return new ApiResult(ApiStatus.Fail, "未找到该文章");

            return new ApiResult(data: article);
        }

        [HttpPost]
        [ActionLog]
        public ApiResult Create(Article model)
        {
            var service = Unity.GetService<ArticleService>();
            model.UpdateTime = DateTime.Now;
            model.Author = CurrentUser.NickName;
            model.AuthorId = CurrentUser.UserId;
            if (!service.Create(model))
                return new ApiResult(ApiStatus.Fail, "添加失败");

            return new ApiResult();
        }

        [ActionLog]
        public ApiResult Update(Article model)
        {
            var service = Unity.GetService<IArticleService>();
            var originData = service.Get(model.Id);
            if (originData == null)
                return new ApiResult(ApiStatus.Fail, "文章不存在");

            model.UpdateTime = DateTime.Now;
            model.AuthorId = CurrentUser.UserId;
            model.Author = CurrentUser.UserName;
            if (!service.Update(model))
                return new ApiResult(ApiStatus.Fail, "更新失败");

            return new ApiResult();
        }

        public ApiResult List(int pageSize, int pageIndex)
        {
            var result = new ApiResult();
            result.Page = new ApiResultPage();
            var service = Unity.GetService<IArticleService>();
            result.Data = service.PageWithoutContent(null, pageSize, pageIndex, out result.Page.Count, out result.Page.Total);

            return result;
        }
    }
}