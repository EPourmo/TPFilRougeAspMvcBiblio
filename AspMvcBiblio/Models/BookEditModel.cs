using AspMvcBiblio.Entities;
using System.ComponentModel.DataAnnotations;

namespace AspMvcBiblio.Models
{
    public class BookEditModel
    {
        public Book Book { get; set; } = null!;

        public int AuthorId { get; set; }

        public int ThemeId { get; set; }

        public int KeywordsId { get; set; }

    }
}
