using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample
{
    public class BookController : Controller
    {
        private readonly BookModelRepo _repo;

        public BookController()
        {
            _repo = new BookModelRepo();
        }

        public ActionResult GetAllBooks()
        {
            var data = _repo.GetAllBooks();
            
            return View(data);
        }
    }
}
