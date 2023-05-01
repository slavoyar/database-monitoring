using System.Linq.Expressions;
using DatabaseMonitoring.Services.Notification.Core.Interfaces;
using DatabaseMonitoring.Services.Notification.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMonitoring.Services.Notification.Infrastructure.Data;

public class EfRepository<T> 
        : IRepository<T>
         where T : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public EfRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> exspression = null)
        {
            if(exspression == null)
                return await dbSet.ToListAsync();

            IQueryable<T> query = dbSet;
            return await query.Where(exspression).ToListAsync();
        }

        public async Task<T> GetFristOrDefaultAsync(Expression<Func<T, bool>> exspression)
        {
            IQueryable<T> query = dbSet;
            return await query.Where(exspression).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
            => await GetFristOrDefaultAsync(x => x.Id == id);
        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(()  => dbSet.Update(entity));
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
            return entity;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }


    }