﻿using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Odev1
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                throw new InvalidOperationException("Belirttiğiniz kitap bulunmamaktadır.");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.GenreId;
            book.PublishedDate = Model.PublishedDate != default ? Model.PublishedDate : book.PublishedDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishedDate { get; set; }
        }
    }
}
