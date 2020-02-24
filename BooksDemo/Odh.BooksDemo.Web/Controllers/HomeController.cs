using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Odh.BooksDemo.Domain.Abstract;
using Odh.BooksDemo.Entities;
using Odh.BooksDemo.Web.Infrastructure;

namespace Odh.BooksDemo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBooksDemoUow _uow;

        public HomeController(IBooksDemoUow uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult GetBooks([DataSourceRequest] DataSourceRequest request, string bookName)
        {
            if (request.Sorts != null && request.Sorts.Any())
            {
                if (request.Sorts[0].Member == "Genre") request.Sorts[0].Member = "GenreId";
            }
            var books = string.IsNullOrWhiteSpace(bookName) ? _uow.BookRepository.GetAll() : _uow.BookRepository.GetAll().AsNoTracking().Where(x => x.BookName.Contains(bookName));
            var result = books.ToDataSourceResult(request);
            return Json(result);
        }
        public ActionResult GetBookDetails(int? bookId)
        {
            var book = bookId.HasValue ? _uow.BookRepository.GetById(bookId.Value) : new Book();
            return PartialView("BookDetails", book);
        }

        public JsonResult SaveBook(Book book)
        {
            var msg = "Success";
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                if (book.BookId > 0)
                {
                    _uow.BookRepository.Update(book);
                }
                else
                {
                    _uow.BookRepository.Add(book);
                }
                _uow.Commit();
            }
            else
            {
                msg = ModelState.GetAllErrors();
            }
            var result = new { Response = msg };
            return Json(result);
        }




        [HttpPost]
        public JsonResult Delete(int id)
        {
            var msg = "Success";
            var book = _uow.BookRepository.GetById(id);
            _uow.BookRepository.Delete(book);
            _uow.Commit();
            var result = new { Response = msg };
            return Json(result);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your Application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your Application contact page.";

            return View();
        }
    }
}