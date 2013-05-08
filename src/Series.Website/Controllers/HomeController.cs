using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Series.Website.Models;

namespace Series.Website.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            SignInRequest vm = new SignInRequest()
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(SignInRequest request)
        {
            if (ModelState.IsValid)
            {
                if (Validate(request.Username, request.Password))
                {
                    FormsAuthentication.SetAuthCookie(request.Username, false);
                    return RedirectToLocal(request.ReturnUrl);
                }
            }
            return View(request);
        }

        public ActionResult SignOut()
        {
            return RedirectToAction("SignIn");
        }

        private ActionResult RedirectToLocal(string url)
        {
            if (this.Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index");
        }

        private bool Validate(string username, string password)
        {
            string expectedUsername = ConfigurationManager.AppSettings["login.username"];
            string expectedPassword = ConfigurationManager.AppSettings["login.password"];
            return username == expectedUsername && password == expectedPassword;
        }
    }
}