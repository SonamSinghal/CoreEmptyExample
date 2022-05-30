using CoreEmptyExample.Model;
using CoreEmptyExample.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<UserModel> _usermanager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepo(UserManager<UserModel> usermanager, 
            SignInManager<UserModel> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<UserModel> FindUserByEmail(string email)
        {
            return await _usermanager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUser(SignUpUserModel user)
        {
            var newUser = new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Phone = user.Phone,
                Email = user.Email,
                UserName = user.Email,
            };

            var data = await _usermanager.CreateAsync(newUser, user.Password);
            //await _usermanager.AddToRoleAsync(newUser, "Admin");

            if (data.Succeeded)
            {
                await TokenGeneratoionAndEmailVerification(newUser);
            }
            return data;
        }


        public async Task TokenGeneratoionAndEmailVerification(UserModel model)
        {
            var token = await _usermanager.GenerateEmailConfirmationTokenAsync(model);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmation(model, token);
            }
        }

        public async Task<SignInResult> LogIn(LoginModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
            return result;
        }

        public async Task  LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var id = _userService.GetUserId();
            var user = await _usermanager.FindByIdAsync(id);
            return await _usermanager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailVerification(string uid, string token)
        {
            var user = await _usermanager.FindByIdAsync(uid);
            return await _usermanager.ConfirmEmailAsync(user, token);
        }



        private async Task SendEmailConfirmation(UserModel user, string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var conformationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            SendEmailModel model = new SendEmailModel()
            {
                SendTo = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}", user.FirstName+" "+user.LastName),
                    new KeyValuePair<string, string>("{{Link}}", String.Format(appDomain+conformationLink, user.Id, token)),
                }
            };

            await _emailService.SendConfirmationEmailService (model);
        }


    }
}
