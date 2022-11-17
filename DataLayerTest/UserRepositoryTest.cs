using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using System.Text;

namespace DataLayerTest
{
    [TestClass]
    public class UserRepositoryTest : BaseRepositoryTest<UserDto>
    {
        protected override IRepository<UserDto> getRepository()
        {
            return new UserRepository(CONNECTION_STRING);
        }

        protected override UserDto GetEntity()
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
                AccountState = (int)AccountState.AwatingApproval,
                IsAdmin = false
            };
        }

        [TestMethod]
        public void UserRepository_FindByStreet_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            var result = ((IUserRepository)repository).Find(DbFindType.AND, string.Empty, string.Empty, "Krizkovskeho", string.Empty, null);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void UserRepository_FindUsingAnd_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            var result = ((IUserRepository)repository).Find(DbFindType.AND, "first", "Surname", "Blan", "0101010008", null);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void UserRepository_FindUsingOr_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            var result = ((IUserRepository)repository).Find(DbFindType.OR, "first", "Surname", "Blan", "0101010008", null);

            Assert.IsTrue(result.Any());
        }

        protected override void updateEntity(UserDto entity)
        {
            entity.AccountState = (int)AccountState.Banned;
            entity.Firstname = "Updatefirstname";
            entity.Address = new AddressDto
            {
                Street = "Uzka",
                DescriptiveNumber = "488",
                OrientationNumber = "8",
                City = "Brno",
                Zip = "60200"
            };
        }

        protected override void assertAfterUpdate(UserDto entity)
        {
            Assert.AreEqual((int)AccountState.Banned, entity.AccountState);
            Assert.AreEqual("Updatefirstname", entity.Firstname);
            Assert.AreEqual("Uzka", entity.Address.Street);
            Assert.AreEqual("488", entity.Address.DescriptiveNumber);
            Assert.AreEqual("8", entity.Address.OrientationNumber);
            Assert.AreEqual("Brno", entity.Address.City);
            Assert.AreEqual("60200", entity.Address.Zip);
        }
    }
}
