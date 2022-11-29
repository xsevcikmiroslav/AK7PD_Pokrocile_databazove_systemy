using DataLayer.DTO;

namespace DataLayer.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : BaseDto
    {
        T Get(string id);

        T Add(T entity);

        void Update(T entity);

        void Delete(string id);

        void DeleteAll();
    }
}
