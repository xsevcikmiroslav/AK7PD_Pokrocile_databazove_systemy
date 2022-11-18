using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<BookDto>
    {
        IEnumerable<BookDto> Find(FindTypeDb findType, string title, string author, int yearOfPublication, string sortBy);
    }
}
