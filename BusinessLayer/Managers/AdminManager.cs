using AutoMapper;
using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AdminManager(
            IMapper mapper,
            IBorrowingRepository BorrowingRepository,
            IUserRepository UserRepository)
        {
            _mapper = mapper;
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
    }
}
