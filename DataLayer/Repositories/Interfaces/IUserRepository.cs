using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserDto>
    {
        IEnumerable<UserDto> Find(DbFindType findType, string firstname, string surname, string address, string pin, string sortBy);
    }
}
