using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserDto>
    {
        IEnumerable<UserDto> Find(FindTypeDb findType, string username, string firstname, string surname, string address, string pin, string sortBy);

        UserDto GetByUserName(string username);
    }
}
