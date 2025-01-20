namespace Library__Web_
{
    public interface IBookRepository
    {
        public List<Book> GetAll();
    }
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;
        public BookRepository()
        {
            _books =
            [
                new Book(1, "FirstTitle", "First Author", 2015, new string[] {"horror", "comedy", "detective"}, true),
                new Book(2, "Second Title", "Second Author", 1995, new string[] {"adventure", "comedy"}, true),
                new Book(3, "Third Title", "Third Author", 2001, new string[] {"drama", "fantasy"}, true)
            ];
        }
        public List<Book> GetAll() => _books;
    }
}
