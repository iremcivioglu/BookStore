using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.BookTitle == Model.BookTitle);
            if (book != null)
                throw new InvalidOperationException("Kitap zaten mevcut.");
            book = new Book();
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.BookTitle = Model.BookTitle != default ? Model.BookTitle : book.BookTitle;

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string BookTitle { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
