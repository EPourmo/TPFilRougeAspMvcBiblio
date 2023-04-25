using AspMvcBiblio.Entities;
using System.Linq.Expressions;

namespace AspMvcBiblio.Data
{
	public interface IAuthorRepository
	{
		Task Delete(Author entity);
		Task<Author?> GetById(int id);
		Task<Author?> GetSingle(Expression<Func<Author, bool>> criteria);
		Task<Author> Insert(Author entity);
		Task<IEnumerable<Author>> List(Expression<Func<Author, bool>> criteria);
		Task<IEnumerable<Author>> ListAll();
		Task Update(Author entity);
	}
}