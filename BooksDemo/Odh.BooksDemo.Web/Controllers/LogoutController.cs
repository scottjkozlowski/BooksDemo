using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Odh.BooksDemo.Web.Infrastructure.Abstract;
using Odh.BooksDemo.Web.Infrastructure.Concrete;

namespace Odh.BooksDemo.Web.Controllers
{
    public class LogoutController : Controller
    {
        private readonly ISessionHandler _sessionHandler;

        public LogoutController(ISessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler ?? new SessionHandler();
            //unit testing issues
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            _sessionHandler.Abandon();
            //return to gateway
            var gatewayUrl = ConfigurationManager.AppSettings.Get("GatewayLoginURL");
            return Redirect(gatewayUrl);
        }
    }
}