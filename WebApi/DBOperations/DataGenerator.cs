using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
       serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Books.AddRange(
                   new Book()
                   {
                       BookTitle = "Lean Startup",
                       GenreId = 2/*(int)GenreEnum.PersonalGrowth*/, // Personal Growth
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   });

                context.SaveChanges();
            }
        }
    }
}
