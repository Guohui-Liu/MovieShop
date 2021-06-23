using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentuserService;
        public UserController(IUserService userService, ICurrentUserService currentuserService)
        {
            _userService = userService;
            _currentuserService = currentuserService;
        }

        [Authorize]
        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            if (_currentuserService.UserId!= id)
            {
                return Unauthorized();
            }
            //get all movies purchased by user id
            //we need to check if the client who is calling this method 
           var userMovies = await _userService.GetPurchasesMovies(id);
            return Ok(userMovies);
           
        }
    }
}
