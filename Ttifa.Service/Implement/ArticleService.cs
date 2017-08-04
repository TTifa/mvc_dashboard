using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;

namespace Ttifa.Service
{
    public class ArticleService : BaseService, IArticleService
    {
        public bool Create(Article model)
        {
            using (var db = NewDB())
            {
                db.Articles.Add(model);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(Article model)
        {
            using (var db = NewDB())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        public Article Get(int id)
        {
            using (var db = NewDB())
            {
                return db.Articles.Where(o => o.Id == id).AsNoTracking().FirstOrDefault();
            }
        }

        public List<Article> Page(Expression<Func<Article, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total)
        {
            using (var db = base.NewDB())
            {
                var source = db.Articles.OrderBy(o => o.Id);
                if (predicate == null)
                {
                    return base.GetPage<Article>(source, pageSize, pageIndex, out pageCount, out total);
                }
                return base.GetPage<Article>(source, predicate, pageSize, pageIndex, out pageCount, out total);
            }
        }

        public List<ArticleWithoutContent> PageWithoutContent(Expression<Func<ArticleWithoutContent, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total)
        {
            using (var db = base.NewDB())
            {
                var source = db.Articles.OrderBy(o => o.Id).Select(o => new ArticleWithoutContent
                {
                    Id = o.Id,
                    Title = o.Title,
                    Subtitle = o.Subtitle,
                    UpdateTime = o.UpdateTime,
                    Author = o.Author,
                    AuthorId = o.AuthorId
                });

                if (predicate == null)
                {
                    return base.GetPage<ArticleWithoutContent>(source, pageSize, pageIndex, out pageCount, out total);
                }
                return base.GetPage<ArticleWithoutContent>(source, predicate, pageSize, pageIndex, out pageCount, out total);
            }
        }
    }
}
