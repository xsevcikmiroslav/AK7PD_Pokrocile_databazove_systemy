using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingRepository : IRepository<BorrowingDto>
    {
        BorrowingDto GetByUserAndBook(string userId, string bookId);

        IEnumerable<BorrowingDto> GetUsersBorrowingsHistory(string userId);

        IEnumerable<BorrowingDto> GetUsersCurrentBorrowings(string userId);

        IEnumerable<BorrowingDto> GetBookBorrowingsHistory(string bookId);

        IEnumerable<BorrowingDto> GetBookCurrentBorrowings(string bookId);
    }
}
