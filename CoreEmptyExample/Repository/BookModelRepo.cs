﻿using CoreEmptyExample.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Repository
{
    public class BookModelRepo
    {
        private readonly BookModelContext _connection;

        public BookModelRepo(BookModelContext context)
        {
            _connection = context;
        }


        public async Task<List<BookModel>> GetAllBooks()
        {
            var data = await _connection.BookModel.ToListAsync();
            return data;
        }

        public async Task<bool> InsertBook(BookModel model)
        {
            var book = new BookModel()
            {
                Name = model.Name,
                Author = model.Author,
                Description = model.Description,
                Pages = model.Pages,
                Quantity = model.Quantity,
                CreatedOn = DateTime.Now,
                BookUpdatedOn = DateTime.Now,
                QuantityUpdatedOn = DateTime.Now,
            };
            await _connection.BookModel.AddAsync(book);
            await _connection.SaveChangesAsync();
            return true;
        }

        //public BookModel SearchBook(int id)
        //{

        //    BookModel book;

        //    return book;
        //}

        public bool DeleteBook(int id)
        {
            var book = _connection.BookModel.FirstOrDefault(x => x.Id == id);
            _connection.BookModel.Remove(book);
            _connection.SaveChangesAsync();
            return true;
        }

        public async Task<BookModel> GetBook(int id)
        {
            var book = await _connection.BookModel.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<bool> UpdateBook(int id, BookModel model)
        {
            var book = _connection.BookModel.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                book.Name = model.Name;
                book.Author = model.Author;
                book.Description = model.Description;
                book.Pages = model.Pages;
                book.BookUpdatedOn = DateTime.Now;

                await _connection.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
