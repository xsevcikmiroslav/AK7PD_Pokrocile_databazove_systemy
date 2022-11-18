using MongoDB.Bson;

namespace DataLayer.DTO
{
    public class BorrowingDto : BaseDto
    {
        public ObjectId BookId { get; set; } = ObjectId.Empty;

        public ObjectId UserId { get; set; } = ObjectId.Empty;
        
        public DateTime DateTimeBorrowed { get; set; } = DateTime.MinValue;

        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;
    }
}
