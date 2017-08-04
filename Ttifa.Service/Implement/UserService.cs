using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ttifa.Entity;
using Ttifa.Infrastructure.Utils;

namespace Ttifa.Service
{
    public class UserService : BaseService, IUserService
    {
        public User Get(int id)
        {
            using (var db = NewDB())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User Get(string username)
        {
            using (var db = NewDB())
            {
                return db.Users.FirstOrDefault(u => u.UserName == username);
            }
        }

        public bool Exist(string username)
        {
            using (var db = NewDB())
            {
                return db.Users.Any(u => u.UserName == username);
            }
        }

        public SignInStatus CanLogin(string username, string password)
        {
            using (var db = NewDB())
            {
                var pass_md5 = CryptoHelper.MD5_Encrypt(password);
                var loginUser = db.Users.FirstOrDefault(o => o.UserName == username && o.Password == pass_md5);
                if (loginUser == null)
                {
                    return SignInStatus.Failure;
                }
                if (loginUser.UserStatus == -1)
                {
                    return SignInStatus.LockedOut;
                }

                return SignInStatus.Success;
            }
        }

        public bool Create(User user)
        {
            using (var db = NewDB())
            {
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(User user)
        {
            using (var db = NewDB())
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var db = NewDB())
            {
                db.Database.ExecuteSqlCommand($"delete from Users where Id={id}");
            }

            return true;
        }


        public List<User> All()
        {
            using (var db = NewDB())
            {
                return db.Users.AsNoTracking().ToList();
            }
        }

        public List<User> Page(System.Linq.Expressions.Expression<Func<User, bool>> predicate, int pageSize, int pageIndex, out int pageCount, out int total)
        {
            using (var db = base.NewDB())
            {
                var source = db.Users.OrderBy(o => o.Id);
                if (predicate == null)
                {
                    return base.GetPage<User>(source, pageSize, pageIndex, out pageCount, out total);
                }
                return base.GetPage<User>(source, predicate, pageSize, pageIndex, out pageCount, out total);
            }
        }
    }
}
