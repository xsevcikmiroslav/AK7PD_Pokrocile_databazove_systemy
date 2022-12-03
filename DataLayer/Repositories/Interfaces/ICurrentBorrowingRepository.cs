using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface ICurrentBorrowingRepository : IBorrowingRepository<BorrowingDto>
    {
        IEnumerable<BorrowingDto> GetUsersCurrentBorrowings(string userId);

        IEnumerable<BorrowingDto> GetBookCurrentBorrowings(string bookId);
    }
}
