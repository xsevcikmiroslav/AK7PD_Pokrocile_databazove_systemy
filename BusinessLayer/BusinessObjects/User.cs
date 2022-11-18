namespace BusinessLayer.BusinessObjects
{
    public enum AccountState
    {
        AwatingApproval = 1,
        Active,
        Banned,
        Deleted
    }

    public class User : BaseBusinessObject
    {
        public const int MAX_NUMBER_OF_BORROWED_BOOKS = 6;

        public string Firstname { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Pin { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public byte[] Salt { get; set; } = Array.Empty<byte>();

        public byte[] Hash { get; set; } = Array.Empty<byte>();

        public AccountState AccountState { get; set; }

        public bool IsAdmin { get; set; }

        //public IEnumerable<Borrowing> Borrowings { get; set; } = Array.Empty<Borrowing>();
    }
}
