namespace BusinessLayer.BusinessObjects
{
    public class Borrowing : BaseBusinessObject
    {
        public string BookId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public DateTime DateTimeBorrowed { get; set; } = DateTime.MinValue;

        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;

        //public Book Book { get; set; } = new Book();

        //public User User { get; set; } = new User();
    }
}
