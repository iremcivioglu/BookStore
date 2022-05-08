using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public DeleteBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.BookId == BookId);
            if (book != null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı.");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }

public class DeleteBookModel
{
    public string BookTitle { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}
}
