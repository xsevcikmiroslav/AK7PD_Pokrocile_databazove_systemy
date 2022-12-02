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
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IUserRepository _userRepository;

        public UserManager(
            IBookManager bookManager,
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _bookManager = bookManager;
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
            _userRepository = UserRepository;
        }

        public void BorrowBook(string userId, string bookId)
        {
            var user = GetUser(userId);
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

            _borrowingRepository.Add(borrowing.ToDto());
        }

        public User CreateUser(User createdByUser, User userToCreate)
        {
            var existingUser = _userRepository.GetByUserName(userToCreate.Username).ToBo();
            if (existingUser.IsValid)
            {
                throw new Exception("User already exists");
            }

            if (!createdByUser.IsAdmin)
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
            user.Borrowings = _borrowingRepository
                .GetUsersCurrentBorrowings(user._id)
                .Select(b => b.ToBo());
            return user;
        }

        public IEnumerable<Book> GetUsersCurrentlyBorrowedBooks(string userId)
        {
            return
                _borrowingRepository
                .GetUsersCurrentBorrowings(userId)
                .Select(b => _bookRepository.Get(b.ToBo().BookId).ToBo());
        }

        public IEnumerable<Book> GetUsersBorrowedBooksHistory(string userId)
        {
            return
                _borrowingRepository
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
            _userRepository.Update(userToUpdate.ToDto());
            return userToUpdate;
        }
    }
}
