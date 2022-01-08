using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetails;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetails.GetBookByIdQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Odev3
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
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
                GetBookDetailsQueryValidator validate = new GetBookDetailsQueryValidator();
                validate.ValidateAndThrow(query);
                BookDetailViewModel model = query.Handle();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

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

            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
                command.BookId = id;
                UpdateBookCommandValidator validate = new UpdateBookCommandValidator();
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

            DeleteBookQuery query = new DeleteBookQuery(_context);
            try
            {
                query.BookId = id;
                DeleteBookQueryValidator validator = new DeleteBookQueryValidator();
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