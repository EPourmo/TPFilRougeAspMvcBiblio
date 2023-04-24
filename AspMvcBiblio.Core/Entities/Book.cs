using AspMvcBiblio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMvcBiblio.Entities
{
	public class Book : Entity
	{
		public string ISBN { get; set; } = null!;
		public string Title { get; set; } = null!;

		readonly List<Author> _authors = new List<Author>();
		public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

		readonly List<Keyword> _keyWords = new List<Keyword>();

		public IReadOnlyCollection<Keyword> KeyWords => _keyWords.AsReadOnly();

		readonly List<Theme> _themes = new List<Theme>();
		public IReadOnlyCollection<Theme> Themes => _themes.AsReadOnly();
		public int CopiesNumber { get; set; }
		public DateTime ServiceDate { get; set; }


	}
}


