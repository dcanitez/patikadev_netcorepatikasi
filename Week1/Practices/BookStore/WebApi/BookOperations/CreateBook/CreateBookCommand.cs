using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcuttur");
            book = _mapper.Map<Book>(Model);
            _dbContext.Add(book);
            _dbContext.SaveChanges();

            #region Mapping yapmadan öncesi
            //new Book();
            // book.Title=Model.Title;
            // book.PublishedDate=Model.PublishedDate;
            // book.PageCount=Model.PageCount;
            // book.GenreId=Model.GenreId;
            #endregion
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishedDate { get; set; }
        }
    }
}