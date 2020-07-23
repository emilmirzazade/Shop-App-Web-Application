using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using ShopApp.WebUI.EmailSevices;
using ShopApp.WebUI.Extensions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private ICardService _cardService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager
            , IEmailSender emailSender, ICardService cardService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cardService = cardService;
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {

            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = await _userManager.FindByNameAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Username is not valid");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "PLease check your email for confirmation");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }
            ModelState.AddModelError("", "Username or password is not valid");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                // email
                
                await _emailSender.SendEmailAsync(model.Email, "Confirm your account.", $"Lutfen email hesabinizi onaylamak icin linke <a href='https://localhost:44337{url}'>tiklayiniz.</a>");
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Unknown error occured");
            return View(model);
        }
        public async Task<IActionResult> Logout(LoginModel model)
        {
            TempData.Put("message", new AlertMessage()
            {
                Title = "Account is logged out",
                Message = "Account is logged out",
                AlertType = "danger"
            });
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) 
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Invalid token",
                    Message="Invalid token",
                    AlertType="danger"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cardService.InitializeCard(user.Id);
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Your account is confirmed",
                        Message = "Your account is confirmed",
                        AlertType = "success"
                    });
                    return View();
                }
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "Your account is not confirmed",
                Message = "Your account is not confirmed",
                AlertType = "warning"
            });
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user==null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //generate token
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            // email

            await _emailSender.SendEmailAsync(Email, "Reset Password.", $"Click the <a href='https://localhost:44337{url}'>link for reseting your password.</a>");
            
            return View();
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId==null || token==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordModel { Token = token };

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user==null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}