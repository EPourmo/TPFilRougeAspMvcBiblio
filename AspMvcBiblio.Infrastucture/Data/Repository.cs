using AspMvcBiblio.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspMvcBiblio.Data
{
	public class Repository<T> : IRepository<T>
		where T : Entity
	{
		BiblioContext _context { get; }

		public Repository(BiblioContext context)
		{
			_context = context;
		}

		public async Task<T?> GetById(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T?> GetSingle(Expression<Func<T, bool>> criteria)
		{
			return await _context.Set<T>().SingleOrDefaultAsync(criteria);
		}

		public async Task<IEnumerable<T>> ListAll()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> criteria)
		{
			return await _context.Set<T>().Where(criteria).ToListAsync();
		}

		public async Task<T> Insert(T entity)
		{
			_context.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		public async Task Update(T entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
