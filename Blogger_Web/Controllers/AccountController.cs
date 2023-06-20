using Blogger_Common;
using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Models.AccountViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogger_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BloggerDbContext _bloggerDbContext;
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager, BloggerDbContext bloggerDbContext)
        {
            _userManager = userManager;
            _bloggerDbContext = bloggerDbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    // kiểm tra password nhập vào khớp với password lưu ở database
                    var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                    if (checkPasswordResult)
                    {
                        HttpContext.Session.SetString("userName", user.UserName);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userName");

            return RedirectToAction("Index", "Home"); ;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(registerViewModel);
                }

                if (registerViewModel.Password != registerViewModel.RePassword)
                {
                    ModelState.AddModelError("rePassword", "Mật khẩu và nhập lại mật khẩu không khớp");
                    return View(registerViewModel);
                }

                var user = new User
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true,
                    FullName = registerViewModel.FullName,
                    SendEmail = false,
                };

                var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, new string[] { "User" });
                }

                EmailService.SendMail("Blog IT", "Đăng ký tài khoản Blog IT", "Chúc mừng bạn đăng ký thành công tài khoản tại Blog IT", user.Email);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
            }

            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            var userName = HttpContext.Session.GetString("userName");

            var user = await _userManager.FindByEmailAsync(userName);

            user.SendEmail = true;
            await _bloggerDbContext.SaveChangesAsync();   

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
