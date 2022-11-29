using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System;

namespace BusinessLayerTests
{
    [TestClass]
    public class AdminManagerTests : BaseTest
    {
        private IAdminManager _adminManager;
        private IUserManager _userManager;

        public AdminManagerTests()
        {
            _adminManager = _serviceProvider.GetService<IAdminManager>();
            _userManager = _serviceProvider.GetService<IUserManager>();
        }

        [TestMethod]
        public void UserManager_CreateThenApproveUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            _adminManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);
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
        public void UserManager_CreateThenApproveThenBanUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            _adminManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);

            _adminManager.BanUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Banned, newUser.AccountState);
        }

        [TestMethod]
        public void UserManager_CreateThenApproveThenDeleteUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            _adminManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);

            _userManager.DeleteUser(newUser._id);

            var user = _userManager.GetUser(newUser._id);

            Assert.AreEqual(ObjectId.Empty.ToString(), user._id.ToString());
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            _userManager.DeleteAllUsers();
        }
    }
}
