using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace T.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<T.Models.Places> Places { get; set; }
        public DbSet<T.Models.Agency> Agency { get; set; }
        public DbSet<T.Models.Category> Category { get; set; }
        public DbSet<T.Models.Package> Package { get; set; }
        public DbSet<T.Models.PlacesAgency> PlacesAgency { get; set; }
        public DbSet<T.Models.Book> Book{ get; set; }

    }
}
