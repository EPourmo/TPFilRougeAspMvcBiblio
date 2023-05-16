using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMvcBiblio.Data;
using AspMvcBiblio.Entities;
using System.Net.Http;

namespace AspMvcBiblio.Controllers
{
    public class KeywordsController : Controller
    {
        readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient => _httpClientFactory.CreateClient("API");
        public KeywordsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Keywords
        public async Task<IActionResult> Index()
        {
            var keywords = await HttpClient
               .GetFromJsonAsync<IEnumerable<Keyword>>("api/Keywords");
            return View(keywords);
        }

        // GET: Keywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await HttpClient.
                GetFromJsonAsync<Keyword>($"api/Keywords/{id}");

            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // GET: Keywords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Keywords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Word,Id")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                var response = await HttpClient.
                    PostAsJsonAsync("api/Keywords", keyword);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(keyword);
        }

        // GET: Keywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var keyword = await HttpClient.
               GetFromJsonAsync<Keyword>($"api/Keywords/{id}");

            if (keyword == null)
            {
                return NotFound();
            }
            return View(keyword);
        }

        // POST: Keywords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Word,Id")] Keyword keyword)
        {
            var response = await HttpClient.
                      PutAsJsonAsync($"api/Keywords/{id}", keyword);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(keyword);
        }

        // GET: Keywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var keyword = await HttpClient
                .GetFromJsonAsync<Keyword>($"api/Keywords/{id}");

            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await HttpClient.
                DeleteAsync($"api/Keywords/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        //private bool KeywordExists(int id)
        //{
        //  return (_context.KeyWords?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
