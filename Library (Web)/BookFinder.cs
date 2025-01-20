namespace Library__Web_
{
    public interface IBookFinder
    {
        public List<Book> FindAll();
        public Book? FindById(int id);
    }
    public class BookFinder : IBookFinder
    {
        private IBookRepository _repository;
        public BookFinder(IBookRepository bookRepository)
        {
            this._repository = bookRepository;
        }
        public List<Book> FindAll() => _repository.GetAll();
        public Book? FindById(int id) => _repository.GetAll().FirstOrDefault(x => x.Id == id);
    }
}
