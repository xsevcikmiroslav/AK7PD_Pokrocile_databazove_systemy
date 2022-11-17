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
        private readonly IUserRepository _userRepository;

        public BookManager(
            IMapper mapper,
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
            _userRepository = UserRepository;
        }

        public void BorrowBook(string bookId, string userId)
        {
            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                DateTimeBorrowed = DateTime.Now,
            };

            _borrowingRepository.Add(_mapper.Map<BorrowingDto>(borrowing));

            var book = _bookRepository.Get(bookId);
            book.NumberOfBorrowed++;
            _bookRepository.Update(book);
        }

        public Book CreateBook(Book book)
        {
            var newEntity = _mapper.Map<BookDto>(book);
            _bookRepository.Add(newEntity);
            return _mapper.Map<Book>(newEntity);
        }

        public void DeleteBook(string bookId)
        {
            _bookRepository.Delete(bookId);
        }

        public void DeleteAllBooks()
        {
            _borrowingRepository.DeleteAll();
            _bookRepository.DeleteAll();
        }

        public IEnumerable<Book> FindBooks(FindType findType, string title, string author, int yearOfPublication, string sortBy)
        {
            var dbFindType = _mapper.Map<DbFindType>(findType);

            return
                _bookRepository
                .Find(dbFindType, title, author, yearOfPublication, sortBy)
                .Select(b => _mapper.Map<Book>(b));
        }

        public Book GetBook(string bookId)
        {
            var entity = _bookRepository.Get(bookId);
            return _mapper.Map<Book>(entity);
        }

        public IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersCurrentBorrowings(userId)
                .Select(r => _mapper.Map<Borrowing>(r));

            return
                usersBorrowings.
                Select(r => _mapper.Map<Book>(_bookRepository.Get(r._id)));
        }

        public IEnumerable<Book> GetUsersBorrowedBooksHistory(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersBorrowingsHistory(userId)
                .Select(r => _mapper.Map<Borrowing>(r));

            return
                usersBorrowings.
                Select(r => _mapper.Map<Book>(_bookRepository.Get(r._id)));
        }

        public void ReturnBook(string bookId, string userId)
        {
            var borrowing = _borrowingRepository.GetByUserAndBook(userId, bookId);
            borrowing.DateTimeReturned = DateTime.Now;
            _borrowingRepository.Update(borrowing);

            var book = _bookRepository.Get(bookId);
            book.NumberOfBorrowed--;
            _bookRepository.Update(book);
        }

        public Book UpdateBook(Book book)
        {
            var updateEntity = _mapper.Map<BookDto>(book);
            _bookRepository.Update(updateEntity);
            return _mapper.Map<Book>(updateEntity);
        }
    }
}