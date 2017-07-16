using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Book> Books { get; set; }
    }
}
