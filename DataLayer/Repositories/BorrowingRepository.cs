using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public abstract class BorrowingRepository<T> : Repository<T>, IBorrowingRepository<T>
        where T : BorrowingDto, new()
    {
        public BorrowingRepository(string collectionName)
            : base(collectionName) { }

        public BorrowingRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }

        public T GetByUserAndBook(string userId, string bookId)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new FilterDefinition<BsonDocument>[] {
                filterBuilder.Eq("UserId", ObjectId.Parse(userId)),
                filterBuilder.Eq("BookId", ObjectId.Parse(bookId))
            };

            var filter = filterBuilder.And(filters);

            var queryResult = _mongoCollection.Find(filter);

            var sort = Builders<BsonDocument>.Sort.Descending("DateTimeBorrowed");
            var entity = queryResult.Sort(sort).FirstOrDefault();

            return MapBsonToDto(entity);
        }
    }
}
