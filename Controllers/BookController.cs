using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LibraryAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {

        private readonly BookContext _context;
        private Author author;
        private Genre genre;

        public BookController(BookContext context)
        {
            _context = context;

            if (_context.Books.Count() == 0)
            {
                _context.Books.Add(new Book { Title = "I Broke the Sky", Author = author, Genre = genre});
                _context.SaveChanges();
            }
        }

        // GET: api/book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _context.Books.ToList();
        }

        // GET api/book/{id}
        [HttpGet("{id}", Name = "GetBooks")]
        public IActionResult GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(t => t.ID == id);
            if (book == null) return NotFound();

            return new ObjectResult(book);
        }

        // POST api/book
        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            if (book == null) return BadRequest();

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtRoute("GetBooks", new { id = book.ID, }, book);
        }

        // PUT api/book/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, string title, Author author, Genre genre, [FromBody] Book book)
        {
            if (book == null) return BadRequest();

            var bookId = _context.Books.FirstOrDefault(t => t.ID == id);
            var authorName = _context.Books.FirstOrDefault(t => t.Author == author);
            var genreType = _context.Books.FirstOrDefault(t => t.Genre == genre);

            if (authorName == null || genreType == null) return NotFound();

            authorName.Author = author;
            genreType.Genre = genre;

            _context.Books.Update(authorName);
            _context.Books.Update(genreType);

            return new NoContentResult();
        }

        // DELETE api/book/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.First(t => t.ID == id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
