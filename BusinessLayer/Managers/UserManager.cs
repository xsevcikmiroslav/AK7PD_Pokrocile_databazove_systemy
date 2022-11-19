using AutoMapper;
using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.DTO;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IUserRepository _userRepository;

        public UserManager(
            IMapper mapper,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _mapper = mapper;
            _borrowingRepository = BorrowingRepository;
            _userRepository = UserRepository;
        }

        public User ApproveUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountStateDb.Active;
            _userRepository.Update(userDto);
            return _mapper.Map<User>(userDto);
        }

        public User BanUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountStateDb.Banned;
            _userRepository.Update(userDto);
            return _mapper.Map<User>(userDto);
        }

        public User CreateUser(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            _userRepository.Add(userDto);
            return _mapper.Map<User>(userDto);
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
            var dbFindType = _mapper.Map<FindTypeDb>(findType);

            return
                _userRepository
                .Find(dbFindType, username, firstname, surname, address, pin, sortBy)
                .Select(b => _mapper.Map<User>(b));
        }

        public User GetUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            var user = _mapper.Map<User>(userDto);
            var borrowingsDto = _borrowingRepository.GetUsersCurrentBorrowings(user._id);
            user.Borrowings = borrowingsDto.Select(b => _mapper.Map<Borrowing>(b));
            return user;
        }

        public bool LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void SetPassword(string userId, string password)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
