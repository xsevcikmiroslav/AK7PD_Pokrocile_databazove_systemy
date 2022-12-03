using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingRepository<T> : IRepository<T>
        where T : BorrowingDto
    {
        T GetByUserAndBook(string userId, string bookId);
    }
}
