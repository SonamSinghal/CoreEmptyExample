using CoreEmptyExample.Model;
using System;
using System.Collections.Generic;

namespace CoreEmptyExample.Repository
{
    public interface IBookModelRepo
    {
        bool DeleteBook(Guid id);
        List<BookModel> GetAllBooks();
        BookModel GetBook(Guid id);
        void InsertBook(BookModel model);
        bool UpdateBook(Guid id, BookModel model);
    }
}