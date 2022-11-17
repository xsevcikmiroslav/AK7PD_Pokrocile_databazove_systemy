using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;

namespace DataLayer.Repositories
{
    public class BorrowingRepository : Repository<BorrowingDto>, IBorrowingRepository
    {
        public BorrowingRepository()
            : base("Borrowing") { }

        public BorrowingRepository(string connectionString)
            : base(connectionString, "Borrowing") { }

        public IEnumerable<BorrowingDto> GetAllBorrowingsByUser(string userId)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filter = filterBuilder.Eq("UserId", ObjectId.Parse(userId));

            var entities = _mongoCollection.Find(filter).ToEnumerable();
            foreach (var entity in entities)
            {
                yield return MapBsonToDto(entity);
            }
        }

        public IEnumerable<BorrowingDto> GetCurrentBorrowingsByUser(string userId)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new FilterDefinition<BsonDocument>[] {
                filterBuilder.Eq("UserId", ObjectId.Parse(userId)),
                filterBuilder.Eq("DateTimeReturned", DateTime.MinValue),
                filterBuilder.Gt("DateTimeBorrowed", DateTime.MinValue)
            };

            var filter = filterBuilder.And(filters);

            var entities = _mongoCollection.Find(filter).ToEnumerable();
            foreach (var entity in entities)
            {
                yield return MapBsonToDto(entity);
            }
        }

        public BorrowingDto GetByUserAndBook(string userId, string bookId)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new FilterDefinition<BsonDocument>[] {
                filterBuilder.Eq("UserId", ObjectId.Parse(userId)),
                filterBuilder.Eq("BookId", ObjectId.Parse(bookId))
            };

            var filter = filterBuilder.And(filters);

            var entity = _mongoCollection.Find(filter).FirstOrDefault();
            return MapBsonToDto(entity);
        }
    }
}
