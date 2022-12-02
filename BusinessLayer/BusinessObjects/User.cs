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

        public AccountState AccountState { get; set; }

        public bool IsAdmin { get; set; }

        public IEnumerable<Borrowing> Borrowings { get; set; } = Array.Empty<Borrowing>();

        public bool CanBorrowAnotherBook => MAX_NUMBER_OF_BORROWED_BOOKS > Borrowings.Count();

        public override bool IsValid =>
            !string.IsNullOrEmpty(Firstname)
            && !string.IsNullOrEmpty(Surname)
            && !string.IsNullOrEmpty(Pin)
            && Pin.All(c => char.IsDigit(c))
            && !string.IsNullOrEmpty(Username)
            && Address.IsValid;
    }
}
