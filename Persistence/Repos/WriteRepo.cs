using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Contexts;

namespace Persistence.Repos
{
    public class WriteRepo<T> : IWriteRepo<T> where T : BaseEntity
    {
        readonly CarbonSweeperDbContext dbContext;
        public WriteRepo(CarbonSweeperDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public DbSet<T> Table => dbContext.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public bool Remove(T entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            T entity = await Table.FirstOrDefaultAsync(x => id == x.Id);
            return Remove(entity);
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }


        public bool Update(T model)
        {
            EntityEntry entity = Table.Update(model);
            return entity.State == EntityState.Modified;
        }

        public void UpdateRange(List<T> entities)
        {
            Table.UpdateRange(entities);
        }
        public async Task<int> SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
