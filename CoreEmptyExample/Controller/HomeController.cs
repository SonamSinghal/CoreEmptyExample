using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample
{
    public class HomeController : Controller
    {

        private readonly BookModelRepo _repo;

        public HomeController()
        {
            _repo = new BookModelRepo();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }


        


    }
}
