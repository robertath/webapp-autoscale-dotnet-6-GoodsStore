using AutoMapper;
using EmailService;
using GoodsStore.App.Models;
using GoodsStore.App.Models.AccessManagement;
using GoodsStore.App.Models.AccessManagement.ViewModels;
using GoodsStore.App.Models.AcessManagement.ViewModels;
using GoodsStore.App.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static GoodsStore.App.Models.AccessManagement.ViewModels.Helper;

namespace GoodsStore.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        [NoDirectAccess]
        public IActionResult Login()
        {
            SetReturntUrl("Order", "Carousel");
            _logger.LogInformation(1, "User logged in.");
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            var result = await _userRepository.SignIn(userModel.Email, userModel.Password, userModel.RememberMe);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login: Username or Password not valid.");
                SetReturntUrl("Order", "Carousel");
                _logger.LogInformation(10, "Failed to logged in.");
                return View(userModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.SignOut();            

            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(OrderController.Carousel), "Order");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerRegistrationViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = _mapper.Map<User>(userModel);

            var process = await _userRepository.AddUser(user, userModel.Password);
            IdentityResult result = (IdentityResult) process["IdentityResult"];
            var token = (string) process["Token"];

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(userModel);
            }
            
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            await _userRepository.SendTokenByMail(user, confirmationLink);

            await _userRepository.AddToRole(user, userModel.Profile);

            _logger.LogInformation(5, "User added.");
            return RedirectToAction(nameof(SuccessRegistration));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userRepository.GetUserByMail(email);
            if (user == null)
                return View("Error");

            var result = await _userRepository.ConfirmEmail(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);

            var user = await _userRepository.GetUserByMail(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            else
            {
                var token = await _userRepository.GenerateResetToken(user);
                var confirmationLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                                
                await _userRepository.SendTokenByMail(user, confirmationLink);

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }            
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userRepository.GetUserByMail(resetPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            else
            {
                var resetPassResult = await _userRepository.ResetPassword(user, resetPasswordModel.Token, resetPasswordModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return View();
                }
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Private Methods
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(OrderController.Carousel), "Order");

        }

        private void SetReturntUrl(string controllerTo, string actionTo)
        {
            var url = $"{Request.Scheme}//{Request.Host}/{controllerTo}/{actionTo}";
            ViewData["ReturnUrl"] = url;
        }
        #endregion Private Methods
    }
}
