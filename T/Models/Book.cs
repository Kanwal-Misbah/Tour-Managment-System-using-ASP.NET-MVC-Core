using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace T.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public BookState Status { get; set; }
        public DateTime LastUpdated { get; set; }
        // Book
        [ForeignKey("Place")]
        public int placeId { get; set; }
        public virtual Places Place { get; set; }
        // User
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual IdentityUser Users { get; set; }
    }

    public enum BookState
    {
        InCart,
        BookPlaced,
        Verifying,
        Inprocess,
        Delivered
    }

}
