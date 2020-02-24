namespace Odh.BooksDemo.Web.Infrastructure.Abstract
{
    public interface ISessionHandler
    {
        string SsoUserToken { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        string[] UserRoles { get; set; }

        void Abandon();
    }
}
