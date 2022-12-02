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
            return Ok();
        }
        
        [HttpGet("search")]
        public ActionResult<IEnumerable<Book>> OrFind(string? title = null, string? author = null, int? yearOfPublication = null, string? sortBy = null)
        {
            return _bookManager.Find(FindType.OR, title ?? "", author ?? "", yearOfPublication ?? 0, sortBy ?? "").ToList();
        }

        [HttpGet("andsearch")]
        public ActionResult<IEnumerable<Book>> AndFind(string? title = null, string? author = null, int? yearOfPublication = null, string? sortBy = null)
        {
            return _bookManager.Find(FindType.AND, title ?? "", author ?? "", yearOfPublication ?? 0, sortBy ?? "").ToList();
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
