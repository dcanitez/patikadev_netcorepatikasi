using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Odev1
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public BookDetailViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(b => b.Id == id).SingleOrDefault();
            if (book is null)
                throw new Exception("Belirttiğiniz kitap bulunamadı");
            BookDetailViewModel vm = new BookDetailViewModel
            {
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishedDate = book.PublishedDate.Date.ToString("dd/MM/yyy"),
                PageCount = book.PageCount
            };
            return vm;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishedDate { get; set; }
        public string Genre { get; set; }
    }
}
