using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Odh.BooksDemo.Web.Infrastructure
{
    public class AuthenticateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var routeDataSet = filterContext.RouteData;

            if (HttpContext.Current.Session["SSOUserToken"] == null)
            {
                if (routeDataSet != null &&
                    (
                        (routeDataSet.Values["controller"] != null && routeDataSet.Values["controller"].ToString().ToLower().Equals("account")) ||
                        (routeDataSet.Values["controller"] != null && routeDataSet.Values["controller"].ToString().ToLower().Equals("logout"))
                    ))
                {

                }
                else
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };

                        HttpContext.Current.Response.AddHeader("REQUIRES_AUTH", "1");
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Logout" }, { "action", "Logout" } });
                    }
                }
            }
        }
    }
}