using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories.Implementations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected readonly DataContext _context;

        protected RepositoryBase(DataContext context)
        {
            _context = context;
        }


        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>()
                : _context.Set<T>().AsNoTracking();
        }

        public Task<T?> GetByIdAsync(int Id)
        {
            return _context.Set<T>().FindAsync(Id).AsTask();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task CreateAsyncList(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            await _context.SaveChangesAsync();
        }
        //public async Task CreateAsyncList(List<T> entities)
        //{
        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            _context.Set<T>().AddRange(entities);
        //            await _context.SaveChangesAsync();
        //            transaction.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw; // Hatanın yukarıya fırlatılması
        //        }
        //    }
        //}

        public async Task UpdateAsync(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAllAsync()
        {
            var allEntities = await _context.Set<T>().ToListAsync();

            if (allEntities.Any())
            {
                _context.Set<T>().RemoveRange(allEntities);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
