using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Encodings.Web;
using Translator.Data;
using Translator.Models;

namespace Translator.Controllers
{
    public class HomeController : Controller
    {

        private readonly TranslatorContext _context;

        public HomeController(TranslatorContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Hello, this is a test message!";
            return _context.Translation != null ?
                          View(await _context.Translation.ToListAsync()) :
                          Problem("Entity set 'TranslatorContext.Translation'  is null.");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}