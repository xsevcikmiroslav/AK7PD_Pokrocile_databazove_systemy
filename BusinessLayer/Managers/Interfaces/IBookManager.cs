using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IBookManager
    {
        bool BorrowBook(string bookId, string userId);

        Book CreateBook(Book book);

        void DeleteBook(string bookId);

        void DeleteAllBooks();

        IEnumerable<Book> FindBooks(FindType findType, string title, string author, int yearOfPublication, string sortBy);

        Book GetBook(string bookId);

        IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string UserId);

        IEnumerable<Book> GetUsersBorrowedBooksHistory(string UserId);

        void ReturnBook(string bookId, string userId);

        Book UpdateBook(Book book);
    }
}
