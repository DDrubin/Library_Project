using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{   //TODO learn about db closer and create permanent not virtual from Visual Studio
    // TODO Make bd structure, learn it in perfection
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

            public DbSet<Book> Books { get; set; }
            public DbSet<BookReservation> Reservations { get; set; }


        }
}
