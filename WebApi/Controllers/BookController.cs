using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.CreateBook;
using WebApi.UpdateBook;
using WebApi.GetById;
using BooksViewModel = WebApi.GetById.BooksViewModel;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]/s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks ()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById ()
        {
            GetByIdQuery query = new GetByIdQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        // [HttpGet]
        // public Book Get ([FromQuery]string id)
        // { 
        //     var book = BookList.Where(x => x.BookId == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody]CreateBookModel newBook){
            try
            {
                CreateBookCommand command = new CreateBookCommand(_context);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody]UpdateBookModel updatedBook){

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.BookId == id);
            if (book != null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}