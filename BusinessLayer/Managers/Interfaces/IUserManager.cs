using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        User ApproveUser(string userId);

        User BanUser(string userId);

        User CreateUser(User user);

        void DeleteUser(string userId);

        void DeleteAllUsers();

        IEnumerable<User> Find(FindType findType, string username, string firstname, string surname, string address, string pin, string sortBy);

        User GetUser(string userId);

        User LoginUser(string username, string password);

        void SetPassword(string userId, string password);

        void LogoutUser();

        User UpdateUser(User user);
    }
}
