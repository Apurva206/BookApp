using BookApp.DTO;
using BookApp.Model;
using BookApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookController : ControllerBase
    {
        private readonly IBookServices _repository;

        public BookController(IBookServices repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _repository.GetAllBooksAsync();
            return Ok(books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author ,
                ISBN = b.ISBN ,
                PublicationYear = b.PublicationYear 
            }));
        }

        [HttpGet("{isbn}")]
        //[HttpGet]
        public async Task<IActionResult> GetBookByISBN(string isbn)
        {
            var book = await _repository.GetBookByISBNAsync(isbn);
            if (book == null) return NotFound();

            return Ok(new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var book = new Book
            { 
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                PublicationYear = bookDto.PublicationYear
            };
            await _repository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookByISBN), new { isbn = book.ISBN }, bookDto);
        }

        [HttpPut("{isbn}")]
        //[HttpPut]
        public async Task<IActionResult> UpdateBook(string isbn, [FromBody] BookDto bookDto)
        {
            var book = await _repository.GetBookByISBNAsync(isbn);
            if (book == null) return NotFound();
            //book.Id = bookDto.Id;
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.PublicationYear = bookDto.PublicationYear;

            await _repository.UpdateBookAsync(book);
            return NoContent();
        }
        [Route("DeleteBook")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook([FromForm]string isbn)
        {
            await _repository.DeleteBookAsync(isbn);
            return Ok();
        }

        
    }
}
