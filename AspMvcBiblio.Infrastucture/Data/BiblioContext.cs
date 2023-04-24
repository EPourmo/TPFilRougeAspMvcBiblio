using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using AspMvcBiblio.Core.Entities;

namespace AspMvcBiblio.Data
{
    public class BiblioContext : DbContext
    {

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Theme> Themes => Set<Theme>();


        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options)
        {
            
        }
    }
}
