using AspMvcBiblio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AspMvcBiblio.Data
{
	public class AuthorRepository : IAuthorRepository
	{
		BiblioContext _context { get; }
		public AuthorRepository(BiblioContext context)
		{
			_context = context;
		}

		public async Task<Author?> GetById(int id)
		{
			return await _context.Authors
			.Include(a => a.Books)
			.FirstOrDefaultAsync(b => b.Id == id);
		}



		public async Task<Author?> GetSingle(Expression<Func<Author, bool>> criteria)
		{
			return await _context.Authors
			.Include(a => a.Books)
			.SingleOrDefaultAsync(criteria);
		}



		public async Task<IEnumerable<Author>> ListAll()
		{
			return await _context.Authors.ToListAsync();
		}



		public async Task<IEnumerable<Author>> List(Expression<Func<Author, bool>> criteria)
		{
			return await _context.Authors
			.Include(a => a.Books)
			.Where(criteria).ToListAsync();
		}



		public async Task<Author> Insert(Author entity)
		{
			_context.Authors.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		public async Task Update(Author entity)
		{
			_context.Authors.Update(entity);
			await _context.SaveChangesAsync();
		}



		public async Task Delete(Author entity)
		{
			_context.Authors.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}