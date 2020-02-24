using System.Web.Security;
using Odh.BooksDemo.Web.Infrastructure.Abstract;
using ODH.SingleSignOn;

namespace Odh.BooksDemo.Web.Infrastructure.Concrete
{
    public class SsoAuthProvider : IAuthProvider
    {
        private readonly ISessionHandler _sessionHandler;
        public SsoAuthProvider(ISessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler;
        }
        public AuthenticationStatus SsoAuthenticate(string sessionId, string applicationId)
        {
            if (string.IsNullOrWhiteSpace(sessionId) || string.IsNullOrWhiteSpace(applicationId))
            {
                return AuthenticationStatus.UserNotFound;
            }

            var singleSignOnService = new UserService();
            var authResult = singleSignOnService.AuthenticateSession(sessionId, applicationId);
            if (authResult.ReturnValue != 10000 || authResult.Results == null)
            {
                return AuthenticationStatus.UserNotFound;
            }

            if (authResult.Results.Active != "1")
            {
                //let the user know that he is deactivated.
                return AuthenticationStatus.Deactivated;
            }
            //keep track of user name
            _sessionHandler.UserName = authResult.Results.UserName;
            //keep track of sso user token
            _sessionHandler.SsoUserToken = authResult.Results.UserID;

            FormsAuthentication.SetAuthCookie(authResult.Results.UserName, false);

            return AuthenticationStatus.Authenticated;
        }
    }
}