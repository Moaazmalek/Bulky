using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
         Task<IEnumerable<T>> GetAllAsync(string? includeProperties=null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter,string? includeProperties=null);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);





    }
}
