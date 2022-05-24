using CoreEmptyExample.Model;
using Microsoft.AspNetCore.Identity;
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

        public AccountRepo(UserManager<UserModel> usermanager, SignInManager<UserModel> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
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

            return await _usermanager.CreateAsync(newUser, user.Password);
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
    }
}
