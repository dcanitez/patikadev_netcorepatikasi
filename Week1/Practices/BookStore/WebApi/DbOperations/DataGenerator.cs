using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;
                else
                context.Books.AddRange(new Book
                    {
                        // Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, //PersonalGrowth
                        PageCount = 200,
                        PublishedDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        // Id = 2,
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishedDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 2, //Science Fiction
                        PageCount = 540,
                        PublishedDate = new DateTime(2001, 12, 21)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}