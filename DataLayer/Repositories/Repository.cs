using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public enum FindTypeDb
    {
        AND,
        OR
    }

    public abstract class Repository<T> : IRepository<T>
        where T : BaseDto, new()
    {
        protected IMongoCollection<BsonDocument> _mongoCollection;

        // ATLAS connectionString: mongodb+srv://MirSevMongo:thoHOQXpmkU7m7kx@cluster0.08jipe7.mongodb.net/?retryWrites=true&w=majority
        public Repository(string collectionName)
            : this ("mongodb://localhost:27017", collectionName)
        { }

        public Repository(string connectionString, string collectionName)
        {
            var dbName = "OnlineLibrary";

            if (_mongoCollection == null)
            {
                _mongoCollection =
                new MongoClient(connectionString)
                .GetDatabase(dbName)
                .GetCollection<BsonDocument>(collectionName);
            }
        }

        public T Add(T entity)
        {
            var bsonDoc = entity.ToBsonDocument();
            _mongoCollection.InsertOne(bsonDoc);
            entity._id = bsonDoc["_id"].AsObjectId;
            return entity;
        }

        public void Delete(string id)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            _mongoCollection.DeleteOne(deleteFilter);
        }

        public void DeleteAll()
        {
            var deleteAllFilter = Builders<BsonDocument>.Filter.Empty;
            _mongoCollection.DeleteMany(deleteAllFilter);
        }

        public T Get(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var entity = _mongoCollection.Find(filter).FirstOrDefault();
            return MapBsonToDto(entity);
        }

        public IEnumerable<T> GetAll()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var entities = _mongoCollection.Find(filter).ToEnumerable();
            return entities.Select(e => MapBsonToDto(e));
        }

        protected T MapBsonToDto(BsonDocument entity)
        {
            if (entity == null)
            {
                return new T();
            }
            return BsonSerializer.Deserialize<T>(entity);
        }

        public void Update(T entity)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", entity._id);
            var bsonDoc = entity.ToBsonDocument();

            var fieldsToOmit = new string[] { "_id", "Salt", "Hash" };

            foreach (var prop in entity.GetType().GetProperties().Where(p => !fieldsToOmit.Contains(p.Name)))
            {
                var updateBuilder = Builders<BsonDocument>.Update;
                var update = updateBuilder.Set(prop.Name, bsonDoc[prop.Name]);
                _mongoCollection.UpdateOne(filter, update);
            }
        }
    }
}
