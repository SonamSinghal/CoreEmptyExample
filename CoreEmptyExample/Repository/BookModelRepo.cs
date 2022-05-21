using CoreEmptyExample.Model;
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


        public List<BookModel> GetAllBooks()
        {
            var data = _connection.BookModel.ToList();
            return data;
        }

        public void InsertBook(BookModel model)
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
                //CoverImageUrl = model.CoverImageUrl
            };

             _connection.BookModel.Add(book);
             _connection.SaveChanges();
        }

        //public BookModel SearchBook(int id)
        //{

        //    BookModel book;

        //    return book;
        //}

        public bool DeleteBook(Guid id)
        {
            var book = _connection.BookModel.FirstOrDefault(x => x.Id.ToString() == id.ToString());
            _connection.BookModel.Remove(book);
            _connection.SaveChanges();
            return true;
        }

        public async Task<BookModel> GetBook(Guid id)
        {
            var book = await _connection.BookModel.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
            return book;
        }

        public async Task<bool> UpdateBook(Guid id, BookModel model)
        {
            var book = _connection.BookModel.FirstOrDefault(x => x.Id.ToString() == id.ToString());
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
