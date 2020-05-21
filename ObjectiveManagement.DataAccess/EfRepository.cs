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
        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Коолекция записей</returns>

        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }
        /// <summary>
        /// Получение записи по условию
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="selector">Условие</param>
        /// <returns></returns>
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return _context.Set<T>().Where(selector).AsQueryable();
        }
        /// <summary>
        /// Добавление обекта в БД
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="newEntity">Экземпляр объекта</param>
        /// <returns>Id созданного объекта</returns>
        public async Task<Guid> AddAsync<T>(T newEntity) where T : class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;
        }
        /// <summary>
        /// Удаление записи из базы
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="id">Id объекта</param>
        /// <returns></returns>
        public async Task RemoveAsync<T>(Guid id) where T : class, IEntity
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }

        /// <summary>
        /// Обновление записи в БД
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="entity">Объект для обновления</param>
        /// <returns></returns>
        public async Task UpdateAsync<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Update(entity));
        }

        /// <summary>
        /// Сохрание изменений
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
