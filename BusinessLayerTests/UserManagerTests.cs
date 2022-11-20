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

        public UserManagerTests()
        {
            _userManager = _serviceProvider.GetService<IUserManager>();
        }

        [TestMethod]
        public void UserManager_CreateUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
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
        public void UserManager_CreateThenApproveUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            _userManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);
        }

        [TestMethod]
        public void UserManager_CreateThenApproveThenBanUser_Success()
        {
            var newUser = GetUserEntity();
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            _userManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);

            _userManager.BanUser(newUser._id);

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

            _userManager.ApproveUser(newUser._id);

            newUser = _userManager.GetUser(newUser._id);

            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);
            Assert.AreEqual(AccountState.Active, newUser.AccountState);

            _userManager.DeleteUser(newUser._id);

            var user = _userManager.GetUser(newUser._id);

            Assert.AreEqual(ObjectId.Empty.ToString(), user._id.ToString());
        }

        [TestMethod]
        public void UserManager_AndTypeFindForExistingUser_UserFound()
        {
            for (int i = 1; i <= 5; i++)
            {
                var newUser = GetUserEntity();
                newUser.Username = $"{newUser.Username}{i}";
                newUser.Address.DescriptiveNumber += $"{i}";
                newUser = _userManager.CreateUser(newUser);
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
                newUser = _userManager.CreateUser(newUser);
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
                newUser = _userManager.CreateUser(newUser);
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
            newUser = _userManager.CreateUser(newUser);
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
            newUser = _userManager.CreateUser(newUser);
            Assert.IsFalse(string.IsNullOrEmpty(newUser._id));
            Assert.AreNotEqual(ObjectId.Empty.ToString(), newUser._id);
            Assert.AreEqual("MirSev", newUser.Username);

            var loggedUser = _userManager.LoginUser("MirSev", "Hacker01");

            Assert.IsTrue(string.IsNullOrEmpty(loggedUser._id));
            Assert.AreEqual(string.Empty, loggedUser.Username);
            Assert.IsFalse(loggedUser.IsValid);
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            _userManager.DeleteAllUsers();
        }
    }
}
