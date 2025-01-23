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
            var mockRepository = new Mock<IBookRepository>();
            var mockLogger = new Mock<ILogger<BookAction>>();

            var existingBooks = new List<Book>
        {
            new Book(-1, "Title", "Author", 2027, new string[] { "Fiction" }, false)
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(existingBooks);

            var bookAction = new BookAction(mockRepository.Object, mockLogger.Object);
            var newBook = new Book(-1, "newTitle", "newAuthor", 1995, new string[] { "Fictions" }, false);

            // Act
            bookAction.AddBook(newBook);

            // Assert
            Assert.AreEqual(2, newBook.Id);
            Assert.IsTrue(newBook.IsAviable);
            CollectionAssert.Contains(existingBooks, newBook);
        }
    }
}