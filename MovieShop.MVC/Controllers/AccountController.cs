using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // show a view with empty text boxes for name, dob, email. password

            return View();
        }

        //[HttpGet]
        //public IActionResult Profile()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //save to database
                var user = await _userService.RegisterUser(model);
                // redirect to Login 
            }
            // take name, dob, email, password from view and save it to database
            return View();
        }

        public async Task<IActionResult> Login()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            var user = await _userService.Login(model.Email, model.Password);
            if (user == null)
            {
                return View();
            }

            // ret
            // user entered his correct un/pw
            // we will create a cookie, movieshopauthcookie =>FirstName, LastName, id, Email, expiration time , claims
            // Cookie based Authentication.
            // 2 hours
            // 

            //create claims object and store required information
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,user.FirstName ),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // HttpContext => 
            // method type => get/post
            // Url
            // browsers
            // headers
            // form
            // cookies

            // create an identity object

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create a cookie that stores the identity information

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect("~/");
        }
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login");
            }




        }
    
}