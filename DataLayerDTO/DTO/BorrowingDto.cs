namespace DataLayerDTO.DTO
{
    public class BorrowingDto : BaseDto
    {
        public object BookId { get; set; } = new object();

        public object UserId { get; set; } = new object();
        
        public DateTime DateTimeBorrowed { get; set; } = DateTime.MinValue;

        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;
    }
}
