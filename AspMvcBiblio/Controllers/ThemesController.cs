using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMvcBiblio.Data;
using AspMvcBiblio.Entities;

namespace AspMvcBiblio.Controllers
{
    public class ThemesController : Controller
    {
        readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient => _httpClientFactory.CreateClient("API");

        public ThemesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Themes
        public async Task<IActionResult> Index()
        {
            var themes = await HttpClient
              .GetFromJsonAsync<IEnumerable<Theme>>("api/Themes");
            return View(themes);
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await HttpClient.
               GetFromJsonAsync<Theme>($"api/Themes/{id}");


            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DomainName,Description,Id")] Theme theme)
        {
            if (ModelState.IsValid)
            {

                var response = await HttpClient.
                    PostAsJsonAsync("api/Themes", theme);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(theme);
            }
            return View(theme);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }
            var theme = await HttpClient.
               GetFromJsonAsync<Theme>($"api/Themes/{id}");

            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DomainName,Description,Id")] Theme theme)
        {
            var response = await HttpClient.
                      PutAsJsonAsync($"api/Themes/{id}", theme);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var theme = await HttpClient
               .GetFromJsonAsync<Theme>($"api/Themes/{id}");

            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await HttpClient.
                DeleteAsync($"api/Themes/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        //private bool ThemeExists(int id)
        //{
        //    return (_context.Themes?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
