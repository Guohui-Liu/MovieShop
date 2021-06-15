using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                // save to db, register user
                var createdUser = await _userService.RegisterUser(model);
                // 201 Created
                return Ok(createdUser);
            }
            // 400
            return BadRequest("Please check the data you entered");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string email, string password)
        {

                var User = await _userService.Login(email, password);
            if(User == null) {
                //400
                return BadRequest("Please check the data you entered");
                
            }
            return Ok(User);// 201 Created
        }

        [HttpGet]   
        [Route("{Id:int}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            var user = await _userService.GetUserDetails(Id);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("No User");
        }

        //[HttpGet]   // api/movies/toprevenue
        //[Route("")]
        //public async Task<IActionResult> GetUserDetails()
        //{
        //    var user = await _userService.GetUserDetails();

        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }
        //    return NotFound("No User");
        //}
    }
}
