using MongoDB.Bson;

namespace DataLayer.DTO
{
    public abstract class BaseDto
    {
        public ObjectId _id { get; set; } = ObjectId.Empty;

        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    }
}
