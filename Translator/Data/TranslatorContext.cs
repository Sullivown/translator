using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Translator.Models;

namespace Translator.Data
{
    public class TranslatorContext : DbContext
    {
        public TranslatorContext (DbContextOptions<TranslatorContext> options)
            : base(options)
        {
        }

        public DbSet<Translator.Models.Translation> Translation { get; set; } = default!;

        public DbSet<Translator.Models.Calls> Calls { get; set; } = default!;
    }
}
