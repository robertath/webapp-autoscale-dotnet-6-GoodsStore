using AutoMapper;
using EmailService;
using GoodsStore.App.Infra;
using GoodsStore.App.Models.AccessManagement;
using Microsoft.AspNetCore.Identity;

namespace GoodsStore.App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;


        public UserRepository(DBContext context, 
                                IHttpContextAccessor contextAccessor, 
                                IMapper mapper, 
                                UserManager<User> userManager, 
                                SignInManager<User> signInManager, 
                                IEmailSender emailSender) 
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<SignInResult> SignIn(string userName, string password, bool isPersistent)
        {
            var user = await GetUserByMail(userName);
            
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, false);
            
            if (user != null && result.Succeeded)
                SetUserId(user.Id);

            return result;
        }

        public async Task SignOut()
        {
            await ForceLogout();            
        }

        public async Task<User> GetUserLoged()
        {
            var userId = GetUserId();
            if (userId == null) {
                await ForceLogout();
                return null;
            }               
            
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task ForceLogout()
        {
            await _signInManager.SignOutAsync();
            _contextAccessor.HttpContext?.Session.Clear();            
        }

        public async Task<User> GetUserByMail(string email)
        {            
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<Dictionary<string, object>> AddUser(User user, string pwd)
        {
            //var result = await _userManager.CreateAsync(user, userModel.Password);
            var result = await _userManager.CreateAsync(user, pwd);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //Object[] results = new object[] { result, "token" };
            var results = new Dictionary<string, object>()
            {
                { "IdentityResult", result},
                { "Token", token},
            };

            return results;
        }

        public async Task<IdentityResult> ResetPassword(User user, string token, string pwd)
        {
            return await _userManager.ResetPasswordAsync(user, token, pwd);
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string token)
        {               
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task SendTokenByMail(User user, string confirmationLink)
        {
            try
            {
                var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                await _emailSender.SendEmailAsync(message);
            }
            catch (Exception)
            {
                throw new Exception("E-mail could not be sent due some infraestructure problems, please try again later.");
            }
            
        }

        public async Task<string> GenerateNewToken(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GenerateResetToken(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> AddToRole(User user, string profile)
        {
            return await _userManager.AddToRoleAsync(user, "Visitor");
        }

        #region Private Methods
        private string? GetUserId()
        {
            return _contextAccessor.HttpContext?.Session.GetString("UserId");
        }

        private void SetUserId(string id)
        {
            _contextAccessor.HttpContext?.Session.SetString("UserId", id);
        }       
        #endregion
    }
}
