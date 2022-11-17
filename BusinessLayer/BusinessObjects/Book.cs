namespace BusinessLayer.BusinessObjects
{
    public class Book : BaseBusinessObject
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int NumberOfPages { get; set; }

        public int YearOfPublication { get; set; }

        public int NumberOfLicences { get; set; }

        public int NumberOfBorrowed { get; set; }

        //public IEnumerable<Borrowing> Borrowings { get; set; } = Array.Empty<Borrowing>();

        public bool CanBeBorrowed
            => NumberOfLicences > NumberOfBorrowed;
    }
}
