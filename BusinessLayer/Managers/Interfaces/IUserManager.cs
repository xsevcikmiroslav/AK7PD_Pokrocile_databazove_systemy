﻿using BusinessLayer.BusinessObjects;

namespace BusinessLayer.Managers.Interfaces
{
    public interface IUserManager
    {
        void BorrowBook(string userId, string bookId);

        User CreateUser(bool createdByAdmin, User userToCreate);

        void DeleteAllUsers();

        void DeleteUser(string userId);

        User GetUser(string userId);

        IEnumerable<BorrowingHistory> GetUsersBorrowingsHistory(string UserId);

        IEnumerable<Borrowing> GetUsersCurrentBorrowings(string UserId);

        User LoginUser(string username, string password);

        void ReturnBook(string userId, string bookId);

        void SetPassword(string userId, string password);

        User UpdateUser(bool updatedByAdmin, User userToUpdate);
    }
}
