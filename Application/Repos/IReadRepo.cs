using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repos
{
    public interface IReadRepo<T> : IRepo<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool isTracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T> GetByIdAsync(int id, bool isTracking = true);
    }
}
