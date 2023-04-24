using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Core.Entities
{
    public class Author :Person
    {
        readonly List<Book> _books=new List<Book>();

        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    }
}
