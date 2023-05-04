using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T.Models
{
    public class Agency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string CNIC { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Logo { get; set; }

        public List<Places> PlacesList { get; set; }
    }
}
