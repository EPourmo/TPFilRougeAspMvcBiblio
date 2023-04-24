using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Data
{
 
    public class BiblioContextFactory : IDesignTimeDbContextFactory<BiblioContext>
    {
        public BiblioContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiblioContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=AspMvcBiblioDB;Integrated Security=True;MultipleActiveResultSets=true");


            return new BiblioContext(optionsBuilder.Options);
        }
    }
}
