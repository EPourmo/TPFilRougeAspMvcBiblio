using AspMvcBiblio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Data
{
    public interface IRepository<T>
            where T : Entity
    {
        Task<T?> GetById(int id);
        Task<T?> GetSingle(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> criteria);
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
