using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Odh.BooksDemo.Domain.Abstract;
using Odh.BooksDemo.Web.Infrastructure.Abstract;

namespace Odh.BooksDemo.Web.Controllers
{
    [AllowAnonymous]

    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;
        private readonly IBooksDemoUow _uow;
        private readonly ISessionHandler _sessionHandler;

        public AccountController(IAuthProvider authProvider, IBooksDemoUow uow, ISessionHandler sessionHandler)
        {
            _authProvider = authProvider;
            _uow = uow;
            _sessionHandler = sessionHandler;
        }

        public ActionResult Login(string sessionId, string returnUrl)
        {
            var ssoAppId = ConfigurationManager.AppSettings.Get("SingleSignOnApplicationID");
            var gatewayHomePage = ConfigurationManager.AppSettings.Get("GatewayLoginURL");
            var status = _authProvider.SsoAuthenticate(sessionId, ssoAppId);

            switch (status)
            {
                case AuthenticationStatus.Authenticated:
                    //comment this when using sso auth object
                    FormsAuthentication.SetAuthCookie("TestUser", false);
                    return RedirectToAction("Index", "Home");
                case AuthenticationStatus.ApplicationDeactivated:
                    return RedirectToAction("Index", "Error", new { errMsg = "Your account for this application is not active" });
                case AuthenticationStatus.Deactivated:
                    return RedirectToAction("Index", "Error", new { errMsg = "Your account has been deactivated." });
                case AuthenticationStatus.UserNotFound:
                    return RedirectToAction("Index", "Error", new { errMsg = "User not found.Session Timed out." });
                default:
                    return Redirect(gatewayHomePage);
            }
        }
    }
}