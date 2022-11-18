namespace DataLayer.DTO
{
    public enum AccountStateDb
    {
        AwatingApproval = 1,
        Active,
        Banned,
        Deleted
    }

    public class UserDto : BaseDto
    {
        public string Firstname { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Pin { get; set; } = string.Empty;

        public AddressDto Address { get; set; } = new AddressDto();

        public string Username { get; set; } = string.Empty;

        public byte[] Salt { get; set; } = Array.Empty<byte>();

        public byte[] Hash { get; set; } = Array.Empty<byte>();

        public int AccountState { get; set; }

        public bool IsAdmin { get; set; }

        //public ObjectId[] BorrowedBooks { get; set; } = Array.Empty<ObjectId>();
    }
}
