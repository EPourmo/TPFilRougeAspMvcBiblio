using System.ComponentModel.DataAnnotations;

namespace AspMvcBiblio.Models
{
    public class BookCreateModel
    {
        [Required]
        [StringLength(20)]
        public string? ISBN { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; } 


        public int AuthorId { get; set; }
       


    }
}
