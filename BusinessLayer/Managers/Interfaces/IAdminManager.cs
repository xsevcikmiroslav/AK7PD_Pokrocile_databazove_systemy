using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IAdminManager
    {
        User ApproveUser(string userId);

        Backup BackupDatabase();

        User BanUser(string userId);

        IEnumerable<User> Find(FindType findType, string username, string firstname = "", string surname = "", string address = "", string pin = "", string sortBy = "");

        void RestoreDatabase(Backup data);
    }
}
