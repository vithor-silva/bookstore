using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        public ICollection<Book> BooKs { get; set; } = new List<Book>();

        public Genre()
        {
            
        }
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
