using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataLayer.Repositories
{
    public class UserRepository : Repository<UserDto>, IUserRepository
    {
        public UserRepository()
            : base("User") { }

        public UserRepository(string connectionString)
            : base(connectionString, "User") { }

        public IEnumerable<UserDto> Find(FindTypeDb findType, string username, string firstname, string surname, string address, string pin, string sortBy = "Username")
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            var filters = new List<FilterDefinition<BsonDocument>>();
            if (!string.IsNullOrEmpty(username) && username.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Username", $".*{username}.*"));
            }
            if (!string.IsNullOrEmpty(firstname) && firstname.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Firstname", $".*{firstname}.*"));
            }
            if (!string.IsNullOrEmpty(surname) && surname.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Surname", $".*{surname}.*"));
            }
            if (!string.IsNullOrEmpty(address) && address.Length >= 3)
            {
                var addressFilter = filterBuilder.Or(new List<FilterDefinition<BsonDocument>>
                {
                    filterBuilder.Regex("Address.Street", $".*{address}.*"),
                    filterBuilder.Regex("Address.DescriptiveNumber", $".*{address}.*"),
                    filterBuilder.Regex("Address.OrientationNumber", $".*{address}.*"),
                    filterBuilder.Regex("Address.City", $".*{address}.*"),
                    filterBuilder.Regex("Address.Zip", $".*{address}.*"),
                });
                filters.Add(addressFilter);
            }
            if (!string.IsNullOrEmpty(pin) && pin.Length >= 3)
            {
                filters.Add(filterBuilder.Regex("Pin", $".*{pin}.*"));
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
                .Select(e => BsonSerializer.Deserialize<UserDto>(e));
        }

        public UserDto GetByUserName(string username)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", username);
            var entity = _mongoCollection.Find(filter).FirstOrDefault();
            return MapBsonToDto(entity);
        }
    }
}
