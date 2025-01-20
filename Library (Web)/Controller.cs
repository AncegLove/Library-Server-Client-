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
        public Controller(ILibraryService libraryService) => _libraryService = libraryService;

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _libraryService.GetAllBooks();
            if (books.Count == 0) return NotFound("Library is empty.");
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _libraryService.GetBookById(id);
            if (book == null) return NotFound($"Book with this ID: {id} is not found.");
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            _libraryService.AddNewBook(book);
            return Ok("New Book added.");
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Book book, int id)
        {
            if (_libraryService.Update(id, book)) return Ok($"Book with ID: {id}, is updated.");
            return NotFound($"Book with ID: {id} is not found.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_libraryService.Delete(id)) return Ok($"Book with ID: {id}, is deleted.");
            return NotFound($"Book with ID: {id} is not found.");
        }
        [HttpPut("{id}/checkout")]
        public IActionResult Take(int id)
        {            
            if (_libraryService.Take(id)) return Ok("Successfull request.");
            else return NotFound($"Book with this ID: {id} is not found or not aviable.");
        }
        [HttpPut("{id}/return")]
        public IActionResult Return(int id)
        {
            if (_libraryService.ReturnBook(id)) return Ok("Successfull request.");
            else return BadRequest($"Book with ID: {id} is not found or already aviable.");
        }
    }
}
