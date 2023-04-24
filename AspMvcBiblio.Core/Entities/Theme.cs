using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
    public class Theme : Entity
    {

        public string DomainName { get; set; } = null!;
        public string? Description { get; set; } 

        readonly List<Book> _books = new List<Book>();
        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();
    }
}
