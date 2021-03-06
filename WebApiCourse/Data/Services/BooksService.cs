using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCourse.Data.Models;
using WebApiCourse.Data.ViewModels;

namespace WebApiCourse.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }


        public void AddBookWithAuthors(BookVM book)
        {
            // Add book
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            // Save Books_Authors
            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }

        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book =>
                                    new BookWithAuthorsVM
                                    {
                                        Title = book.Title,
                                        Description = book.Description,
                                        IsRead = book.IsRead,
                                        DateRead = book.IsRead ? book.DateRead.Value : null,
                                        Rate = book.Rate,
                                        Genre = book.Genre,
                                        CoverUrl = book.CoverUrl,
                                        PublisherName = book.Publisher.Name,
                                        AuthorNames = book.Book_Authors.Select(a => a.Author.FullName).ToList()
                                    }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookWithAuthorsVM book)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.Rate;
                _book.Genre = book.Genre;
               // _book.Author = book.Author;
                _book.CoverUrl = book.CoverUrl;
            }

            _context.SaveChanges();

            return _book;

        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
