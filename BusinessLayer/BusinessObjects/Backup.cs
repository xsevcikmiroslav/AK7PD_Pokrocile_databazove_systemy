using DataLayer.DTO;

namespace BusinessLayer.BusinessObjects
{
    public class Backup
    {
        public IEnumerable<BookDto> Books { get; set; }

        public IEnumerable<UserDto> Users { get; set; }

        public IEnumerable<BorrowingDto> Borrowings { get; set; }

        public IEnumerable<BorrowingHistoryDto> BorrowingsHistory { get; set; }
    }
}
