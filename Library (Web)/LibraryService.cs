namespace Library__Web_
{

    public interface ILibraryService
    {
        public List<Book> GetAllBooks();
        public Book? GetBookById(int id);
        public void AddNewBook(Book book);
        public bool Update(int id, Book book);
        public bool Delete(int id);
        public bool Take(int id);
        public bool ReturnBook(int id);
    }
    public class LibraryService : ILibraryService
    {
        private IBookAction _bookAction;
        private IBookFinder _bookFinder;
        public LibraryService(IBookAction action, IBookFinder finder)
        {
            _bookAction = action;
            _bookFinder = finder;
        }
        public List<Book> GetAllBooks()
        {
            return _bookFinder.FindAll();
        }
        public Book? GetBookById(int bookId)
        {
            return _bookFinder.FindById(bookId);
        }
        public void AddNewBook(Book book)
        {
            _bookAction.AddBook(book);
        }
        public bool Update(int id, Book book) => _bookAction.UpdateBook(id, book);
        public bool Delete(int id) => _bookAction.DeleteBook(id);
        public bool Take(int id) => _bookAction.TakeBook(id);
        public bool ReturnBook(int id) => _bookAction.ReturnBook(id);
    }
}
