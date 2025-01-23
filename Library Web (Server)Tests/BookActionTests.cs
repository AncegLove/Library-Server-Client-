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
            mockLogger.Verify(logger => logger.LogInformation("Add new book", Times.Once));
            Assert.AreEqual(2, newBook.Id);
            Assert.IsTrue(newBook.IsAviable);
            CollectionAssert.Contains(existingBooks, newBook);
        }
        [TestMethod()]
        public void UpdateBookTest()
        {
            //arrange
            var mockLogger = new Mock<ILogger<BookAction>>();
            var mockRepository = new Mock<IBookRepository>();
            var existingBooks = new List<Book>
            {
                new Book(1, "Title", "Author", 1995, new string[] {"Fiction"}, true)
            };
            mockRepository.Setup(repo => repo.GetAll()).Returns(existingBooks);
            var bookAction = new BookAction(mockRepository.Object, mockLogger.Object);
            var updateBook = new Book(-1, "updateTitle", "updateAuthor", 5333, new string[] { "updateFictions" }, false);
            int bookId = 1;
            //act
            bookAction.UpdateBook(bookId, updateBook);
            //Assert
            mockLogger.Verify(logger => logger.LogInformation($"Update book: {bookId}", Times.Once));
            StringAssert.Equals(updateBook.Title, mockRepository.Object.GetAll().FirstOrDefault(x => x.Id == bookId).Title);
            StringAssert.Equals(updateBook.Author, mockRepository.Object.GetAll().FirstOrDefault(x => x.Id == bookId).Author);
            Assert.AreEqual(updateBook.Year, mockRepository.Object.GetAll().FirstOrDefault(x => x.Id == bookId).Year);
            CollectionAssert.AreEqual(updateBook.Genres, mockRepository.Object.GetAll().FirstOrDefault(x => x.Id == bookId).Genres);

        }
    }
}