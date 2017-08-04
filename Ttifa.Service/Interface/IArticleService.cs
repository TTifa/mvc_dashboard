using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;

namespace Ttifa.Service
{
    public interface IArticleService
    {
        Article Get(int id);

        bool Create(Article model);

        bool Update(Article model);

        List<Article> Page(System.Linq.Expressions.Expression<Func<Article, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total);

        List<ArticleWithoutContent> PageWithoutContent(System.Linq.Expressions.Expression<Func<ArticleWithoutContent, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total);
    }
}
