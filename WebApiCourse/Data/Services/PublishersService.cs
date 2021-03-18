using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCourse.Data.Models;
using WebApiCourse.Data.ViewModels;

namespace WebApiCourse.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name

            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        public PublisherWithBooksandAuthors GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(p => p.Id == publisherId).Select(n =>
            new PublisherWithBooksandAuthors
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(b => new BookAuthorVM()
                {
                    BookName = b.Title,
                    BookAuthors = b.Book_Authors.Select(a => a.Author.FullName).ToList()
                }).ToList()

            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }
    }
}
