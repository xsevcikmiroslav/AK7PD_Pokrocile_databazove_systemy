using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        User GetUser(string UserId);

        User CreateUser(User User);

        User UpdateUser(User UserId);

        void DeleteUser(User UserId);

        void DeleteAllUsers();

        User ApproveUser(User UserId);

        User BanUser(User UserId);

        IEnumerable<User> Find(FindType findType, string firstname, string surname, string address, string pin, string sortBy);
    }
}
