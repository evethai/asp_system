using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> :IGenericCommandRepository<T>,
        IGenericQueryRepository<T>
        where T : class
    {
    }

    public interface IGenericCommandRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }

    public interface IGenericQueryRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetQueryable();
    }

}
