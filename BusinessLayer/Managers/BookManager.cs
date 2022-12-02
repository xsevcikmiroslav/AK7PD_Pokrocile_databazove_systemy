using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowingRepository _borrowingRepository;

        public BookManager(
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository)
        {
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
        }

        public Book CreateBook(Book book)
        {
            return _bookRepository.Add(book.ToDto()).ToBo();
        }

        public void DeleteAllBooks()
        {
            _borrowingRepository.DeleteAll();
            _bookRepository.DeleteAll();
        }

        public void DeleteBook(string bookId)
        {
            var book = GetBook(bookId);
            if (book.Borrowings.Any())
            {
                throw new Exception("Book cannot be deleted, because it is borrowed");
            }
            _bookRepository.Delete(bookId);
        }

        public IEnumerable<Book> Find(FindType findType, string title, string author, int yearOfPublication, string sortBy)
        {
            return
                _bookRepository
                .Find(findType.ToDto(), title, author, yearOfPublication, sortBy)
                .Select(b => b.ToBo());
        }

        public Book GetBook(string bookId)
        {
            var book = _bookRepository.Get(bookId).ToBo();
            book.Borrowings = _borrowingRepository
                .GetBookCurrentBorrowings(book._id)
                .Select(b => b.ToBo());
            return book;
        }

        public Book UpdateBook(Book book)
        {
            _bookRepository.Update(book.ToDto());
            return book;
        }
    }
}