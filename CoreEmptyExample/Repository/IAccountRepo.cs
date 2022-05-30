using CoreEmptyExample.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreEmptyExample.Repository
{
    public interface IAccountRepo
    {
        Task<UserModel> FindUserByEmail(string email);
        Task<IdentityResult> CreateUser(SignUpUserModel user);
        Task TokenGeneratoionAndEmailVerification(UserModel model);
        Task<SignInResult> LogIn(LoginModel user);
        Task LogOut();
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailVerification(string uid, string token);

    }
}