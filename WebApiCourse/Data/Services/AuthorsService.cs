using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCourse.Data.Models;
using WebApiCourse.Data.ViewModels;

namespace WebApiCourse.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthors(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName

            };

            _context.Authors.Add(_author);
            _context.SaveChanges();

        }

    }
}
