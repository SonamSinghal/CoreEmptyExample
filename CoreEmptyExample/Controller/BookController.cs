﻿using CoreEmptyExample.Model;
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
        private readonly BookModelRepo _repo;
        private readonly IWebHostEnvironment _env;


        public BookController(BookModelRepo repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public ActionResult GetAllBooks(bool? createMsg=false, bool? deleteMsg=false, bool? updateMsg=false)
        {
            ViewBag.CreateMsg = createMsg;
            ViewBag.DeleteMsg = deleteMsg;
            ViewBag.UpdateMsg = updateMsg;

            List<BookModel> data = _repo.GetAllBooks();
            
            return View(data);
        }

        public ActionResult InsertBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertBook(BookModel book)
            {
            
            if (ModelState.IsValid)
            {
                //if (book.CoverPhoto != null)
                //{
                //    string Folder = "BookImages/cover";
                //    Folder += Guid.NewGuid().ToString() + book.CoverPhoto.FileName;
                //    string ServerFolder = Path.Combine(_env.WebRootPath, Folder);
                //    book.CoverImageUrl = "/"+Folder;
                //    book.CoverPhoto.CopyTo(new FileStream(ServerFolder, FileMode.Create));
                //}
                _repo.InsertBook(book);
                return RedirectToAction(nameof(GetAllBooks))/* new { createMsg = true })*/;
            }

                ModelState.AddModelError("", "Invalid Input!!");
                return View();
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

        public async Task<ActionResult> UpdateBook(Guid id)
        {
            var book = await _repo.GetBook(id);
            return View(book);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBook(Guid id, BookModel book)
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
