﻿using Blogger_Web.Models.AccountViewModel;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public UsersController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel registerVM)
        //{
        //    var identityUser = new IdentityUser
        //    {
        //        UserName = registerVM.Username,
        //        Email = registerVM.Username,
        //    };

        //    var identityResult = await _userManager.CreateAsync(identityUser, registerVM.Password);

        //    if (identityResult.Succeeded)
        //    {
        //        //add roles to this user
        //        if (registerVM.Roles != null && registerVM.Roles.Any())
        //        {

        //            identityResult = await _userManager.AddToRolesAsync(identityUser, registerVM.Roles);
        //        }

        //        if (identityResult.Succeeded)
        //        {
        //            return Ok("Register successfull");
        //        }
        //    }

        //    return BadRequest("Something wrong");

        //}

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                // kiểm tra password nhập vào khớp với password lưu ở database

                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (checkPasswordResult)
                {
                    //get roles for this user – lấy quyền của user từ database

                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token – tạo token cho user này
                        var jwtToken = _tokenRepository.CreateJWTToken(
                            user, roles.ToList());

                        //var response = new LoginResponseDTO
                        //{
                        //    JwtToken = jwtToken
                        //};

                        return Ok(jwtToken);
                    }
                }
            }

            return BadRequest();
        }
    }
}
