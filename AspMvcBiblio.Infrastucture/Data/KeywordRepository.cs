using AspMvcBiblio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Data
{
	public class KeywordRepository : IKeywordRepository
	{
		BiblioContext _context { get; }
		public KeywordRepository(BiblioContext context)
		{
			_context = context;
		}

		public async Task<Keyword?> GetById(int id)
		{
			return await _context.KeyWords
			.FindAsync(id);
		}



		public async Task<Keyword?> GetSingle(Expression<Func<Keyword, bool>> criteria)
		{
			return await _context.KeyWords
				.SingleOrDefaultAsync(criteria);
		}



		public async Task<IEnumerable<Keyword>> ListAll()
		{
			return await _context.KeyWords.ToListAsync();
		}



		public async Task<IEnumerable<Keyword>> List(Expression<Func<Keyword, bool>> criteria)
		{
			return await _context.KeyWords
			.Where(criteria).ToListAsync();
		}



		public async Task<Keyword> Insert(Keyword entity)
		{
			_context.KeyWords.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
		public async Task Update(Keyword entity)
		{
			_context.KeyWords.Update(entity);
			await _context.SaveChangesAsync();
		}


		public async Task Delete(Keyword entity)
		{
			_context.KeyWords.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
