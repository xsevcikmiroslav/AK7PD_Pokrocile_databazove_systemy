using AutoMapper;
using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.DTO;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowingRepository _borrowingRepository;

        public BookManager(
            IMapper mapper,
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
        }

        public bool BorrowBook(string bookId, string userId)
        {
            if (!UserCanBorrowBook(userId))
            {
                return false;
            }

            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                DateTimeBorrowed = DateTime.Now,
            };

            _borrowingRepository.Add(_mapper.Map<BorrowingDto>(borrowing));

            return true;
        }

        private bool UserCanBorrowBook(string userId)
        {
            return GetUsersCurrentlyBorrowedBooks(userId).Count() < User.MAX_NUMBER_OF_BORROWED_BOOKS;
        }

        public Book CreateBook(Book book)
        {
            var newEntity = _mapper.Map<BookDto>(book);
            _bookRepository.Add(newEntity);
            return _mapper.Map<Book>(newEntity);
        }

        public void DeleteBook(string bookId)
        {
            var book = GetBook(bookId);
            foreach (var borrowing in book.Borrowings)
            {
                _borrowingRepository.Delete(borrowing._id);
            }
            _bookRepository.Delete(bookId);
        }

        public void DeleteAllBooks()
        {
            _borrowingRepository.DeleteAll();
            _bookRepository.DeleteAll();
        }

        public IEnumerable<Book> Find(FindType findType, string title, string author, int yearOfPublication, string sortBy)
        {
            var dbFindType = _mapper.Map<FindTypeDb>(findType);

            return
                _bookRepository
                .Find(dbFindType, title, author, yearOfPublication, sortBy)
                .Select(b => _mapper.Map<Book>(b));
        }

        public Book GetBook(string bookId)
        {
            var bookDto = _bookRepository.Get(bookId);
            var book = _mapper.Map<Book>(bookDto);
            var borrowingsDto = _borrowingRepository.GetBookCurrentBorrowings(book._id);
            book.Borrowings = borrowingsDto.Select(b => _mapper.Map<Borrowing>(b));
            return book;
        }

        public IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersCurrentBorrowings(userId)
                .Select(r => _mapper.Map<Borrowing>(r));

            return
                usersBorrowings.
                Select(r => _mapper.Map<Book>(_bookRepository.Get(r.BookId)));
        }

        public IEnumerable<Book> GetUsersBorrowedBooksHistory(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersBorrowingsHistory(userId)
                .Select(r => _mapper.Map<Borrowing>(r));

            return
                usersBorrowings.
                Select(r => _mapper.Map<Book>(_bookRepository.Get(r.BookId)));
        }

        public void ReturnBook(string bookId, string userId)
        {
            var borrowing = _borrowingRepository.GetByUserAndBook(userId, bookId);
            borrowing.DateTimeReturned = DateTime.Now;
            _borrowingRepository.Update(borrowing);
        }

        public Book UpdateBook(Book book)
        {
            var updateEntity = _mapper.Map<BookDto>(book);
            _bookRepository.Update(updateEntity);
            return _mapper.Map<Book>(updateEntity);
        }
    }
}