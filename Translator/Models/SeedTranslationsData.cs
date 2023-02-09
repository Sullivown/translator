using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Translator.Data;
using System;
using System.Linq;

namespace Translator.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new TranslatorContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<TranslatorContext>>()))
        {
            // Look for any movies.
            if (context.Translation.Any())
            {
                return;   // DB has been seeded
            }
            context.Translation.Add(
                new Translation
                {
                    Name = "Leetspeak",
                    Url = "https://api.funtranslations.com/translate/leetspeak.json",
                }
            );
            context.SaveChanges();
        }
    }
}