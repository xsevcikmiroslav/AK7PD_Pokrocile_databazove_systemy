using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers;
using BusinessLayer.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Authentication;

namespace OnlineLibraryApi.Controllers
{
    [Route("Book")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private IBookManager _bookManager;

        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpPost("create")]
        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            return _bookManager.CreateBook(book);
        }

        [HttpDelete("{bookId}/delete")]
        public ActionResult DeleteBook(string bookId)
        {
            _bookManager.DeleteBook(bookId);
            return NoContent();
        }

        [HttpGet("{findType}/{title}/{author}/{yearOfPublication}/{sortBy}")]
        public ActionResult<IEnumerable<Book>> Find(FindType findType, string title, string author, int yearOfPublication, string sortBy)
        {
            return _bookManager.Find(findType, title, author, yearOfPublication, sortBy).ToList();
        }

        [HttpGet("{bookId}")]
        public ActionResult<Book> GetBook(string bookId)
        {
            return _bookManager.GetBook(bookId);
        }

        [HttpPut("update")]
        public ActionResult<Book> UpdateBook([FromBody] Book book)
        {
            return _bookManager.UpdateBook(book);
        }
    }
}
