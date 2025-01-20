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
        public BookAction(IBookRepository bookRepository)
        {
            _repository = bookRepository;
        }
        public void AddBook(Book book)
        {
            book.Id = _repository.GetAll().Any() ? _repository.GetAll().Count + 1 : 1;
            book.IsAviable = true;
            _repository.GetAll().Add(book);
        }
        public bool UpdateBook(int id, Book updatebook)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null)  return false;
            book.Title = updatebook.Title;
            book.Author = updatebook.Author;
            book.Year = updatebook.Year;
            book.Genres = updatebook.Genres;
            return true;
        }
        public bool DeleteBook(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(book => book.Id == id);
            if (book == null) return false;
            _repository.GetAll().Remove(book);
            return true;
        }
        public bool TakeBook(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x =>x.Id == id);
            if (book == null || !book.IsAviable) return false;
            book.IsAviable = false;
            return true;
        }
        public bool ReturnBook(int id)
        {
            var book = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book == null || book.IsAviable) return false;
            book.IsAviable = true;
            return true;
        }
    }
}
