using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Odev1
{
    public class DeleteBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public DeleteBookQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                throw new Exception("Belirttiğiniz kitap bulunamadı");
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}

