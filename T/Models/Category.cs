using System.ComponentModel.DataAnnotations;

namespace T.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
