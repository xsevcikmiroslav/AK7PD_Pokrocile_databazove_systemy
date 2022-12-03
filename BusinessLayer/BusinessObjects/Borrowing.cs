using System.Text.Json.Serialization;

namespace BusinessLayer.BusinessObjects
{
    public class Borrowing : BaseBusinessObject
    {
        public string BookId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public DateTime DateTimeBorrowed { get; set; } = DateTime.MinValue;

        public DateTime DateTimeReturned { get; set; } = DateTime.MinValue;

        [JsonIgnore]
        public override bool IsValid =>
            !string.IsNullOrEmpty(BookId)
            && !string.IsNullOrEmpty(UserId);
    }
}
