using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Security;

namespace Ttifa.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private LoginUser _CurrentUser;

        protected LoginUser CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                    _CurrentUser = !User.Identity.IsAuthenticated ? null :
                        JsonConvert.DeserializeObject<LoginUser>((User.Identity as FormsIdentity).Ticket.UserData);

                return _CurrentUser;
            }
        }
    }
}
