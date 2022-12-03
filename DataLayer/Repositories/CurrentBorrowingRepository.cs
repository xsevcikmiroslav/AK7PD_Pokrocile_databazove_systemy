using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public class CurrentBorrowingRepository : BorrowingRepository, ICurrentBorrowingRepository
    {
        public CurrentBorrowingRepository()
            : base("Borrowing") { }

        public CurrentBorrowingRepository(string connectionString)
            : base(connectionString, "Borrowing") { }

        public IEnumerable<BorrowingDto> GetUsersCurrentBorrowings(string userId)
        {
            return GetCurrentBorrowings("UserId", userId);
        }

        private IEnumerable<BorrowingDto> GetCurrentBorrowings(string foreignIdName, string foreignIdValue)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new FilterDefinition<BsonDocument>[] {
                filterBuilder.Eq(foreignIdName, ObjectId.Parse(foreignIdValue)),
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

        public IEnumerable<BorrowingDto> GetBookCurrentBorrowings(string bookId)
        {
            return GetCurrentBorrowings("BookId", bookId);
        }
    }
}
