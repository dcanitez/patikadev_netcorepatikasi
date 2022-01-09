using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext=context;
            _mapper=mapper;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(b => b.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Belirttiğiniz kitap bulunamadı");
            BookDetailViewModel vm=_mapper.Map<BookDetailViewModel>(book);
            return vm;
            
            #region Mapping Öncesi
               //new BookDetailViewModel{
            //     Title=book.Title,
            //     Genre=((GenreEnum)book.GenreId).ToString(),
            //     PublishedDate=book.PublishedDate.Date.ToString("dd/MM/yyy"),
            //     PageCount=book.PageCount
            // };  
            #endregion
            
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishedDate { get; set; }
            public string Genre { get; set; }
        }
    }
}