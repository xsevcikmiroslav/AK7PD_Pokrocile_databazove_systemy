using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICurrentBorrowingRepository _currentBorrowingRepository;
        private readonly IBorrowingHistoryRepository _borrowingHistoryRepository;
        private readonly IUserRepository _userRepository;

        public AdminManager(
            IBookRepository bookRepository,
            ICurrentBorrowingRepository currentBorrowingRepository,
            IBorrowingHistoryRepository borrowingHistoryRepository,
            IUserRepository UserRepository)
        {
            _bookRepository = bookRepository;
            _currentBorrowingRepository = currentBorrowingRepository;
            _borrowingHistoryRepository = borrowingHistoryRepository;
            _userRepository = UserRepository;
        }

        public User ApproveUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountState.Active;
            _userRepository.Update(userDto);
            return userDto.ToBo();
        }

        public Backup BackupDatabase()
        {
            return new Backup
            {
                Books = _bookRepository.GetAll(),
                Users = _userRepository.GetAll(),
                Borrowings = _currentBorrowingRepository.GetAll(),
                BorrowingsHistory = _borrowingHistoryRepository.GetAll()
            };
        }

        public User BanUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountState.Banned;
            _userRepository.Update(userDto);
            return userDto.ToBo();
        }


        public IEnumerable<User> Find(FindType findType, string username, string firstname = "", string surname = "", string address = "", string pin = "", string sortBy = "")
        {
            return
                _userRepository
                .Find(findType.ToDto(), username, firstname, surname, address, pin, sortBy)
                .Select(b => b.ToBo());
        }


        public void RestoreDatabase(Backup backup)
        {
            AddAllEntities(_bookRepository, backup.Books);
            AddAllEntities(_userRepository, backup.Users);
            AddAllEntities(_currentBorrowingRepository, backup.Borrowings);
            AddAllEntities(_borrowingHistoryRepository, backup.BorrowingsHistory);
        }

        private void AddAllEntities<T>(IRepository<T> repository, IEnumerable<T> entites)
            where T : BaseDto
        {
            foreach (var entity in entites)
            {
                repository.Add(entity);
            }
        }
    }
}
