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
    public class GenreController : Controller
    {

        private readonly GenreContext _context;

        public GenreController(GenreContext context)
        {
            _context = context;

            if (_context.Genres.Count() == 0)
            {
                _context.Genres.Add(new Genre { Name = "fantasy"});
                _context.SaveChanges();
            }
        }

        // GET: api/genre
        [HttpGet]
        public IEnumerable<Genre> Get()
        {
            return _context.Genres.ToList();
        }

        // GET api/genre/{id}
        [HttpGet("{id}", Name = "GetGenre")]
        public IActionResult GetById(int id)
        {
            var genre = _context.Genres.FirstOrDefault(t => t.ID == id);
            if (genre == null) return NotFound();

            return new ObjectResult(genre);
        }

        // POST api/genre
        [HttpPost]
        public IActionResult Create([FromBody] Genre genre)
        {
            if (genre == null) return BadRequest();

            _context.Genres.Add(genre);
            _context.SaveChanges();

            return CreatedAtRoute("GetGenres", new { id = genre.ID, }, genre);
        }

        // PUT api/genre/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, string title, [FromBody] Genre genre)
        {
            if (genre == null) return BadRequest();

            var genreId = _context.Genres.FirstOrDefault(t => t.ID == id);
            var genreName = _context.Genres.FirstOrDefault(t => t.Name == title);

            if (genreName == null) return NotFound();

            genreName.Name = genre.Name;

            _context.Genres.Update(genreName);

            return new NoContentResult();
        }

        // DELETE api/genre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genre = _context.Genres.First(t => t.ID == id);
            if (genre == null) return NotFound();

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
