namespace DataLayer.DTO
{
    public class BorrowingHistoryDto : BorrowingDto
    {
        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;
    }
}
