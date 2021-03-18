using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCourse.Data.Models
{
    public class Book_Author
    {
        // Model M - N Relation
        public int Id { get; set; }

        // Navigation Properties
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
