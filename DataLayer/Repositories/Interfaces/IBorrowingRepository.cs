using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IBorrowingRepository : IRepository<BorrowingDto>
    {
        BorrowingDto GetByUserAndBook(string userId, string bookId);

        IEnumerable<BorrowingDto> GetUsersAllBorrowings(string userId);

        IEnumerable<BorrowingDto> GetUsersBorrowingsHistory(string userId);
    }
}
