using Agregation.Domain.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Agregation.Domain.Interfaces
{
    /// <summary>
    /// Репозиторий чтения и записи
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class AbstractRepository<T> : IAbstractRepository<T> where T : class, IEntity
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> _entitySet;

        protected AbstractRepository(DbContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        #region Get

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>сущность</returns>
        public T? Get(Guid id)
        {
            return _entitySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по ID
        /// </summary>
        /// <param name="id">ID сущности</param>
        /// <returns>сущность</returns>
        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Запросить все сущности в базе
        /// </summary>
        /// <param name="asNoTracking">Вызвать с AsNoTracking</param>
        /// <returns>IQueryable массив сущностей</returns>
        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        /// <summary>
        /// Запросить все сущности в базе
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="asNoTracking">Вызвать с AsNoTracking</param>
        /// <returns>Список сущностей</returns>
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll(asNoTracking).ToListAsync(cancellationToken);
        }

        public List<T> GetPaged(int page, int itemsPerPage)
        {
            var query = _entitySet
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
            return query.ToList();
        }

        public Task<List<T>> GetPagedAsync(int page, int itemsPerPage)
        {
            return Task.Run(() => GetPaged(page, itemsPerPage));
        }

        #endregion

        #region Create

        /// <summary>
        /// Добавить в базу одну сущность
        /// </summary>
        /// <param name="entity">сущность для добавления</param>
        /// <returns>добавленная сущность</returns>
        public T Add(T entity)
        {
            var objToReturn = _entitySet.Add(entity);
            return objToReturn.Entity;
        }

        /// <summary>
        /// Добавить в базу одну сущность
        /// </summary>
        /// <param name="entity">сущность для добавления</param>
        /// <returns>добавленная сущность</returns>
        public async Task<T> AddAsync(T entity)
        {
            return (await _entitySet.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Добавить в базу массив сущностей
        /// </summary>
        /// <param name="entities">массив сущностей</param>
        public void AddRange(List<T> entities)
        {
            var enumerable = entities as IList<T> ?? entities.ToList();
            _entitySet.AddRange(enumerable);
        }

        /// <summary>
        /// Добавить в базу массив сущностей
        /// </summary>
        /// <param name="entities">массив сущностей</param>
        public async Task AddRangeAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }
            await _entitySet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        /// <summary>
        /// Для сущности проставить состояние - что она изменена
        /// </summary>
        /// <param name="entity">сущность для изменения</param>
        public bool TryUpdate(T entity)
        {
            if (_entitySet.Find(entity.Id) == null)
                return false;
            Context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id">ID удалённой сущности</param>
        /// <returns>была ли сущность удалена</returns>
        public bool TryDelete(Guid id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">сущность для удаления</param>
        /// <returns>была ли сущность удалена</returns>
        public bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            Context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        /// <summary>
        /// Удалить сущности
        /// </summary>
        /// <param name="entities">Коллекция сущностей для удаления</param>
        /// <returns>была ли операция завершена успешно</returns>
        public bool DeleteRange(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _entitySet.RemoveRange(entities);
            return true;
        }

        #endregion

        #region SaveChanges

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
