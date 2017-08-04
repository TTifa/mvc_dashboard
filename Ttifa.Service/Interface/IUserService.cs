using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;

namespace Ttifa.Service
{
    public interface IUserService
    {
        User Get(int id);

        User Get(string username);

        bool Exist(string username);

        SignInStatus CanLogin(string username, string password);

        bool Create(User user);

        bool Update(User user);

        bool Delete(int id);

        List<User> All();

        List<User> Page(System.Linq.Expressions.Expression<Func<User, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total);
    }
}
