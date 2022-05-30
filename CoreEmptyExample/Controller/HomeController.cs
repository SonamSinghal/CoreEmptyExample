using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using CoreEmptyExample.Service;
using CoreEmptyExample.Model;

namespace CoreEmptyExample
{
    public class HomeController : Controller
    {

        private readonly IBookModelRepo _repo;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public HomeController(IBookModelRepo repo, 
            IConfiguration configuration,
            IEmailService emailService)
        {

            _repo = repo;
            _configuration = configuration;
            _emailService = emailService;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        [Authorize]
        public ActionResult ContactUs()
        {
            return View();
        }


        


    }
}
