using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Authentication;

namespace OnlineLibraryApi.Controllers
{
    [Route("admin")]
    [ApiController]
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpPost("approve/{userId}")]
        public ActionResult<User> ApproveUser(string userId)
        {
            return _adminManager.ApproveUser(userId);
        }

        [HttpPost("ban/{userId}")]
        public ActionResult<User> BanUser(string userId)
        {
            return _adminManager.BanUser(userId);
        }

        [HttpGet("backupdatabase")]
        public ActionResult<Backup> BackupDatabase()
        {
            return _adminManager.BackupDatabase();
        }

        [HttpPost("restoredatabase")]
        public ActionResult RestoreDatabase([FromBody] Backup data)
        {
            _adminManager.RestoreDatabase(data);
            return Ok();
        }
    }
}
