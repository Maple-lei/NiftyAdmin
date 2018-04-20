using Nifty.Model.SystemManagement;
using System.Web.Mvc;

namespace Nifty.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
        
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult ValidationLogin(LoginModel loginModel)
        {
            return null;
        }
    }
}