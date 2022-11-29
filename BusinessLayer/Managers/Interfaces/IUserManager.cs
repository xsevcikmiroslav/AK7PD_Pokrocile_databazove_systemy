using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        void BorrowBook(string userId, string bookId);

        User CreateUser(User user);

        void DeleteAllUsers();

        void DeleteUser(string userId);

        IEnumerable<User> Find(FindType findType, string username, string firstname, string surname, string address, string pin, string sortBy);

        User GetUser(string userId);

        IEnumerable<Book> GetUsersBorrowedBooksHistory(string UserId);

        IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string UserId);

        User LoginUser(string username, string password);

        void ReturnBook(string userId, string bookId);

        void SetPassword(string userId, string password);

        User UpdateUser(User updatedBy, User userToUpdate);
    }
}
