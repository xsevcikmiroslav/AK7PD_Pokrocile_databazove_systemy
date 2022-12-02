using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers;
using BusinessLayer.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace BusinessLayerTests
{
    [TestClass]
    public class UserManagerTests : BaseTest
    {
        private IUserManager _userManager;
        private IBookManager _bookManager;

        public UserManagerTests()
        {
            _userManager = _serviceProvider.GetService<IUserManager>();
            _bookManager = _serviceProvider.GetService<IBookManager>();
        }

        [TestMethod]
        public void UserManager_CreateUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(new User(), newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
        }

        private User GetUserEntity()
        {
            return new User
            {
                AccountState = AccountState.AwatingApproval,
                Address = new Address
                {
                    City = "Blansko",
                    DescriptiveNumber = "1124",
                    OrientationNumber = "29",
                    Street = "Krizkovskeho",
                    Zip = "67801"
                },
                Firstname = "Miroslav",
                Pin = "0101010008",
                Surname = "Sevcik",
                Username = "MirSev",
                Password = "Pa55w0RdO12EAS"
            };
        }

        [TestMethod]
        public void BookManager_BorrowAndThenReturnBook_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(new User(), newUser);

            var newBook = GetBookEntity();
            newBook.NumberOfLicences = 1;
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);
            Assert.IsTrue(newBook.CanBeBorrowed);

            _userManager.BorrowBook(newUser._id, newBook._id);

            var book = _bookManager.GetBook(newBook._id);

            Assert.IsNotNull(book);
            Assert.IsFalse(book.CanBeBorrowed);

            _userManager.ReturnBook(newUser._id, newBook._id);

            book = _bookManager.GetBook(newBook._id);

            Assert.IsNotNull(book);
            Assert.IsTrue(newBook.CanBeBorrowed);
        }

        private Book GetBookEntity()
        {
            return new Book
            {
                Author = "Test Insert",
                Title = "Book Manager Test Insert",
                NumberOfLicences = 5,
                NumberOfPages = 350,
                YearOfPublication = 1988
            };
        }

        [TestMethod]
        public void UserManager_AndTypeFindForExistingUser_UserFound()
        {
            for (int i = 1; i <= 5; i++)
            {
                var newUser = GetUserEntity();
                newUser.Username = $"{newUser.Username}{i}";
                newUser.Address.DescriptiveNumber += $"{i}";
                newUser = _userManager.CreateUser(new User(), newUser);
                Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            }

            var users = _userManager.Find(FindType.AND, "MirSev2", string.Empty, string.Empty, string.Empty, "0101010008", string.Empty);

            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count());
            Assert.AreEqual("MirSev2", users.First().Username);
            Assert.AreEqual("11242", users.First().Address.DescriptiveNumber);
        }

        [TestMethod]
        public void UserManager_AndTypeFindForNonExistingUser_UserNotFound()
        {
            for (int i = 1; i <= 5; i++)
            {
                var newUser = GetUserEntity();
                newUser.Username = $"{newUser.Username}{i}";
                newUser.Address.DescriptiveNumber += $"{i}";
                newUser = _userManager.CreateUser(new User(), newUser);
                Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            }

            var users = _userManager.Find(FindType.AND, "MirSev10", string.Empty, string.Empty, string.Empty, "0101010008", string.Empty);

            Assert.IsNotNull(users);
            Assert.AreEqual(0, users.Count());
            Assert.IsFalse(users.Any());
        }

        [TestMethod]
        public void UserManager_OrTypeFindUsers_Success()
        {
            for (int i = 1; i <= 5; i++)
            {
                var newUser = GetUserEntity();
                newUser.Username = $"{newUser.Username}{i}";
                newUser.Address.DescriptiveNumber += $"{i}";
                newUser = _userManager.CreateUser(new User(), newUser);
                Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            }

            var users = _userManager.Find(FindType.OR, "MirSev1", string.Empty, string.Empty, "11242", string.Empty, string.Empty);

            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count());
            Assert.IsTrue(users.Any(b => b.Username.Equals("MirSev1")));
            Assert.IsTrue(users.Any(b => b.Address.DescriptiveNumber.Equals("11242")));
        }

        [TestMethod]
        public void UserManager_CreateUserAndTryLoginWithValidCredentials_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(new User(), newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            var loggedUser = _userManager.LoginUser("MirSev", "Pa55w0RdO12EAS");

            Assert.IsFalse(string.IsNullOrEmpty(loggedUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), loggedUser._id);
            Assert.AreEqual("MirSev", loggedUser.Username);
            Assert.IsTrue(loggedUser.IsValid);
        }

        [TestMethod]
        public void UserManager_CreateUserAndTryLoginWithInvalidCredentials_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(new User(), newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            Assert.ThrowsException<Exception>(() => _userManager.LoginUser("MirSev", "Hacker01"));
        }


        [TestMethod]
        public void BookManager_GetUsersCurrentlyBorrowedBooksWhenUserHasSomeBorrowedBooks_BooksSuccessfullyRetrieved()
        {
            var userId = "876543218765432187654321";

            for (int i = 1; i <= 10; i++)
            {
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

                if (i % 5 == 0)
                {
                    _userManager.BorrowBook(userId, newBook._id);
                }
            }

            var books = _userManager.GetUsersCurrentlyBorrowedBooks(userId);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 5")));
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 10")));
        }

        [TestMethod]
        public void BookManager_GetUsersCurrentlyBorrowedBooksWhenUserHasNoBorrowedBooks_NoBooksReturned()
        {
            var userId = "876543218765432187654321";

            for (int i = 1; i <= 10; i++)
            {
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

                if (i % 5 == 0)
                {
                    _userManager.BorrowBook(userId, newBook._id);
                    _userManager.ReturnBook(userId, newBook._id);
                }
            }

            var books = _userManager.GetUsersCurrentlyBorrowedBooks(userId);

            Assert.IsNotNull(books);
            Assert.IsFalse(books.Any());
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public void BookManager_GetUsersBorrowedBooksHistoryWhenUserHasSomeBorrowedBooks_BooksSuccessfullyRetrieved()
        {
            var userId = "876543218765432187654321";

            for (int i = 1; i <= 10; i++)
            {
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

                if (i % 5 == 0)
                {
                    _userManager.BorrowBook(userId, newBook._id);
                }
            }

            var books = _userManager.GetUsersBorrowedBooksHistory(userId);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 5")));
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 10")));
        }

        [TestMethod]
        public void BookManager_GetUsersBorrowedBooksHistoryWhenUserHasNoBorrowedBooks_BooksSuccessfullyRetrieved()
        {
            var userId = "876543218765432187654321";

            for (int i = 1; i <= 10; i++)
            {
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

                if (i % 5 == 0)
                {
                    _userManager.BorrowBook(userId, newBook._id);
                    _userManager.ReturnBook(userId, newBook._id);
                }
            }

            var books = _userManager.GetUsersBorrowedBooksHistory(userId);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 5")));
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 10")));
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            _userManager.DeleteAllUsers();
        }
    }
}
