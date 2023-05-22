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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Newtonsoft.Json;
using SQLitePCL;

namespace AspMvcBiblio.Controllers
{
    public class BooksController : Controller
    {

        //Pour API
        readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient => _httpClientFactory.CreateClient("API");

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
			HttpResponseMessage response = await HttpClient.GetAsync("https://localhost:7235/api/Books");
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<IEnumerable<Book>>(responseBody);


			return View(book);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await HttpClient.
                GetFromJsonAsync<Book>($"api/Books/{id}");

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
			ViewData["Authors"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Author>>("api/Authors"), nameof(Author.Id), nameof(Author.FullName));
			ViewData["Themes"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Theme>>("api/Themes"), nameof(Theme.Id), nameof(Theme.DomainName));
			ViewData["Keywords"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Keyword>>("api/Keywords"), nameof(Keyword.Id), nameof(Keyword.Word));


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
                var author = await HttpClient.GetFromJsonAsync<Author>($"api/Books/{model.AuthorId}");
                var theme = await HttpClient.GetFromJsonAsync<Theme>($"api/Themes/{model.ThemeId}");
                var keyword = await HttpClient.GetFromJsonAsync<Keyword>($"api/Keywords/{model.KeywordsId}");

                var newbook = new Book()
                {
                    ISBN = model.ISBN!,
                    Title = model.Title!
                };

                newbook.Authors.Add(author);
                newbook.Themes.Add(theme);
                newbook.KeyWords.Add(keyword);

                var response = await HttpClient.
                    PostAsJsonAsync("api/Books", newbook);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
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

            var book = await HttpClient.
                    GetFromJsonAsync<Book>($"api/Books/{id}");

            if (book == null)
            {
                return NotFound();
            }

			ViewData["Authors"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Author>>("api/Authors"), nameof(Author.Id), nameof(Author.FullName));
			ViewData["Themes"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Theme>>("api/Themes"), nameof(Theme.Id), nameof(Theme.DomainName));
			ViewData["Keywords"] = new SelectList(await HttpClient.GetFromJsonAsync<IEnumerable<Keyword>>("api/Keywords"), nameof(Keyword.Id), nameof(Keyword.Word));

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

                    var response = await HttpClient.
                    PutAsJsonAsync($"api/Books/{id}", book);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(book);

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
                //return RedirectToAction(nameof(Index));
            }

            //ViewData["Authors"] = new SelectList(await _authorRepository.ListAll(), nameof(Author.Id), nameof(Author.FullName));
            //ViewData["Themes"] = new SelectList(await _themeRepository.ListAll(), nameof(Theme.Id), nameof(Theme.DomainName));
            //ViewData["Keywords"] = new SelectList(await _keywordRepository.ListAll(), nameof(Keyword.Id), nameof(Keyword.Word));


            ViewData["Authors"] = await HttpClient.GetFromJsonAsync<Author>($"api/Books/{id}/Authors/{book.Authors}");
            ViewData["Themes"] = await HttpClient.GetFromJsonAsync<Theme>($"api/Books/{id}/Themes/{book.Themes}");
            ViewData["Keywords"] = await HttpClient.GetFromJsonAsync<Keyword>($"api/Books/{id}/Keywords/{book.KeyWords}");



            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var book = await HttpClient
                    .GetFromJsonAsync<Book>($"api/Books/{id}");

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
            var response = await HttpClient.
                      DeleteAsync($"api/Books/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }


        private async Task<bool> BookExists(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"api/books/{id}");
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<IActionResult> Search(string query)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"api/books/search?query={query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(content);
                    return View("Index", books);
                }
                else
                {
                    // Handle the error response
                    return View("Error");
                }
            }
        }



    }
}
