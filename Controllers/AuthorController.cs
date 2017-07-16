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
    public class AuthorController : Controller
    {

        private readonly AuthorContext _context;

        public AuthorController(AuthorContext context)
        {
            _context = context;

            if (_context.Authors.Count() == 0)
            {
                _context.Authors.Add(new Author { firstName = "FirstName", lastName = "LastName" });
                _context.SaveChanges();
            }
        }

        
        // GET: api/author
        [HttpGet]
        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        // GET api/author/{id}
        [HttpGet("{id}", Name = "GetAuthors")]
        public IActionResult GetById(int id)
        {
            var author = _context.Authors.FirstOrDefault(t => t.ID == id);
            if (author == null) return NotFound();

            return new ObjectResult(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Author author)
        {
            if (author == null) return BadRequest();

            _context.Authors.Add(author);
            _context.SaveChanges();

            return CreatedAtRoute("GetAuthors", new { id = author.ID }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, string first, string last, [FromBody] Author author)
        {
            if (author == null || author.ID != id) return BadRequest();

            var authorId = _context.Authors.FirstOrDefault(t => t.ID == id);
            var authorFirst = _context.Authors.FirstOrDefault(t => t.firstName == first);
            var authorLast = _context.Authors.FirstOrDefault(t => t.lastName == last);

            if (authorFirst == null || authorLast == null) return NotFound();

            authorFirst.firstName = author.firstName;
            authorLast.lastName = author.lastName;

            _context.Authors.Update(authorFirst);
            _context.Authors.Update(authorLast);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = _context.Authors.First(t => t.ID == id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
