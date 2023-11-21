

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll(bool trackChanges);
        Task<T> GetByIdAsync(int Id);
        Task CreateAsync(T entity);
        Task CreateAsyncList(List<T> entities);

        Task UpdateAsync(int Id,T entity);
        Task<bool> DeleteAsync(int Id);
        Task<bool> DeleteAllAsync();

    }
}
