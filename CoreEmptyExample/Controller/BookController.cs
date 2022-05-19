using CoreEmptyExample.Model;
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

        public BookController(BookModelRepo repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> GetAllBooks(bool? createMsg=false, bool? deleteMsg=false, bool? updateMsg=false)
        {
            ViewBag.CreateMsg = createMsg;
            ViewBag.DeleteMsg = deleteMsg;
            ViewBag.UpdateMsg = updateMsg;

            List<BookModel> data = await _repo.GetAllBooks();
            
            return View(data);
        }

        public ActionResult InsertBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertBook(BookModel book)
        {
            
            if (ModelState.IsValid)
            {
                var success = await _repo.InsertBook(book);
                if (success)
                {
                    return RedirectToAction(nameof(GetAllBooks), new { createMsg = true });
                }
            }

            ModelState.AddModelError("", "Invalid Input!!");
            return View();
        }

        public ActionResult DeleteBook(int id)
        {
            var success = _repo.DeleteBook(id);
            if (success)
            {
                return RedirectToAction(nameof(GetAllBooks), new { deleteMsg = true });
            }
            return Redirect("GetAllBooks");
        }

        public async Task<ActionResult> UpdateBook(int id)
        {
            var book = await _repo.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBook(int id, BookModel book)
        {
            
                var success = await _repo.UpdateBook(id, book);
                if (success)
                {
                    return RedirectToAction(nameof(GetAllBooks), new { updateMsg = true });
                }
            

            ModelState.AddModelError("", "Invalid Inputs!");
            return View();
        }
    }
}
