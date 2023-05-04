using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace T.Models
{
    public class PlacesAgency
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("agency")]
        public int AgencyID { get; set; }
        public Agency agency { get; set; }

        [ForeignKey("place")]
        public int PlaceID { get; set; }
        public Places place { get; set; }
    }
}
