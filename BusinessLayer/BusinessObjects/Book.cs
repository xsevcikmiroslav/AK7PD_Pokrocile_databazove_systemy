using System.Text.Json.Serialization;

namespace BusinessLayer.BusinessObjects
{
    public class Book : BaseBusinessObject
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int NumberOfPages { get; set; }

        public int YearOfPublication { get; set; }

        public int NumberOfLicences { get; set; }

        public string ImageBase64 { get; set; } = string.Empty;

        public IEnumerable<Borrowing> Borrowings { get; set; } = Array.Empty<Borrowing>();

        public bool CanBeBorrowed
            => NumberOfLicences > Borrowings.Count();

        [JsonIgnore]
        public override bool IsValid =>
            !string.IsNullOrEmpty(Title)
            && !string.IsNullOrEmpty(Author)
            && NumberOfPages > 0
            && YearOfPublication > 0
            && NumberOfLicences > 0;
    }
}
