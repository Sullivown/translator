using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using Translator.Data;
using Translator.Models;
using Newtonsoft.Json;

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
            return _context.Translation != null ?
                          View(await _context.Translation.ToListAsync()) :
                          Problem("Entity set 'TranslatorContext.Translation'  is null.");

        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection collection)  
        {
            ViewData["OriginalText"] = collection["originalText"];
            ViewData["TranslatorType"] = collection["translatorType"];

            // Make API call
            using (var client = new HttpClient())
            {
                var uri = new Uri(collection["translatorUrl"] + "?text=" + collection["originalText"]);

                var response = await client.GetAsync(uri);

                var responseStatus = (int)response.StatusCode;

                string textResult = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<dynamic>(textResult);

                // If error, show error message
                if (responseStatus != 200)
                {
                    ViewData["translatedText"] = result?.error.message;
                } else
                {
                    ViewData["translatedText"] = result?.contents.translated;

                }

                
            }

            return _context.Translation != null ?
               View(await _context.Translation.ToListAsync()) :
               Problem("Entity set 'TranslatorContext.Translation'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}