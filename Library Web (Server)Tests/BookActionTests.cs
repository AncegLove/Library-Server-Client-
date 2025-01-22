using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library__Web_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;

namespace Library__Web_.Tests
{
    [TestClass()]
    public class BookActionTests
    {
        [TestMethod()]
        public void AddBookTest()
        {
            //arrange
            var mockLocker = new Mock<ILogger<BookAction>>();
            var mockRepository = new Mock<IBookRepository>();
            var bookAction = new BookAction(mockRepository.Object, mockLocker.Object);
            var newBook = new Book(-1, "Title", "Author", 2027, new string[] { "Fiction" }, false);

            //act
            bookAction.AddBook(newBook as Book);
            //assert
            mockLocker.Verify(logger => logger.LogInformation("Add new book."), Times.Once);
            Assert.Equals(1, newBook.Id);
            Assert.IsTrue(newBook.IsAviable);

        }
    }
}