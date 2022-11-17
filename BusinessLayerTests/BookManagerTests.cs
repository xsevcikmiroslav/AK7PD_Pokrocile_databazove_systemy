using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers;
using BusinessLayer.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace BusinessLayerTests
{
    [TestClass]
    public class BookManagerTests : BaseTest
    {
        private IBookManager _bookManager;

        public BookManagerTests()
        {
            _bookManager = _serviceProvider.GetService<IBookManager>();
        }

        [TestMethod]
        public void BookManager_CreateBook_Success()
        {
            var newBook = CreateBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
        }

        private Book CreateBookEntity()
        {
            return new Book
            {
                Author = "Test Insert",
                Title = "Book Manager Test Insert",
                NumberOfBorrowed = 0,
                NumberOfLicences = 5,
                NumberOfPages = 350,
                YearOfPublication = 1988
            };
        }

        [TestMethod]
        public void BookManager_BorrowAndThenReturnBook_Success()
        {
            var newBook = CreateBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));

            _bookManager.BorrowBook(newBook._id, "876543218765432187654321");

            var book = _bookManager.GetBook(newBook._id);

            Assert.IsNotNull(book);
            Assert.AreEqual(1, book.NumberOfBorrowed);

            _bookManager.ReturnBook(newBook._id, "876543218765432187654321");

            book = _bookManager.GetBook(newBook._id);

            Assert.IsNotNull(book);
            Assert.AreEqual(0, book.NumberOfBorrowed);
        }

        [TestMethod]
        public void BookManager_CreateThenDeleteBook_Success()
        {
            var newBook = CreateBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));

            var bookId = newBook._id;

            _bookManager.DeleteBook(bookId);

            var book = _bookManager.GetBook(bookId);

            Assert.AreEqual(ObjectId.Empty.ToString(), book._id.ToString());
        }

        [TestMethod]
        public void BookManager_AndTypeFindForExistingBook_BookFound()
        {
            for (int i = 0; i < 5; i++)
            {
                var newBook = CreateBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            }

            var books = _bookManager.FindBooks(FindType.AND, "Insert 2", string.Empty, 1990, string.Empty);

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("Book Manager Test Insert 2", books.First().Title);
            Assert.AreEqual(1990, books.First().YearOfPublication);
        }

        [TestMethod]
        public void BookManager_AndTypeFindForNonExistingBook_BookNotFound()
        {
            for (int i = 0; i < 5; i++)
            {
                var newBook = CreateBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            }

            var books = _bookManager.FindBooks(FindType.AND, "NonExisting", string.Empty, 2100, string.Empty);

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
            Assert.IsFalse(books.Any());
        }

        [TestMethod]
        public void BookManager_OrTypeFindBooks_Success()
        {
            for (int i = 0; i < 5; i++)
            {
                var newBook = CreateBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            }

            var books = _bookManager.FindBooks(FindType.OR, "Insert 2", string.Empty, 1992, string.Empty);

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 2")));
            Assert.IsTrue(books.Any(b => b.YearOfPublication == 1992));
            Assert.AreEqual(1990, books.First().YearOfPublication);
        }

        [TestMethod]
        public void BookManager_GetBooksCurrentlyBorrowedByUserWhenUserHasSomeBorrowedBooks_BooksSuccessfullyRetrieved()
        {

        }

        public void BookManager_GetBooksCurrentlyBorrowedByUserWhenUserHasNoBorrowedBooks_NoBooksReturned()
        {

        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            _bookManager.DeleteAllBooks();
        }
    }
}
