using AspMvcBiblio.Entities;
using System.Linq.Expressions;

namespace AspMvcBiblio.Data
{
	public interface IKeywordRepository
	{
		Task Delete(Keyword entity);
		Task<Keyword?> GetById(int id);
		Task<Keyword?> GetSingle(Expression<Func<Keyword, bool>> criteria);
		Task<Keyword> Insert(Keyword entity);
		Task<IEnumerable<Keyword>> List(Expression<Func<Keyword, bool>> criteria);
		Task<IEnumerable<Keyword>> ListAll();
		Task Update(Keyword entity);
	}
}