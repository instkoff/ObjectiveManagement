using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ObjectiveManagement.DataAccess.Entities;

namespace ObjectiveManagement.DataAccess
{
    public interface IDbRepository
    {
        IQueryable<T> Get<T>() where T : class, IEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
        Task<Guid> AddAsync<T>(T newEntity) where T : class, IEntity;
        Task RemoveAsync<T>(Guid id) where T : class, IEntity;
        Task<int> SaveChangesAsync();
    }
}