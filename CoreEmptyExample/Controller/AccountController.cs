using CoreEmptyExample.Model;
using CoreEmptyExample.Repository;
using CoreEmptyExample.Service;
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
        private readonly IUserService _userService;

        public AccountController(IAccountRepo accountRepo, IUserService userService)
        {
            _accountRepo = accountRepo;
            _userService = userService;
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
                    return RedirectToAction("LogIn");
                }
            }
            return View();
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginModel user, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var success = await _accountRepo.LogIn(user);
                if (success.Succeeded)
                {
                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }

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

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var success = await _accountRepo.ChangePassword(changePassword);
                if (success.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in success.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }

            return View();
        }

    }
}
