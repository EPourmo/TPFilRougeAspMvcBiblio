using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
    public class Author : Person
    {
        public List<AuthorBook> AuthorBooks { get; } = new();

	}
}
