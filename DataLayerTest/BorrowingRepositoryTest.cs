using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using SharpCompress.Common;
using System.Text;
using MongoDB.Bson;

namespace DataLayerTest
{
    [TestClass]
    public class BorrowingRepositoryTest
    {
        protected const string CONNECTION_STRING = "mongodb://localhost:27017";

        protected IBorrowingRepository borrowingRepository;
        protected IBookRepository bookRepository;
        protected IUserRepository userRepository;

        [TestInitialize]
        public void Init()
        {
            borrowingRepository = new BorrowingRepository(CONNECTION_STRING);
            bookRepository = new BookRepository(CONNECTION_STRING);
            userRepository = new UserRepository(CONNECTION_STRING);
        }

        [TestMethod]
        public void Repository_Add_Success()
        {
            var user = CreateUserEntity();
            userRepository.Add(user);
            var book = CreateBookEntity();
            bookRepository.Add(book);

            var borrowing = CreateBorrowingEntity(user._id, book._id);
            borrowingRepository.Add(borrowing);

            Assert.AreNotEqual(ObjectId.Empty.ToString(), borrowing._id.ToString());

            var borrowingId = borrowing._id.ToString();
            borrowing = borrowingRepository.GetByUserAndBook(user._id.ToString(), book._id.ToString());

            Assert.AreEqual(borrowingId, borrowing._id.ToString());
        }

        private UserDto CreateUserEntity()
        {
            return new UserDto
            {
                Firstname = "Insertfirstname",
                Surname = "InsertSurname",
                Pin = "0101010008",
                Address = new AddressDto
                {
                    Street = "Krizkovskeho",
                    DescriptiveNumber = "1124",
                    OrientationNumber = "29",
                    City = "Blansko",
                    Zip = "67801"
                },
                Username = "Insertusername",
                Salt = Encoding.UTF8.GetBytes("XIn+Dt7BHfDZtaeZF1cUY8A8yeaBPpQjpBEI0xBX5GEu+Y/wiDa9QhZdo61apD2Jujp72IXLQ3nnlBNN03GOZg=="),
                Hash = Encoding.UTF8.GetBytes("XIn+Dt7BHfDZtaeZF1cUY8A8yeaBPpQjpBEI0xBX5GEu+Y/wiDa9QhZdo61apD2Jujp72IXLQ3nnlBNN03GOZg=="),
                AccountState = (int)AccountStateDb.AwatingApproval,
                IsAdmin = false
            };
        }

        private BookDto CreateBookEntity()
        {
            return new BookDto
            {
                Author = "Test Author Insert",
                NumberOfLicences = 5,
                NumberOfPages = 200,
                Title = "Test Title Insert",
                YearOfPublication = 2022
            };
        }

        private BorrowingDto CreateBorrowingEntity(ObjectId userId, ObjectId bookId)
        {
            return new BorrowingDto
            {
                BookId = bookId,
                UserId = userId,
                DateTimeBorrowed = DateTime.Now,
            };
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            borrowingRepository.DeleteAll();
            bookRepository.DeleteAll();
            userRepository.DeleteAll();
        }
    }
}
