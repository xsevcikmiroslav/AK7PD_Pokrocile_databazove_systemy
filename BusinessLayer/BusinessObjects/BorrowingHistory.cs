namespace BusinessLayer.BusinessObjects
{
    public class BorrowingHistory : Borrowing
    {
        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;
    }
}
