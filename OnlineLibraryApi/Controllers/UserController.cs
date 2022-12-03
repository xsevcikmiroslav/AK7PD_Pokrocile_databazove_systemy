using BusinessLayer.BusinessObjects;
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
            return _userManager.CreateUser(LoggedUserIsAdmin(), user);
        }

        private bool LoggedUserIsAdmin()
        {
            var loggedInUser = (User)Request.HttpContext.Items["User"];
            return loggedInUser == null ? false : loggedInUser.IsAdmin;
        }

        [HttpDelete("{userId}/delete")]
        public ActionResult DeleteUser(string userId)
        {
            _userManager.DeleteUser(userId);
            return Ok();
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(string userId)
        {
            return _userManager.GetUser(userId);
        }

        [HttpGet("{userId}/currentborrowings")]
        public ActionResult<IEnumerable<Borrowing>> GetUsersCurrentBorrowings(string UserId)
        {
            return _userManager.GetUsersCurrentBorrowings(UserId).ToList();
        }

        [HttpGet("{userId}/borrowingshistory")]
        public ActionResult<IEnumerable<BorrowingHistory>> GetUsersBorrowingsHistory(string UserId)
        {
            return _userManager.GetUsersBorrowingsHistory(UserId).ToList();
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
            return _userManager.UpdateUser(LoggedUserIsAdmin(), user);
        }
    }
}
