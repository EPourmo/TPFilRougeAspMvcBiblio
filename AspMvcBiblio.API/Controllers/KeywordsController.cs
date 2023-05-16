using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspMvcBiblio.Data;
using AspMvcBiblio.Entities;

namespace AspMvcBiblio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly BiblioContext _context;

        public KeywordsController(BiblioContext context)
        {
            _context = context;
        }

        // GET: api/Keywords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keyword>>> GetKeyWords()
        {
          if (_context.KeyWords == null)
          {
              return NotFound();
          }
            return await _context.KeyWords.ToListAsync();
        }

        // GET: api/Keywords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Keyword>> GetKeyword(int id)
        {
          if (_context.KeyWords == null)
          {
              return NotFound();
          }
            var keyword = await _context.KeyWords.FindAsync(id);

            if (keyword == null)
            {
                return NotFound();
            }

            return keyword;
        }

        // PUT: api/Keywords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeyword(int id, Keyword keyword)
        {
            if (id != keyword.Id)
            {
                return BadRequest();
            }

            _context.Entry(keyword).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeywordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Keywords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Keyword>> PostKeyword(Keyword keyword)
        {
          if (_context.KeyWords == null)
          {
              return Problem("Entity set 'BiblioContext.KeyWords'  is null.");
          }
            _context.KeyWords.Add(keyword);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKeyword", new { id = keyword.Id }, keyword);
        }

        // DELETE: api/Keywords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyword(int id)
        {
            if (_context.KeyWords == null)
            {
                return NotFound();
            }
            var keyword = await _context.KeyWords.FindAsync(id);
            if (keyword == null)
            {
                return NotFound();
            }

            _context.KeyWords.Remove(keyword);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KeywordExists(int id)
        {
            return (_context.KeyWords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
