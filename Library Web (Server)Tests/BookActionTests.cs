using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library__Web_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using Library_Web__Server_Tests;

namespace Library__Web_.Tests
{
    [TestClass()]
    public class BookActionTests
    {
        [TestMethod()]
        public void AddBook_ShouldAssign_WhenBookExist()
        {
            //arrange
            var mockRepository = new Mock<IBookRepository>();
            var mockLogger = new MockLogger<BookAction>();

            var existingBooks = new List<Book>
        {
            new Book(-1, "Title", "Author", 2027, new string[] { "Fiction" }, false)
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(existingBooks);

            var bookAction = new BookAction(mockRepository.Object, mockLogger);
            var newBook = new Book(-1, "newTitle", "newAuthor", 1995, new string[] { "Fictions" }, false);

            // Act
            bookAction.AddBook(newBook);

            // Assert
            CollectionAssert.Contains(mockLogger.LoggedMessages, "Add new book.");
            Assert.AreEqual(2, newBook.Id);
            Assert.IsTrue(newBook.IsAviable);
            CollectionAssert.Contains(existingBooks, newBook);
        }

        [TestMethod()]
        public void UpdateBook_ShouldReAssignBookByID_WhenBookFound()
        {
            //arrange
            var mockLogger = new MockLogger<BookAction>();
            var mockRepository = new Mock<IBookRepository>();
            var existingBooks = new List<Book>
            {
                new Book(1, "Title", "Author", 1995, new string[] {"Fiction"}, true)
            };
            mockRepository.Setup(repo => repo.GetAll()).Returns(existingBooks);
            var bookAction = new BookAction(mockRepository.Object, mockLogger);
            var updateBook = new Book(-1, "updateTitle", "updateAuthor", 5333, new string[] { "updateFictions" }, false);
            int bookId = 1;
            //act
            var result = bookAction.UpdateBook(bookId, updateBook);
            //Assert
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(mockLogger.LoggedMessages, $"Book not found with id: {bookId}");
            CollectionAssert.Contains(mockLogger.LoggedMessages, $"Update book: {bookId}");
            StringAssert.Equals(updateBook.Title, mockRepository.Object.GetAll().First(x => x.Id == bookId).Title);
            StringAssert.Equals(updateBook.Author, mockRepository.Object.GetAll().First(x => x.Id == bookId).Author);
            Assert.AreEqual(updateBook.Year, mockRepository.Object.GetAll().First(x => x.Id == bookId).Year);
            CollectionAssert.AreEqual(updateBook.Genres, mockRepository.Object.GetAll().First(x => x.Id == bookId).Genres);

        }
        [TestMethod()]
        public void UpdateBook_ShouldReturnFalse_WhenBookNotFound()
        {
            //arrange
            var mockLogger = new MockLogger<BookAction>();
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<Book>());
            var bookAction = new BookAction(mockRepository.Object, mockLogger);
            var updateBook = new Book(4, "updateTitle", "updateAuthor", 5333, new string[] { "updateFiction" }, true);
            int bookId = 4;
            //act
            var result = bookAction.UpdateBook(bookId, updateBook);
            //assert
            Assert.IsFalse(result);
            CollectionAssert.Contains(mockLogger.LoggedMessages, $"Book not found with id: {bookId}");
        }

        [TestMethod()]
        public void DeleteBook_ShouldReturnTrue_WhenBookFound()
        {
            //arrange
            var mockRepository = new Mock<IBookRepository>();
            var mockLogger = new MockLogger<BookAction>();

            var existingBooks = new List<Book>
        {
            new Book(1, "Title", "Author", 2027, new string[] { "Fiction" }, false),
            new Book(2, "SecondTitle", "SecondAuthor", 1317, new string[]{"SecondFiction"}, true)
        };

            mockRepository.Setup(repo => repo.GetAll()).Returns(existingBooks);

            var bookAction = new BookAction(mockRepository.Object, mockLogger);
            var newBook = new Book(-1, "newTitle", "newAuthor", 1995, new string[] { "Fictions" }, false);
            int bookId = 2;
            //act
            var result = bookAction.DeleteBook(bookId);
            //assert
            CollectionAssert.Contains(mockLogger.LoggedMessages, $"Delete book: {bookId}");
            CollectionAssert.DoesNotContain(mockLogger.LoggedMessages, $"Book not found with id: {bookId}");
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(mockRepository.Object.GetAll(), mockRepository.Object.GetAll().FirstOrDefault(x => x.Id == bookId));
        }
        [TestMethod()]
        public void DeleteBook_ShouldReturnFalse_WhenBookNotFound()
        {
            var mockRepository = new Mock<IBookRepository>();
            var mockLogger = new MockLogger<BookAction>();
            var bookAction = new BookAction(mockRepository.Object, mockLogger);
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<Book>());
            int bookId = 2;

            var result = bookAction.DeleteBook(bookId);

            CollectionAssert.Contains(mockLogger.LoggedMessages, $"Book not found with id: {bookId}");
            Assert.IsFalse(result);
        }
    }
}