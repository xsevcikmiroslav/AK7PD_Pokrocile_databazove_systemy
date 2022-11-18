using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        User ApproveUser(User UserId);

        User BanUser(User UserId);

        User CreateUser(User User);

        void DeleteUser(User UserId);

        void DeleteAllUsers();

        IEnumerable<User> Find(FindType findType, string firstname, string surname, string address, string pin, string sortBy);

        User GetUser(string UserId);

        bool LoginUser(string username, string password);

        User UpdateUser(User UserId);
    }
}
