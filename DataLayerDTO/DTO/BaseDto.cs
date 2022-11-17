namespace DataLayerDTO.DTO
{
    public abstract class BaseDto
    {
        public ObjectId _id { get; set; } = new object();

        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    }
}
