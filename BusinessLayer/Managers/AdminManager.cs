using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IUserRepository _userRepository;

        public AdminManager(
            IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        public User ApproveUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountState.Active;
            _userRepository.Update(userDto);
            return userDto.ToBo();
        }

        public User BanUser(string userId)
        {
            var userDto = _userRepository.Get(userId);
            userDto.AccountState = (int)AccountState.Banned;
            _userRepository.Update(userDto);
            return userDto.ToBo();
        }
    }
}
