﻿using AspMvcBiblio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AspMvcBiblio.Data
{
    public class BookRepository : IBookRepository
    {
        BiblioContext _context { get; }
        public BookRepository(BiblioContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetById(int id)
        {

            return await _context.Books
            .Include(a => a.Authors)
            .Include(b => b.KeyWords)
            .Include(b => b.Themes)
            .FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task<Book?> GetSingle(Expression<Func<Book, bool>> criteria)
        {
            return await _context.Books
            .Include(a => a.Authors)
            .Include(b => b.KeyWords)
            .Include(b => b.Themes)
            .SingleOrDefaultAsync(criteria);
        }



        public async Task<IEnumerable<Book>> ListAll()
        {
            return await _context.Books.Include(a => a.Authors).Include(b => b.KeyWords)
            .Include(b => b.Themes).ToListAsync();
        }



        public async Task<IEnumerable<Book>> List(Expression<Func<Book, bool>> criteria)
        {
            return await _context.Books
            .Include(a => a.Authors)
            .Include(b => b.KeyWords)
            .Include(b => b.Themes).Where(criteria).ToListAsync();
        }



        public async Task<Book> Insert(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task Update(Book entity)
        {
            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
        }



        public async Task Delete(Book entity)
        {
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Search(string query)
        {
            var books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.KeyWords)
                .Include(b => b.Themes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                books = books.Where(b => b.Title.Contains(query)
                                         //|| b.Authors.Any(a=>a.FullName!=null && a.FullName.Contains(query))
                                         //|| b.KeyWords.Any(k => k.Word != null && k.Word.Contains(query))
                                         //|| b.Themes.Any(t => t.DomainName != null && t.DomainName.Contains(query))
                                         );
            }

            return await books.ToListAsync();

        }

    }
}


