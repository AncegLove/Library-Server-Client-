using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
namespace Library__Web_
{
    [Route("api/books")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private ILibraryService _libraryService;
        private ILogger<Controller> _logger;
        public Controller(ILibraryService libraryService, ILogger<Controller> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Take request to get all books.");
            var books = _libraryService.GetAllBooks();
            if (books.Count == 0)
            {
                _logger.LogWarning("Book not found.");
                return NotFound("Library is empty.");                
            }
            _logger.LogInformation("Books sended to client.");
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Take request to get book by id: {id}");
            var book = _libraryService.GetBookById(id);
            if (book == null)
            {
                _logger.LogWarning("Theres no book in library.");
                return NotFound($"Book with this ID: {id} is not found.");
            }
            _logger.LogInformation("Return books.");
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            _logger.LogInformation($"Take request to add book: {book}");
            _libraryService.AddNewBook(book);
            _logger.LogInformation("Book was added.");
            return Ok("New Book added.");
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Book book, int id)
        {
            _logger.LogInformation($"Take request to update information about book with id: {id} : {book}");
            if (_libraryService.Update(id, book))
            {
                _logger.LogInformation($"Information about with id: {id}, updated.");
                return Ok($"Book with ID: {id}, is updated.");
            }
            _logger.LogWarning($"Book with id: {id} not found.");
            return NotFound($"Book with ID: {id} is not found.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Take request to delete book by id: {id}");
            if (_libraryService.Delete(id))
            {
                _logger.LogInformation($"Book with id: {id} successfully deleted.");
                return Ok($"Book with ID: {id}, is deleted.");
            }
            _logger.LogWarning($"Book with id: {id} not found.");
            return NotFound($"Book with ID: {id} is not found.");
        }
        [HttpPut("{id}/checkout")]
        public IActionResult Take(int id)
        {
            _logger.LogInformation($"Take request to take book with id: {id}");
            if (_libraryService.Take(id))
            {
                _logger.LogInformation($"Book with id: {id} was taken");
                return Ok("Successfull request.");
            }
            else
            {
                _logger.LogWarning($"Book with id: {id} not found.");
                return NotFound($"Book with this ID: {id} is not found or not aviable.");
            }
        }
        [HttpPut("{id}/return")]
        public IActionResult Return(int id)
        {
            _logger.LogInformation($"Take request to return book with id: {id}");
            if (_libraryService.ReturnBook(id))
            {
                _logger.LogInformation($"Book with id: {id} was return");
                return Ok("Successfull request.");
            }
            else
            {
                _logger.LogWarning($"Book with id: {id} not found");
                return BadRequest($"Book with ID: {id} is not found or already aviable.");
            }
        }
    }
}
