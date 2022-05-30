using CoreEmptyExample.Model;
using CoreEmptyExample.Repository;
using CoreEmptyExample.Service;
using Microsoft.AspNetCore.Authorization;
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
                    return RedirectToAction(nameof(ConfirmationEmail), new { email = user.Email });
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


                else if (success.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Please Verify your Email and Try Again!");
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


        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmationEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(" ", "+");
                var result = await _accountRepo.ConfirmEmailVerification(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
                return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmationEmail(EmailConfirmModel model)
        {
            var user = await _accountRepo.FindUserByEmail(model.Email);

            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.IsConfirmed = true;
                    return View(model);
                }
                
                await _accountRepo.TokenGeneratoionAndEmailVerification(user);
                model.EmailSent = true;
                ModelState.Clear();                
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }
    }
}
