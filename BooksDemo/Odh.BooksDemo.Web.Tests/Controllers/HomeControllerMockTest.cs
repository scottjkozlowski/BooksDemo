using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Odh.BooksDemo.Domain.Abstract;
using Odh.BooksDemo.Entities;
using Odh.BooksDemo.Web.Controllers;

namespace Odh.BooksDemo.Web.Tests.Controllers
{

    [TestClass]
    public class HomeControllerMockTest
    {
        private readonly Mock<IBooksDemoUow> _mockUow;
        private readonly Mock<IBookRepository> _mockBooks = new Mock<IBookRepository>();

        public HomeControllerMockTest()
        {
            _mockUow = new Mock<IBooksDemoUow>();
            _mockBooks.Setup(m => m.GetAll()).Returns(new[]
                                                     {
                                                             new Book() {BookId = 1, BookName = "Harry Potter and Prisoner of Azkaban",IsbNumber = "00873"},
                                                             new Book() {BookId = 2, BookName = "Testbook2",IsbNumber = "1234"},
                                                             new Book() {BookId = 3, BookName = "Testbook3",IsbNumber = "9897"},
                                                         }.AsQueryable());
            _mockUow.Setup(m => m.BookRepository).Returns(_mockBooks.Object);
        }

        [TestMethod]
        public void CanGetBooks()
        {
            var controller = new HomeController(_mockUow.Object);
            var kendoDataRequest = new DataSourceRequest();
            var result = controller.GetBooks(kendoDataRequest, null) as JsonResult;
            Assert.IsTrue(result != null);
            dynamic kendoResultData = result.Data;
            var model = kendoResultData.Data as List<Book>;
            Assert.IsTrue(model != null);
            Assert.IsTrue(model.Any());
        }

        [TestMethod]
        public void CanSearchBooks()
        {
            var controller = new HomeController(_mockUow.Object);
            var kendoDataRequest = new DataSourceRequest();
            var result = controller.GetBooks(kendoDataRequest, "Harry") as JsonResult;
            Assert.IsTrue(result != null);
            dynamic kendoResultData = result.Data;
            var model = kendoResultData.Data as List<Book>;
            Assert.IsTrue(model != null);
            Assert.IsTrue(model.Count == 1);
        }

        [TestMethod]
        public void CanSaveBook()
        {
            var controller = new HomeController(_mockUow.Object);
            var result = controller.SaveBook(new Book()
            {
                BookId = 1,
                BookName = "Harry Potter and Prisoner of Azkaban",
                GenreId = 1,
                IsbNumber = "00873",
                PublishedDate = DateTime.Parse("06/12/2001")
            });
            Assert.IsTrue(result != null);
            var data = result.Data;
            Assert.IsTrue(data != null);
            string testValue = GetValueFromJsonResult<string>(result, "Response");
            Assert.IsTrue("Success" == testValue);
        }

        [TestMethod]
        public void CanDeleteBook()
        {
            var controller = new HomeController(_mockUow.Object);
            var result = controller.Delete(1);
            Assert.IsTrue(result != null);
            var data = result.Data;
            Assert.IsTrue(data != null);
            string testValue = GetValueFromJsonResult<string>(result, "Response");
            Assert.IsTrue("Success" == testValue);
        }

        private T GetValueFromJsonResult<T>(JsonResult jsonResult, string propertyName)
        {
            var property =
                jsonResult.Data.GetType()
                .GetProperties()
                .FirstOrDefault(p => String.CompareOrdinal(p.Name, propertyName) == 0);

            if (null == property)
                throw new ArgumentException("propertyName not found", propertyName);
            return (T)property.GetValue(jsonResult.Data, null);
        }

    }
}

