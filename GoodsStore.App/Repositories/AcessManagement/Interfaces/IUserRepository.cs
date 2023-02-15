using GoodsStore.App.Models.AccessManagement;
using Microsoft.AspNetCore.Identity;

namespace GoodsStore.App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserLoged();
        Task<SignInResult> SignIn(string userName, string password,bool isPersistent);
        Task SignOut();
        Task<User> GetUserByMail(string email);
        Task<User> GetUserById(string id);
        Task<Dictionary<string, object>> AddUser(User user, string pwd);
        Task<IdentityResult> ResetPassword(User user, string token, string pwd);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        Task<string> GenerateNewToken(User user);
        Task<string> GenerateResetToken(User user);
        Task SendTokenByMail(User user, string confirmationLink);
        Task<IdentityResult> AddToRole(User user, string profile);
    }
}
