using System.Web.Mvc;

namespace Odh.BooksDemo.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string errMsg)
        {
            //HandleErrorInfo errInfo = new HandleErrorInfo(exception: new System.ApplicationException(errMsg), actionName:"Index", controllerName:"Home" );

            ViewBag.ErrorMessage = errMsg;
            return View("Error");
        }
    }
}