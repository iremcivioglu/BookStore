﻿using System;
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
        public GetByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.BookId).ToList();
            //book = new Book();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    BookId = book.BookId,
                    BookTitle = book.BookTitle,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM//yyyy"),
                    PageCount = book.PageCount,
                });
            }
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
