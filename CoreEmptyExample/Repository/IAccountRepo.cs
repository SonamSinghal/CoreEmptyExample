using CoreEmptyExample.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreEmptyExample.Repository
{
    public interface IAccountRepo
    {
        Task<IdentityResult> CreateUser(SignUpUserModel user);
        Task<SignInResult> LogIn(LoginModel user);
        Task LogOut();
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);

    }
}