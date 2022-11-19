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
        protected static IMongoCollection<BsonDocument> _mongoCollection;

        public Repository(string collectionName)
            : this ("mongodb+srv://MirSevMongo:thoHOQXpmkU7m7kx@cluster0.08jipe7.mongodb.net/?retryWrites=true&w=majority", collectionName)
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

        public void Add(T entity)
        {
            var bsonDoc = entity.ToBsonDocument();
            bsonDoc["_id"] = ObjectId.Empty;
            _mongoCollection.InsertOne(bsonDoc);
            entity._id = bsonDoc["_id"].AsObjectId;
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

            foreach (var prop in entity.GetType().GetProperties().Where(p => !p.Name.Equals("_id")))
            {
                var updateBuilder = Builders<BsonDocument>.Update;
                var update = updateBuilder.Set(prop.Name, bsonDoc[prop.Name]);
                _mongoCollection.UpdateOne(filter, update);
            }
        }
    }
}
