using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CoreEmptyExample
{
    public class HomeController : Controller
    {

        private readonly IBookModelRepo _repo;
        private readonly IConfiguration _configuration;

        public HomeController(IBookModelRepo repo, IConfiguration configuration)
        {

            _repo = repo;
            _configuration = configuration;
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
