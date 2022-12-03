using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingHistoryRepository : IBorrowingRepository<BorrowingHistoryDto>
    {
        IEnumerable<BorrowingHistoryDto> GetUsersBorrowingsHistory(string userId);

        IEnumerable<BorrowingHistoryDto> GetBookBorrowingsHistory(string bookId);
    }
}
