using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Translator.Data;
using Translator.Models;

namespace Translator.Controllers
{
    public class CallsController : Controller
    {
        private readonly TranslatorContext _context;

        public CallsController(TranslatorContext context)
        {
            _context = context;
        }

        // GET: Calls
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.OriginalTextSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.OriginalTextSortParm = sortOrder == "originalText" ? "originalText_desc" : "originalText";
            ViewBag.TranslatorTypeSortParm = sortOrder == "translatorType" ? "translatorType_desc" : "translatorType";
            ViewBag.IsSuccessfulSortParm = sortOrder == "isSuccessful" ? "isSuccessful_desc" : "isSuccessful";
            ViewBag.translatedTextSortParm = sortOrder == "translatedText" ? "translatedText_desc" : "translatedText";
            ViewBag.DateCreatedSortParm = sortOrder == "dateCreated" ? "dateCreated_desc" : "dateCreated";
            var calls = from c in _context.Calls
                           select c;
            switch (sortOrder)
            {
                case "id_desc":
                    calls = calls.OrderByDescending(c => c.OriginalText);
                    break;
                case "originalText":
                    calls = calls.OrderBy(c => c.OriginalText);
                    break;
                case "originalText_desc":
                    calls = calls.OrderByDescending(c => c.OriginalText);
                    break;
                case "translatorType":
                    calls = calls.OrderBy(c => c.TranslatorType);
                    break;
                case "translatorType_desc":
                    calls = calls.OrderByDescending(c => c.TranslatorType);
                    break;
                case "isSuccessful":
                    calls = calls.OrderBy(c => c.IsSuccessful);
                    break;
                case "isSuccessful_desc":
                    calls = calls.OrderByDescending(c => c.IsSuccessful);
                    break;
                case "translatedText":
                    calls = calls.OrderBy(c => c.TranslatedText);
                    break;
                case "translatedText_desc":
                    calls = calls.OrderByDescending(c => c.TranslatedText);
                    break;
                case "dateCreated":
                    calls = calls.OrderBy(c => c.DateCreated);
                    break;
                case "dateCreated_desc":
                    calls = calls.OrderByDescending(c => c.DateCreated);
                    break;
                default:
                    calls = calls.OrderBy(c => c.Id);
                    break;
            }
            return _context.Calls != null ? 
                          View(await calls.ToListAsync()) :
                          Problem("Entity set 'TranslatorContext.Calls'  is null.");
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calls == null)
            {
                return NotFound();
            }

            return View(calls);
        }

        // GET: Calls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OriginalText,TranslatorType,IsSuccessful,TranslatedText,DateCreated")] Calls calls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calls);
        }

        // GET: Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls.FindAsync(id);
            if (calls == null)
            {
                return NotFound();
            }
            return View(calls);
        }

        // POST: Calls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OriginalText,TranslatorType,IsSuccessful,TranslatedText,DateCreated")] Calls calls)
        {
            if (id != calls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallsExists(calls.Id))
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
            return View(calls);
        }

        // GET: Calls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var calls = await _context.Calls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calls == null)
            {
                return NotFound();
            }

            return View(calls);
        }

        // POST: Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calls == null)
            {
                return Problem("Entity set 'TranslatorContext.Calls'  is null.");
            }
            var calls = await _context.Calls.FindAsync(id);
            if (calls != null)
            {
                _context.Calls.Remove(calls);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallsExists(int id)
        {
          return (_context.Calls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
