using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer
{
    public class Test
    {
        public IEnumerable<string> GetTestItems()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://MirSevMongo:thoHOQXpmkU7m7kx@cluster0.08jipe7.mongodb.net/?retryWrites=true&w=majority");

            var dbList = dbClient.ListDatabases().ToList();

            var collection = dbClient.GetDatabase("OnlineLibrary").GetCollection<BsonDocument>("Books");

            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Regex("Author", ".*Tolki.*");
            var results = collection.Find(filter).ToList();
            var items = new List<string>();
            foreach (var result in results)
            {
                items.Add(result.ToString());
            }

            return dbList.Select(d => d.ToString()).Concat(items);
        }
    }
}
