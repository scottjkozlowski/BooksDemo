using System.Web.Mvc;
using Odh.BooksDemo.Web.Infrastructure.Abstract;

namespace Odh.BooksDemo.Web.Controllers
{
    public class NavController : Controller
    {
        private readonly ISessionHandler _sessionHandler;

        public NavController(ISessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler;
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            if (_sessionHandler != null)
            {
                ViewBag.UserName = _sessionHandler.UserName;
            }
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult PageFooter()
        {
            return PartialView();
        }
    }
}