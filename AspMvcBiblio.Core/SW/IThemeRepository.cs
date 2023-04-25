using AspMvcBiblio.Entities;
using System.Linq.Expressions;

namespace AspMvcBiblio.Data
{
    public interface IThemeRepository
    {
        Task Delete(Theme entity);
        Task<Theme?> GetById(int id);
        Task<Theme?> GetSingle(Expression<Func<Theme, bool>> criteria);
        Task<Theme> Insert(Theme entity);
        Task<IEnumerable<Theme>> List(Expression<Func<Theme, bool>> criteria);
        Task<IEnumerable<Theme>> ListAll();
        Task Update(Theme entity);
    }
}