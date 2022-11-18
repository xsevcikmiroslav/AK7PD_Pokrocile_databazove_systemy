using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        User ApproveUser(User userId);

        User BanUser(User userId);

        User CreateUser(User user);

        void DeleteUser(User userId);

        void DeleteAllUsers();

        IEnumerable<User> Find(FindType findType, string firstname, string surname, string address, string pin, string sortBy);

        User GetUser(string userId);

        bool LoginUser(string username, string password);

        User UpdateUser(User userId);
    }
}
