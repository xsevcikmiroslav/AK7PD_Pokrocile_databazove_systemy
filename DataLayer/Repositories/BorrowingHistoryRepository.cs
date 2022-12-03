using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public class BorrowingHistoryRepository : BorrowingRepository, IBorrowingHistoryRepository
    {
        public BorrowingHistoryRepository()
            : base("BorrowingHistory") { }

        public BorrowingHistoryRepository(string connectionString)
            : base(connectionString, "BorrowingHistory") { }

        public IEnumerable<BorrowingDto> GetUsersBorrowingsHistory(string userId)
        {
            return GetBorrowingsHistory("UserId", userId);
        }

        private IEnumerable<BorrowingDto> GetBorrowingsHistory(string foreignIdName, string foreignIdValue)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filter = filterBuilder.Eq(foreignIdName, ObjectId.Parse(foreignIdValue));

            var entities = _mongoCollection.Find(filter).ToEnumerable();
            foreach (var entity in entities)
            {
                yield return MapBsonToDto(entity);
            }
        }

        public IEnumerable<BorrowingDto> GetBookBorrowingsHistory(string bookId)
        {
            return GetBorrowingsHistory("BookId", bookId);
        }
    }
}
