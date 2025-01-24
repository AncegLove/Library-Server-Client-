namespace Library__Web_
{
    public interface IBookAction
    {
        public bool UpdateBook(int id, Book updatebook);
        public void AddBook(Book book);
        public bool DeleteBook(int id);
        public bool TakeBook(int id);
        public bool ReturnBook(int id);
    }
    public class BookAction : IBookAction
    {
        private IBookRepository _repository;
        private ILogger<BookAction> _logger;
        public BookAction(IBookRepository bookRepository, ILogger<BookAction> logger)
        {
            _logger = logger;
            _repository = bookRepository;
        }
        public void AddBook(Book book)
        {
            _logger.LogInformation("Add new book.");
            book.Id = _repository.GetAll().Any() ? _repository.GetAll().Count + 1 : 1;
            book.IsAviable = true;
            _repository.GetAll().Add(book);
        }
        public bool UpdateBook(int id, Book updatebook)
        {
            _logger.LogInformation($"Update book: {id}");
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                _logger.LogWarning($"Book not found with id: {id}");
                return false;
            }
            book.Title = updatebook.Title;
            book.Author = updatebook.Author;
            book.Year = updatebook.Year;
            book.Genres = updatebook.Genres;
            return true;
        }
        public bool DeleteBook(int id)
        {
            _logger.LogInformation($"Delete book: {id}");
            var book = _repository.GetAll().FirstOrDefault(book => book.Id == id);
            if (book == null)
            {
                _logger.LogWarning($"Book not found with id: {id}");
                return false;
            }
            _repository.GetAll().Remove(book);
            return true;
        }
        public bool TakeBook(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null || !book.IsAviable)
            {
                _logger.LogWarning($"Book not found or not aviable with id: {id}");
                return false;
            }
            _logger.LogInformation($"Take book {book.Title}");
            book.IsAviable = false;
            return true;
        }
        public bool ReturnBook(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null || book.IsAviable)
            {
                _logger.LogWarning($"Book not found or already aviable with id: {id}");
                return false;
            }
            _logger.LogInformation($"Return book {book.Title}");
            book.IsAviable = true;
            return true;
        }
    }
}
