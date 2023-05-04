using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace T.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("AgencyPackage")]

        public int Agency_ID { get; set; }
        public virtual Agency AgencyPackage { get; set; }
    }
}
