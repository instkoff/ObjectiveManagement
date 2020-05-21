using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObjectiveManagement.DataAccess.Entities;

namespace ObjectiveManagement.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с БД
    /// </summary>
    public class EfRepository : IDbRepository
    {
        private readonly DataContext _context;

        public EfRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return _context.Set<T>().Where(selector).AsQueryable();
        }

        public async Task<Guid> AddAsync<T>(T newEntity) where T : class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;
        }


        public async Task RemoveAsync<T>(Guid id) where T : class, IEntity
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }


        public async Task UpdateAsync<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
