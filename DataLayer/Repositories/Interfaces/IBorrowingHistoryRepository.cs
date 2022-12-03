using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingHistoryRepository : IBorrowingRepository
    {
        IEnumerable<BorrowingDto> GetUsersBorrowingsHistory(string userId);

        IEnumerable<BorrowingDto> GetBookBorrowingsHistory(string bookId);
    }
}
