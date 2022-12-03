using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IBookManager
    {
        Book CreateBook(Book book);

        void DeleteBook(string bookId);

        void DeleteAllBooks();

        IEnumerable<Book> Find(FindType findType, string title, string author = "", int? yearOfPublication = 0, string sortBy = "");

        Book GetBook(string bookId);

        Book UpdateBook(Book book);
    }
}
