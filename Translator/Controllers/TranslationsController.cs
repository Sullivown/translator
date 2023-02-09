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
    public class TranslationsController : Controller
    {
        private readonly TranslatorContext _context;

        public TranslationsController(TranslatorContext context)
        {
            _context = context;
        }

        // GET: Translations
        public async Task<IActionResult> Index()
        {
              return _context.Translation != null ? 
                          View(await _context.Translation.ToListAsync()) :
                          Problem("Entity set 'TranslatorContext.Translation'  is null.");
        }

        // GET: Translations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Translation == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Translations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(translation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(translation);
        }

        // GET: Translations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Translation == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }
            return View(translation);
        }

        // POST: Translations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url")] Translation translation)
        {
            if (id != translation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(translation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationExists(translation.Id))
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
            return View(translation);
        }

        // GET: Translations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Translation == null)
            {
                return NotFound();
            }

            var translation = await _context.Translation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Translations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Translation == null)
            {
                return Problem("Entity set 'TranslatorContext.Translation'  is null.");
            }
            var translation = await _context.Translation.FindAsync(id);
            if (translation != null)
            {
                _context.Translation.Remove(translation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranslationExists(int id)
        {
          return (_context.Translation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
