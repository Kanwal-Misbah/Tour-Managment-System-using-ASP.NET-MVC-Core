using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace T.Models
{
    public class Places
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public int Price { get; set; }

        [ForeignKey("PlaceCategory")]
        public int CategoryID { get; set; }
        public virtual Category PlaceCategory { get; set; }

      
        public List<Agency> AgenciesList { get; set; }
    }
}
