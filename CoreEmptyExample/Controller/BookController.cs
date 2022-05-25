using CoreEmptyExample.Model;
using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample
{
    public class BookController : Controller
    {
        private readonly IBookModelRepo _repo;
        private readonly IWebHostEnvironment _env;


        public BookController(IBookModelRepo repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [Authorize]
        public ActionResult GetAllBooks(bool? createMsg=false, bool? deleteMsg=false, bool? updateMsg=false)
        {
            ViewBag.CreateMsg = createMsg;
            ViewBag.DeleteMsg = deleteMsg;
            ViewBag.UpdateMsg = updateMsg;

            List<BookModel> data = _repo.GetAllBooks();
            
            return View(data);
        }

        [Authorize]
        public ActionResult InsertBook()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult InsertBook(BookModel book)
            {
            
            if (ModelState.IsValid)
            {
                if (book.CoverPhoto != null)
                {
                    string Folder = "BookImages/cover";
                    Folder += Guid.NewGuid().ToString() + book.CoverPhoto.FileName;
                    string ServerFolder = Path.Combine(_env.WebRootPath, Folder);
                    book.CoverImageUrl = "/" + Folder;
                    book.CoverPhoto.CopyTo(new FileStream(ServerFolder, FileMode.Create));
                }
                _repo.InsertBook(book);

                //AJAX_UNOBTRUSIVE WAS GIVNG A PROBLEM SO...
                //return RedirectToAction(nameof(GetAllBooks)), new { createMsg = true });
                return base.Json(new { redirectToUrl = Url.Action("GetAllBooks", "Book")+ "?createMsg=true" });
            }
                //ModelState.AddModelError("", "Invalid Input!!");
                return View(nameof(InsertBook));
        }

        public ActionResult DeleteBook(Guid id)
        {
            var success = _repo.DeleteBook(id);
            if (success)
            {
                return RedirectToAction(nameof(GetAllBooks), new { deleteMsg = true });
            }
            return Redirect("GetAllBooks");
        }

        public ActionResult UpdateBook(Guid id)
        {
            var book = _repo.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult UpdateBook(Guid id, BookModel book)
        {
            if (ModelState.IsValid)
            {
                var success = _repo.UpdateBook(id, book);
                if(success)
                    return base.Json(new { redirectToUrl = Url.Action("GetAllBooks", "Book") + "?updateMsg=true" });
            }

            ModelState.AddModelError("", "Invalid Inputs!");
            return View();
        }
    }
}
