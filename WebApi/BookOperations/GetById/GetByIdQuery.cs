using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.GetById
{
    public class GetByIdQuery
    {
        public BooksViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.BookId == BookId).SingleOrDefault();
            //book = new Book();
            BooksViewModel vm = new BooksViewModel();

            vm.BookId = book.BookId;
            vm.BookTitle = book.BookTitle;
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM//yyyy");
            vm.PageCount = book.PageCount;


            return vm;
        }
    }

    public class BooksViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
