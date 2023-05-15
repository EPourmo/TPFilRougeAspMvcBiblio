using AspMvcBiblio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.SW
{
    public interface IReaderRepository
    {

        Task Delete(Reader entity);
        Task<Reader?> GetById(int id);
        Task<Reader?> GetSingle(Expression<Func<Reader, bool>> criteria);
        Task<Reader> Insert(Reader entity);
        Task<IEnumerable<Reader>> List(Expression<Func<Reader, bool>> criteria);
        Task<IEnumerable<Reader>> ListAll();
        Task Update(Reader entity);
    }
}

