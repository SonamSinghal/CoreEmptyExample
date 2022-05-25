using CoreEmptyExample.Model;
using CoreEmptyExample.Service;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public AccountRepo(UserManager<UserModel> usermanager, 
            SignInManager<UserModel> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
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

            return data;

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
    }
}
