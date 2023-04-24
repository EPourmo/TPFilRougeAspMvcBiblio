using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
	public class AuthorBook : Entity
	{
        public int AuthorId { get; set; }
		public int BookId { get; set; }

		public Author Author { get; set; } = null!;
		public Book Book { get; set; } = null!;
	}
}
