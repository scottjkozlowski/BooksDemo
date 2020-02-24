namespace Odh.BooksDemo.Web.Infrastructure.Abstract
{
    public enum AuthenticationStatus
    {
        Authenticated,
        UserNotFound,
        Deactivated,
        ApplicationDeactivated
    }

    public interface IAuthProvider
    {
        AuthenticationStatus SsoAuthenticate(string sessionId, string applicationId);
    }
}
