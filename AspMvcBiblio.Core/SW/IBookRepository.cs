using AspMvcBiblio.Entities;
using System.Linq.Expressions;

namespace AspMvcBiblio.Data
{
	public interface IBookRepository
	{
		Task Delete(Book entity);
		Task<Book?> GetById(int id);
		Task<Book?> GetSingle(Expression<Func<Book, bool>> criteria);
		Task<Book> Insert(Book entity);
		Task<IEnumerable<Book>> List(Expression<Func<Book, bool>> criteria);
		Task<IEnumerable<Book>> ListAll();
		Task Update(Book entity);

	
	}
}