using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IUserRepository _userRepository;

        public AdminManager(
            IBookRepository bookRepository,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _bookRepository = bookRepository;
            _borrowingRepository = BorrowingRepository;
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
                Borrowings = _borrowingRepository.GetAll()
            };
        }

        public User BanUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountState.Banned;
            _userRepository.Update(userDto);
            return userDto.ToBo();
        }

        public void RestoreDatabase(Backup backup)
        {
            AddAllEntities(_bookRepository, backup.Books);
            AddAllEntities(_userRepository, backup.Users);
            AddAllEntities(_borrowingRepository, backup.Borrowings);
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
