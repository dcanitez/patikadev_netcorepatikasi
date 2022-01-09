using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetails;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetails.GetBookByIdQuery;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //DataGenerator içerisinde InMemory Database e bu listeyi Initialize metodu ile attık.
        #region Static BookList
        // private static List<Book> BookList=new List<Book>()
        // {
        //     new Book{
        //         Id=1,
        //         Title="Lean Startup",
        //         GenreId=1, //PersonalGrowth
        //         PageCount=200,
        //         PublishedDate=new DateTime(2001,06,12)
        //     },
        //     new Book{
        //         Id=2,
        //         Title="Herland",
        //         GenreId=2, //Science Fiction
        //         PageCount=250,
        //         PublishedDate=new DateTime(2010,05,23)
        //     },
        //     new Book{
        //         Id=3,
        //         Title="Dune",
        //         GenreId=2, //Science Fiction
        //         PageCount=540,
        //         PublishedDate=new DateTime(2001,12,21)
        //     }

        // };
        #endregion
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            try
            {
                query.BookId = id;
                GetBookDetailsQueryValidator validate=new GetBookDetailsQueryValidator();
                validate.ValidateAndThrow(query);
                BookDetailViewModel model = query.Handle();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Parametresiz HttpGet bir defa kullanılır!! FromQuery 
        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book=BookList.Where(b=>b.Id==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
        #endregion

        //POST
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            #region Before CreateBookCommand.cs
            //CRUD işlemlerde yapılması gereken validations 
            // Gelen book elimizdeki bookListte mevcut mu kontrolü yapılmalı!
            // var book=_context.Books.SingleOrDefault(b=>b.Title==newBook.Title);
            // if(book is not null)
            //     return BadRequest();

            // _context.Books.Add(newBook);
            // _context.SaveChanges();
            // return Ok();   
            #endregion
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                // ValidationResult result = validator.Validate(command);
                // if (!result.IsValid)                
                //     foreach (ValidationFailure item in result.Errors)                    
                //         throw new Exception("Özellik "+ item.PropertyName+"- Error Message: "+ item.ErrorMessage);
                // else  
                //     command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();                
        }

        //PUT

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            #region Before UpdateBookCommand.cs
            //gelen id li book listemde var mı kontrolü yapmalıyım.
            // var book = _context.Books.SingleOrDefault(b => b.Id == id);
            // if (book is null)
            //     return BadRequest();
            // //güncelleme yapacağız.
            // //gelen veri doldurulmuş ya da değiştirilmiş bir değer mi? kontrol etmeliyim.
            // book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            // book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.GenreId;
            // book.PublishedDate = updatedBook.PublishedDate != default ? updatedBook.PublishedDate : book.PublishedDate;
            // book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            // _context.SaveChanges();
            // return Ok();
            #endregion
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
                command.BookId = id;
                UpdateBookCommandValidator validate=new UpdateBookCommandValidator();
                validate.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            #region Before DeleteBookQuery.cs
            //id validation yapılması gerekiyor.
            // var book = _context.Books.SingleOrDefault(b => b.Id == id);
            // if (book is null)
            //     return BadRequest();

            // _context.Books.Remove(book);
            // _context.SaveChanges();
            // return Ok();  
            #endregion
            
            DeleteBookQuery query = new DeleteBookQuery(_context);
            try
            {
                query.BookId = id;
                DeleteBookQueryValidator validator=new DeleteBookQueryValidator();
                validator.ValidateAndThrow(query);
                query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok();
        }

    }
}