﻿using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IBookManager
    {
        public void BorrowBook(string bookId, string userId);

        Book CreateBook(Book book);

        void DeleteBook(string bookId);

        void DeleteAllBooks();

        IEnumerable<Book> FindBooks(FindType findType, string title, string author, int yearOfPublication, string sortBy);

        Book GetBook(string bookId);

        IEnumerable<Book> GetBooksCurrentlyBorrowedByUser(string UserId);

        public void ReturnBook(string bookId, string userId);

        Book UpdateBook(Book book);
    }
}
