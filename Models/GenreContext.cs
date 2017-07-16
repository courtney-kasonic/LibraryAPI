using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models
{
    public class GenreContext : DbContext
    {
        public GenreContext(DbContextOptions<GenreContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
    }
}
