using AutoMapper;
using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
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

        public User ApproveUser(User UserId)
        {
            throw new NotImplementedException();
        }

        public User BanUser(User UserId)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User User)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User UserId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(FindType findType, string firstname, string surname, string address, string pin, string sortBy)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User UserId)
        {
            throw new NotImplementedException();
        }
    }
}
