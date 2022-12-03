using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingRepository : IRepository<BorrowingDto>
    {
        BorrowingDto GetByUserAndBook(string userId, string bookId);
    }
}
