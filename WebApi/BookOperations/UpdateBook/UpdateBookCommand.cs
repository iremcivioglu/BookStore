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
        public int BookId { get; set; }
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
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.BookTitle = Model.BookTitle != default ? Model.BookTitle : book.BookTitle;
             
            //_dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string BookTitle { get; set; }
        public int GenreId { get; set; }
    }
}
