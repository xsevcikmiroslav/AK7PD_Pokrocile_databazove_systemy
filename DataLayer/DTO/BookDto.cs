namespace DataLayer.DTO
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int NumberOfPages { get; set; }
        
        public int YearOfPublication { get; set; }
        
        public int NumberOfLicences { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}; Author: {Author}; NumberOfPages: {NumberOfPages}; YearOfPublication: {YearOfPublication}; NumberOfLicences: {NumberOfLicences}";
        }
    }
}
