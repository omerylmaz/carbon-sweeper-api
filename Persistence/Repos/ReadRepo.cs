using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public class ReadRepo<T> : IReadRepo<T> where T : BaseEntity
    {
        readonly CarbonSweeperDbContext dbContext;
        public ReadRepo(CarbonSweeperDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public DbSet<T> Table => dbContext.Set<T>();

        public async Task<T> GetByIdAsync(int id, bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> GetAll(bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
                query = query.AsNoTracking();
            return query;

        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = GetAll(isTracking);
            if (!isTracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = GetAll(isTracking);
            if (!isTracking)
                query = query.AsNoTracking().Where(expression);
            else
                query = query.Where(expression);
            return query;
        }
    }
}
