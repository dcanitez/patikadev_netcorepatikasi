using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            return vm;

            #region Mapping Öncesi
            //List<BooksViewModel> vm = new List<BooksViewModel>();
            // foreach (Book item in bookList)
            // {
            //     vm.Add(new BooksViewModel
            //     {
            //         Title = item.Title,
            //         Genre=((GenreEnum)item.GenreId).ToString(),
            //         PublishedDate=item.PublishedDate.Date.ToString("dd/MM/yyy"),
            //         PageCount=item.PageCount                    
            //     });
            // }  
            #endregion

        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishedDate { get; set; }
            public string Genre { get; set; }
        }
    }
}