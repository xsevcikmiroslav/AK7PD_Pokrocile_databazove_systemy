using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using BusinessLayer.Password;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IUserRepository _userRepository;

        public UserManager(
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
            _userRepository = UserRepository;
        }

        public void BorrowBook(string userId, string bookId)
        {
            if (!UserCanBorrowBook(userId))
            {
                throw new Exception("User cannot borrow another book");
            }

            var book = _bookRepository.Get(bookId).ToBo();
            if (!book.CanBeBorrowed)
            {
                throw new Exception("No free licence for this book");
            }

            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                DateTimeBorrowed = DateTime.Now,
            };

            _borrowingRepository.Add(borrowing.ToDto());
        }

        private bool UserCanBorrowBook(string userId)
        {
            return GetUsersCurrentlyBorrowedBooks(userId).Count() < User.MAX_NUMBER_OF_BORROWED_BOOKS;
        }

        public User CreateUser(User user)
        {
            var existingUser = _userRepository.GetByUserName(user.Username).ToBo();
            if (existingUser.IsValid)
            {
                throw new Exception("User already exists");
            }

            var userDto = user.ToDto();
            var password = PasswordHelper.HashNewPassword(user.Password);
            userDto.Salt = password.Item1;
            userDto.Hash = password.Item2;
            return _userRepository.Add(userDto).ToBo();
        }

        public void DeleteUser(string userId)
        {
            var user = GetUser(userId);
            foreach (var borrowing in user.Borrowings)
            {
                _borrowingRepository.Delete(borrowing._id);
            }
            _userRepository.Delete(userId);
        }

        public void DeleteAllUsers()
        {
            _borrowingRepository.DeleteAll();
            _userRepository.DeleteAll();
        }

        public IEnumerable<User> Find(FindType findType, string username, string firstname, string surname, string address, string pin, string sortBy)
        {
            return
                _userRepository
                .Find(findType.ToDto(), username, firstname, surname, address, pin, sortBy)
                .Select(b => b.ToBo());
        }

        public User GetUser(string userId)
        {
            var user = _userRepository.Get(userId).ToBo();
            var borrowingsDto = _borrowingRepository.GetUsersCurrentBorrowings(user._id);
            user.Borrowings = borrowingsDto.Select(b => b.ToBo());
            return user;
        }

        public IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersCurrentBorrowings(userId)
                .Select(r => r.ToBo());

            return
                usersBorrowings.
                Select(r => _bookRepository.Get(r.BookId).ToBo());
        }

        public IEnumerable<Book> GetUsersBorrowedBooksHistory(string userId)
        {
            var usersBorrowings =
                _borrowingRepository
                .GetUsersBorrowingsHistory(userId)
                .Select(r => r.ToBo());

            return
                usersBorrowings.
                Select(r => _bookRepository.Get(r.BookId).ToBo());
        }

        public User LoginUser(string username, string password)
        {
            var userDto = _userRepository.GetByUserName(username);
            var user = userDto.ToBo();

            if (!user.IsValid)
            {
                return new User();
            }

            var passwordHash = PasswordHelper.HashPassword(userDto.Salt, password);

            if (!passwordHash.SequenceEqual(userDto.Hash))
            {
                return new User();
            }

            return user;
        }

        public void ReturnBook(string userId, string bookId)
        {
            var borrowing = _borrowingRepository.GetByUserAndBook(userId, bookId);
            borrowing.DateTimeReturned = DateTime.Now;
            _borrowingRepository.Update(borrowing);
        }

        public void SetPassword(string userId, string password)
        {
            var userDto = GetUser(userId).ToDto();
            var passwordHash = PasswordHelper.HashNewPassword(password);
            userDto.Salt = passwordHash.Item1;
            userDto.Hash = passwordHash.Item2;
            _userRepository.Update(userDto);
        }

        public User UpdateUser(User updatedBy, User userToUpdate)
        {
            if (!updatedBy.IsAdmin)
            {
                userToUpdate.AccountState = AccountState.AwatingApproval;
            }
            var updateEntity = userToUpdate.ToDto();
            _userRepository.Update(updateEntity);
            return updateEntity.ToBo();
        }
    }
}
