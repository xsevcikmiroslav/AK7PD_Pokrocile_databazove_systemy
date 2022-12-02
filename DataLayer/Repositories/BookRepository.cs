using DataLayer.DTO;
using DataLayer.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public class BookRepository : Repository<BookDto>, IBookRepository
    {
        public BookRepository()
            : base("Book") { }

        public BookRepository(string connectionString)
            : base(connectionString, "Book") { }

        public IEnumerable<BookDto> Find(FindTypeDb findType, string title, string author, int yearOfPublication, string sortBy = "Title")
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new List<FilterDefinition<BsonDocument>>();
            if (!string.IsNullOrEmpty(title) && title.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Title", $".*{title}.*"));
            }
            if (!string.IsNullOrEmpty(author) && author.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Author", $".*{author}.*"));
            }
            if (yearOfPublication > 0)
            {
                filters.Add(filterBuilder.Eq("YearOfPublication", yearOfPublication));
            }

            FilterDefinition<BsonDocument> filter;
            if (filters.Any())
            {
                filter =
                    findType == FindTypeDb.AND
                    ? filterBuilder.And(filters)
                    : filterBuilder.Or(filters);
            }
            else
            {
                filter = filterBuilder.Empty;
            }

            var queryResult = _mongoCollection.Find(filter);

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sort = Builders<BsonDocument>.Sort.Ascending(sortBy);
                queryResult = queryResult.Sort(sort);
            }

            return
                queryResult
                .ToEnumerable()
                .Select(e => BsonSerializer.Deserialize<BookDto>(e));
        }
    }
}
