using CoreEmptyExample.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample
{
    public class BookModelContext : DbContext
    {

        public BookModelContext(DbContextOptions<BookModelContext> options) : base()
        {

        }

        public DbSet<BookModel> BookModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //WE CAN EITHER PASS OUR CONNECTION STRING HERE OR IN THE STARTUP CLASS UNDER SERVIECES
            //IF WE DEFINE CONNECTION STRING IN STARTUP CLASS, THIS METHOD IS NOT NEEDED
            optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
    
}
