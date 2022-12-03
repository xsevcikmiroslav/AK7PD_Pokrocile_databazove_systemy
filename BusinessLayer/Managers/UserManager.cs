using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using BusinessLayer.Password;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IBookManager _bookManager;
        private readonly IBookRepository _bookRepository;
        private readonly ICurrentBorrowingRepository _currentBorrowingRepository;
        private readonly IBorrowingHistoryRepository _borrowingHistoryRepository;
        private readonly IUserRepository _userRepository;

        public UserManager(
            IBookManager bookManager,
            IBookRepository bookRepository,
            ICurrentBorrowingRepository currentBorrowingRepository,
            IBorrowingHistoryRepository borrowingHistoryRepository,
            IUserRepository UserRepository)
        {
            _bookManager = bookManager;
            _bookRepository = bookRepository;
            _currentBorrowingRepository = currentBorrowingRepository;
            _borrowingHistoryRepository = borrowingHistoryRepository;
            _userRepository = UserRepository;
        }

        public void BorrowBook(string userId, string bookId)
        {
            var user = GetUser(userId);
            if (user.AccountState != AccountState.Active)
            {
                throw new Exception("User account is awaiting approval");
            }

            if (!user.CanBorrowAnotherBook)
            {
                throw new Exception("User cannot borrow another book");
            }

            if (user.Borrowings.Any(b => b.BookId.ToString().Equals(bookId)))
            {
                throw new Exception("User has this book already borrowed");
            }

            var book = _bookManager.GetBook(bookId);
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

            _currentBorrowingRepository.Add(borrowing.ToDto());

            borrowing.DateTimeReturned = DateTime.Now.AddDays(6);
            _borrowingHistoryRepository.Add(borrowing.ToDto());
        }

        public User CreateUser(bool createdByAdmin, User userToCreate)
        {
            var existingUser = _userRepository.GetByUserName(userToCreate.Username).ToBo();
            if (existingUser.IsValid)
            {
                throw new Exception("User already exists");
            }

            if (!createdByAdmin)
            {
                userToCreate.AccountState = AccountState.AwatingApproval;
            }

            var userDto = userToCreate.ToDto();
            var password = PasswordHelper.HashNewPassword(userToCreate.Password);
            userDto.Salt = password.Item1;
            userDto.Hash = password.Item2;
            return _userRepository.Add(userDto).ToBo();
        }

        public void DeleteUser(string userId)
        {
            var user = GetUser(userId);
            foreach (var borrowing in user.Borrowings)
            {
                _currentBorrowingRepository.Delete(borrowing._id);
                _borrowingHistoryRepository.Delete(borrowing._id);
            }
            _userRepository.Delete(userId);
        }

        public void DeleteAllUsers()
        {
            _currentBorrowingRepository.DeleteAll();
            _borrowingHistoryRepository.DeleteAll();
            _userRepository.DeleteAll();
        }

        public User GetUser(string userId)
        {
            var user = _userRepository.Get(userId).ToBo();
            user.Borrowings = _currentBorrowingRepository
                .GetUsersCurrentBorrowings(user._id)
                .Select(b => b.ToBo());
            return user;
        }

        public IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string userId)
        {
            return
                _currentBorrowingRepository
                .GetUsersCurrentBorrowings(userId)
                .Select(b => _bookRepository.Get(b.ToBo().BookId).ToBo());
        }

        public IEnumerable<Book> GetUsersBorrowedBooksHistory(string userId)
        {
            return
                _borrowingHistoryRepository
                .GetUsersBorrowingsHistory(userId)
                .Select(b => _bookRepository.Get(b.ToBo().BookId).ToBo());
        }

        public User LoginUser(string username, string password)
        {
            var userDto = _userRepository.GetByUserName(username);
            var user = userDto.ToBo();

            if (!user.IsValid)
            {
                throw new Exception("User does not exist");
            }

            var passwordHash = PasswordHelper.HashPassword(userDto.Salt, password);

            if (!passwordHash.SequenceEqual(userDto.Hash))
            {
                throw new Exception("Incorrect username or password");
            }

            return user;
        }

        public void ReturnBook(string userId, string bookId)
        {
            var currentBorrowing = _currentBorrowingRepository.GetByUserAndBook(userId, bookId);
            _currentBorrowingRepository.Delete(currentBorrowing._id.ToString());
            
            var borrowingHistory = _borrowingHistoryRepository.GetByUserAndBook(userId, bookId);
            borrowingHistory.DateTimeReturned = DateTime.Now;
            _borrowingHistoryRepository.Update(borrowingHistory);
        }

        public void SetPassword(string userId, string password)
        {
            var userDto = GetUser(userId).ToDto();
            var passwordHash = PasswordHelper.HashNewPassword(password);
            userDto.Salt = passwordHash.Item1;
            userDto.Hash = passwordHash.Item2;
            _userRepository.Update(userDto);
        }

        public User UpdateUser(bool updatedByAdmin, User userToUpdate)
        {
            if (!updatedByAdmin)
            {
                var user = GetUser(userToUpdate._id);
                if (user.AccountState != AccountState.Active)
                {
                    throw new Exception("User cannot update account when it is not Approved by admin");
                }
                userToUpdate.AccountState = AccountState.AwatingApproval;
            }
            _userRepository.Update(userToUpdate.ToDto());
            return userToUpdate;
        }
    }
}
