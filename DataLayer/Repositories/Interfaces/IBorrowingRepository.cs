using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingRepository : IRepository<BorrowingDto>
    {
        BorrowingDto GetByUserAndBook(string userId, string bookId);

        IEnumerable<BorrowingDto> GetAllBorrowingsByUser(string userId);

        IEnumerable<BorrowingDto> GetCurrentBorrowingsByUser(string userId);
    }
}
