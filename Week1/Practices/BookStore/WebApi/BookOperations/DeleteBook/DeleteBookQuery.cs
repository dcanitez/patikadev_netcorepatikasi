using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
                throw new Exception("Belirttiğiniz kitap bulunamadı");
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}