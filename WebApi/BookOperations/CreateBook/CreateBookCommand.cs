using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.BookTitle == Model.BookTitle);
            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut.");
            book = new Book();
            book.BookTitle = Model.BookTitle;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string BookTitle { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
