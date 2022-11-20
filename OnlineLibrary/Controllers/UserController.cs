using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OnlineLibrary.Controllers
{
    public class UserController : Controller
    {
        private IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var user = _userManager.GetUser("637a3f571f3d0cf2f3b7aea8");

            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(BusinessLayer.BusinessObjects.User user)
        {
            _userManager.UpdateUser(user);

            return View(user);
        }
    }
}
