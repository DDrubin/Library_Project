using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Library.Data;

namespace BookStore.Models
{//TODO: delete and connect external db like google books API
    public class BookDbInitializer

    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Books.Any())
                {
                    return;   // DB has been seeded
                }
                if (context.Reservations.Any())
                {
                    return;   // DB has been seeded
                }
                context.Reservations.AddRange(
                    new BookReservation
                    {
                        ReservationDate = DateTime.Parse("1989-2-12"),
                        BookID = 1,
                        UserID = "gg"
                    }
                    );
                context.Books.AddRange(
                    new Book
                    {
                        Name = "Black Board",
                        Author = "Prawnik",
                        PublishDate = DateTime.Parse("1989-2-12"),
                        Description = "997–2021 Blackboard Inc. Wszelkie prawa zastrzeżone. Patent nr 7 493 396 i 7 558 853 na terenie Stanów Zjednoczonych. Dodatkowe zgłoszenia patentowe oczekują na rozpatrzenie. "
                    },

                    new Book
                    {
                        Name = "Read description",
                        Author = "J. J. Jokes",
                        PublishDate = DateTime.Parse("1989-2-12"),
                        Description = "997–2021 Blackboard Inc. Wszelkie prawa zastrzeżone. Patent nr 7 493 396 i 7 558 853 na terenie Stanów Zjednoczonych. Dodatkowe zgłoszenia patentowe oczekują na rozpatrzenie. "
                    },

                    new Book
                    {
                        Name = "Garward",
                        Author = "New Ton",
                        PublishDate = DateTime.Parse("1989-2-12"),
                        Description = "997–2021 Blackboard Inc. Wszelkie prawa zastrzeżone. Patent nr 7 493 396 i 7 558 853 na terenie Stanów Zjednoczonych. Dodatkowe zgłoszenia patentowe oczekują na rozpatrzenie. "
                    },

                    new Book
                    {
                        Name = "First price",
                        Author = "Firan",
                        PublishDate = DateTime.Parse("1989-2-12"),
                        Description = "997–2021 Blackboard Inc. Wszelkie prawa zastrzeżone. Patent nr 7 493 396 i 7 558 853 na terenie Stanów Zjednoczonych. Dodatkowe zgłoszenia patentowe oczekują na rozpatrzenie. "
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
