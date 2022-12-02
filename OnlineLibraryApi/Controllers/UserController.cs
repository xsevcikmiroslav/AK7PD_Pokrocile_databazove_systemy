using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers;
using BusinessLayer.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Authentication;

namespace OnlineLibraryApi.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("{userId}/borrow/{bookId}")]
        public ActionResult BorrowBook(string userId, string bookId)
        {
            _userManager.BorrowBook(userId, bookId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            return _userManager.CreateUser((User)Request.HttpContext.Items["User"], user);
        }

        [HttpDelete("{userId}/delete")]
        public ActionResult DeleteUser(string userId)
        {
            _userManager.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<User>> OrFind(string? firstname = null, string? surname = null, string? address = null, string? pin = null, string? sortBy = null)
        {
            return _userManager.Find(FindType.OR, string.Empty, firstname ?? "", surname ?? "", address ?? "", pin ?? "", sortBy ?? "").ToList();
        }

        [HttpGet("andsearch")]
        public ActionResult<IEnumerable<User>> AndFind(string? firstname = null, string? surname = null, string? address = null, string? pin = null, string? sortBy = null)
        {
            return _userManager.Find(FindType.AND, string.Empty, firstname ?? "", surname ?? "", address ?? "", pin ?? "", sortBy ?? "").ToList();
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(string userId)
        {
            return _userManager.GetUser(userId);
        }

        [HttpGet("{userId}/currentlyborrowed")]
        public ActionResult<IEnumerable<Book>> GetUsersCurrentlyBorrowedBooks(string UserId)
        {
            return _userManager.GetUsersCurrentlyBorrowedBooks(UserId).ToList();
        }

        [HttpGet("{userId}/historicallyborrowed")]
        public ActionResult<IEnumerable<Book>> GetUsersBorrowedBooksHistory(string UserId)
        {
            return _userManager.GetUsersBorrowedBooksHistory(UserId).ToList();
        }

        [HttpPost("{userId}/return/{bookId}")]
        public ActionResult ReturnBook(string userId, string bookId)
        {
            _userManager.ReturnBook(userId, bookId);
            return Ok();
        }

        [HttpPost("{userId}/setpassword")]
        public ActionResult SetPassword(string userId, [FromBody] string password)
        {
            _userManager.SetPassword(userId, password);
            return Ok();
        }

        [HttpPut("update")]
        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            return _userManager.UpdateUser((User)Request.HttpContext.Items["User"], user);
        }
    }
}
