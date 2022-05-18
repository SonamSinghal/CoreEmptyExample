using CoreEmptyExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Repository
{
    public class BookModelRepo
    {

        static readonly BookModel[] Books =
        {
            
        };

        //private readonly BookModelContext _connection;



        public List<BookModel> GetAllBooks()
        {
            return DataSource();
            //return _connection.BookModel.ToList();
        }

        //public void InsertBook(BookModel book)
        //{

        //}

        //public BookModel SearchBook(int id)
        //{

        //    BookModel book;

        //    return book;
        //}

        //public void DeleteBook(int id)
        //{

        //}

        //public BookModel UpdateBook(int id)
        //{

        //}
        

        private List<BookModel> DataSource()
        {
            return new List<BookModel>
            {
                new BookModel {Id=1, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=2, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=3, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=4, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=5, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=6, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=7, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=8, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=9, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            new BookModel {Id=10, Name="MVC", Author="Sonam", Description="This is Description", Pages=100, Quantity=100 },
            };
        }

    }
}
