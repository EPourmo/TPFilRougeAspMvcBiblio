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
    public class ThemeRepository : IThemeRepository
    {
        BiblioContext _context { get; }
        public ThemeRepository(BiblioContext context)
        {
            _context = context;
        }

        public async Task<Theme?> GetById(int id)
        {
            return await _context.Themes
            .Include(a => a.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task<Theme?> GetSingle(Expression<Func<Theme, bool>> criteria)
        {
            return await _context.Themes
            .Include(a => a.Books)
            .SingleOrDefaultAsync(criteria);
        }



        public async Task<IEnumerable<Theme>> ListAll()
        {
            return await _context.Themes.ToListAsync();
        }



        public async Task<IEnumerable<Theme>> List(Expression<Func<Theme, bool>> criteria)
        {
            return await _context.Themes
            .Include(a => a.Books)
            .Where(criteria).ToListAsync();
        }



        public async Task<Theme> Insert(Theme entity)
        {
            _context.Themes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task Update(Theme entity)
        {
            _context.Themes.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(Theme entity)
        {
            _context.Themes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
