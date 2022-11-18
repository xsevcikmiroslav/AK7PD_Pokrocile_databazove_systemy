﻿using BusinessLayer.BusinessObjects;
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
            var newBook = GetBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);
            Assert.AreEqual("Book Manager Test Insert", newBook.Title);
        }

        private Book GetBookEntity()
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
            var newBook = GetBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

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
            var newBook = GetBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

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
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);
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
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);
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
                var newBook = GetBookEntity();
                newBook.Title = $"{newBook.Title} {i}";
                newBook.YearOfPublication = newBook.YearOfPublication + i;
                newBook = _bookManager.CreateBook(newBook);
                Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
                Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);
            }

            var books = _bookManager.FindBooks(FindType.OR, "Insert 2", string.Empty, 1992, string.Empty);

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 2")));
            Assert.IsTrue(books.Any(b => b.YearOfPublication == 1992));
            Assert.AreEqual(1990, books.First().YearOfPublication);
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
                    _bookManager.BorrowBook(newBook._id, userId);
                }
            }

            var books = _bookManager.GetUsersCurrentlyBorrowedBooks(userId);

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
                    _bookManager.BorrowBook(newBook._id, userId);
                    _bookManager.ReturnBook(newBook._id, userId);
                }
            }

            var books = _bookManager.GetUsersCurrentlyBorrowedBooks(userId);

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
                    _bookManager.BorrowBook(newBook._id, userId);
                }
            }

            var books = _bookManager.GetUsersBorrowedBooksHistory(userId);

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
                    _bookManager.BorrowBook(newBook._id, userId);
                    _bookManager.ReturnBook(newBook._id, userId);
                }
            }

            var books = _bookManager.GetUsersBorrowedBooksHistory(userId);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 5")));
            Assert.IsTrue(books.Any(b => b.Title.Equals("Book Manager Test Insert 10")));
        }

        [TestMethod]
        public void BookManager_CreateBookThenUpdateBook_SuccessfullyUpdated()
        {
            var newBook = GetBookEntity();
            newBook = _bookManager.CreateBook(newBook);
            Assert.IsFalse(string.IsNullOrEmpty(newBook._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newBook._id);

            newBook.Title = "Book Manager Test Update";
            newBook.NumberOfLicences = 10;

            var updatedBook = _bookManager.UpdateBook(newBook);

            Assert.AreEqual("Book Manager Test Update", updatedBook.Title);
            Assert.AreEqual(10, updatedBook.NumberOfLicences);
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            _bookManager.DeleteAllBooks();
        }
    }
}
