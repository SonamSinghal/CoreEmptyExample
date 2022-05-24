using CoreEmptyExample.Model;
using CoreEmptyExample.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel user)
        {
             if (ModelState.IsValid)
            {
                var result = await _accountRepo.CreateUser(user);
                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.Clear();
                }
            }
            return View();
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var success = await _accountRepo.LogIn(user);
                if (success.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials!!");
                }
            }
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await _accountRepo.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
