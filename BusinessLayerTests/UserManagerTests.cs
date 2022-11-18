using BusinessLayer.BusinessObjects;
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
        public void BookManager_CreateAndConfirmAccount_Success()
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
                Password = "Heslo123",
                Pin = "0101010008",
                Surname = "Sevcik",
                Username = "MirSev"
            };
        }
    }
}
