using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMvcBiblio.Data;
using AspMvcBiblio.Entities;
using AspMvcBiblio.Models;

namespace AspMvcBiblio.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IThemeRepository _themeRepository;
        private readonly IKeywordRepository _keywordRepository;

        public BooksController(IBookRepository repository, IAuthorRepository authorRepository, IThemeRepository themeRepository, IKeywordRepository keywordRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _themeRepository = themeRepository;
            _keywordRepository = keywordRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _repository.ListAll());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Authors"] = new SelectList(await _authorRepository.ListAll(), nameof(Author.Id), nameof(Author.FullName));
            ViewData["Themes"] = new SelectList(await _themeRepository.ListAll(), nameof(Theme.Id), nameof(Theme.DomainName));
            ViewData["Keywords"] = new SelectList(await _keywordRepository.ListAll(), nameof(Keyword.Id), nameof(Keyword.Word));
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateModel model)
        {

            if (ModelState.IsValid)
            {
                var author = await _authorRepository.GetById(model.AuthorId);
                var theme = await _themeRepository.GetById(model.ThemeId);
                var keyword = await _keywordRepository.GetById(model.KeywordsId);

                if (author == null )
                { return NotFound(); }

                var newbook = new Book()
                {
                    ISBN = model.ISBN!,
                    Title = model.Title!
                };

                newbook.Authors.Add(author);
                newbook.Themes.Add(theme);
                newbook.KeyWords.Add(keyword);

                await _repository.Insert(newbook);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(await _authorRepository.ListAll(), nameof(Author.Id), nameof(Author.FullName));
            ViewData["Themes"] = new SelectList(await _themeRepository.ListAll(), nameof(Theme.Id), nameof(Theme.DomainName));
            ViewData["Keywords"] = new SelectList(await _keywordRepository.ListAll(), nameof(Keyword.Id), nameof(Keyword.Word));
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(book);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.GetById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _repository.GetById(id);

            await _repository.Delete(book);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookExists(int id)
        {
            return await _repository.GetById(id) != null;
        }

        public async Task<IActionResult> Search(string query)
        {
            var books = await _repository.Search(query);
            return View("Index", books);
        }

    }
}
