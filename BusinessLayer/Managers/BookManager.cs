using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICurrentBorrowingRepository _currentBorrowingRepository;
        private readonly IBorrowingHistoryRepository _borrowingHistoryRepository;

        public BookManager(
            IBookRepository bookRepository,
            ICurrentBorrowingRepository currentBorrowingRepository,
            IBorrowingHistoryRepository borrowingHistoryRepository)
        {
            _bookRepository = bookRepository;
            _currentBorrowingRepository = currentBorrowingRepository;
            _borrowingHistoryRepository = borrowingHistoryRepository;
        }

        public Book CreateBook(Book book)
        {
            return _bookRepository.Add(book.ToDto()).ToBo();
        }

        public void DeleteAllBooks()
        {
            _currentBorrowingRepository.DeleteAll();
            _borrowingHistoryRepository.DeleteAll();
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

        public IEnumerable<Book> Find(FindType findType, string title, string author = "", int? yearOfPublication = 0, string sortBy = "")
        {
            return
                _bookRepository
                .Find(findType.ToDto(), title, author, yearOfPublication ?? 0, sortBy)
                .Select(b => b.ToBo());
        }

        public Book GetBook(string bookId)
        {
            var book = _bookRepository.Get(bookId).ToBo();
            book.Borrowings = _currentBorrowingRepository
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