using System;
using System.Web;
using Odh.BooksDemo.Web.Infrastructure.Abstract;

namespace Odh.BooksDemo.Web.Infrastructure.Concrete
{
    public class SessionHandler : ISessionHandler
    {
        public string SsoUserToken
        {
            get { return (string)HttpContext.Current.Session["SSOUserToken"]; }
            set { HttpContext.Current.Session["SSOUserToken"] = value; }
        }
        public int UserId
        {
            get { return HttpContext.Current.Session["USER_IDENTIFIER"] == null ? -1 : (int)HttpContext.Current.Session["USER_IDENTIFIER"]; }
            set { HttpContext.Current.Session["USER_IDENTIFIER"] = value; }
        }
        public string UserName
        {
            get { return (string)HttpContext.Current.Session["USER_NAME"]; }
            set { HttpContext.Current.Session["USER_NAME"] = value; }
        }
        public string[] UserRoles
        {
            get { return HttpContext.Current.Session["ROLES"] == null ? null : (String[])HttpContext.Current.Session["ROLES"]; }
            set { HttpContext.Current.Session["ROLES"] = value; }
        }

        public void Abandon()
        {
            HttpContext.Current.Session["ROLES"] = null;
            HttpContext.Current.Session["SSOUserToken"] = null;
            HttpContext.Current.Session["USER_IDENTIFIER"] = null;
            HttpContext.Current.Session["USER_NAME"] = null;

            HttpContext.Current.Session.Abandon();
        }
    }
}