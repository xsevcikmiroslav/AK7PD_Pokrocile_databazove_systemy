using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using MongoDB.Bson;

namespace DataLayerTest
{
    [TestClass]
    public abstract class BaseRepositoryTest<T>
        where T : BaseDto
    {
        protected const string CONNECTION_STRING = "mongodb://localhost:27017";

        protected IRepository<T> repository;

        [TestInitialize]
        public void Init()
        {
            repository = getRepository();
        }

        protected abstract IRepository<T> getRepository();

        protected abstract T GetEntity();

        [TestMethod]
        public void Repository_Add_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            Assert.AreNotEqual(ObjectId.Empty.ToString(), entity._id.ToString());
        }

        [TestMethod]
        public void Repository_Get_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            var returnedEntity = repository.Get(entity._id.ToString());

            foreach (var prop in returnedEntity.GetType().GetProperties().Where(p => !new string[] { "_id", "Salt", "Hash", "Address", "DateTimeCreated" }.Contains(p.Name)))
            {
                Assert.AreEqual(prop.GetValue(entity, null), prop.GetValue(returnedEntity, null));
            }
        }

        [TestMethod]
        public void Repository_Update_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);

            updateEntity(entity);

            repository.Update(entity);

            assertAfterUpdate(entity);
        }

        protected abstract void updateEntity(T entity);

        protected abstract void assertAfterUpdate(T entity);

        [TestMethod]
        public void Repository_Delete_Success()
        {
            var entity = GetEntity();
            repository.Add(entity);
            repository.Delete(entity._id.ToString());
            var getResult = repository.Get(entity._id.ToString());
            Assert.AreEqual(ObjectId.Empty.ToString(), getResult._id.ToString());
        }

        [TestCleanup]
        public void DeleteAllEntries()
        {
            repository.DeleteAll();
        }
    }
}