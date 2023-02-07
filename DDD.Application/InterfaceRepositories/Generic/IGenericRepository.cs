
namespace DDD.Application.InterfaceRepositories.Generic;

public interface IGenericRepository<T> where T : class
{
    // Get all entities
    Task<IEnumerable<T>> All();

    //Get specific entity based on Id
    Task<T> GetById(Guid id);

    Task<bool> Create(T entity);

    void Delete(T entity);
    // Update entity or add if it does not exist
    Task Update(T entity);

    Task CompleteAsync();
}
