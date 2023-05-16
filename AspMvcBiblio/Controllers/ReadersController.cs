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
    public class ReadersController : Controller
    {

        readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient => _httpClientFactory.CreateClient("API");

        public ReadersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
            var readers = await HttpClient
                .GetFromJsonAsync<IEnumerable<Reader>>("api/Readers");
            return View(readers);
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var reader = await HttpClient.
                    GetFromJsonAsync<Reader>($"api/Readers/{id}");
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Email,Phone,Id")] Reader reader)
        {

            if (ModelState.IsValid)
            {
                var response = await HttpClient.
                    PostAsJsonAsync("api/Readers", reader);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reader = await HttpClient.
              GetFromJsonAsync<Reader>($"api/Readers/{id}");

            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            var response = await HttpClient.
                  PutAsJsonAsync($"api/Readers/{id}", reader);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }



        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await HttpClient
                .GetFromJsonAsync<Reader>($"api/Readers/{id}");

            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
    
            var response = await HttpClient.
              DeleteAsync($"api/Readers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        //private bool ReaderExists(int id)
        //{
        //    return (_context.Readers?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
