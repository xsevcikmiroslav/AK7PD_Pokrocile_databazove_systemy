using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface ICurrentBorrowingRepository : IBorrowingRepository
    {
        IEnumerable<BorrowingDto> GetUsersCurrentBorrowings(string userId);

        IEnumerable<BorrowingDto> GetBookCurrentBorrowings(string bookId);
    }
}
