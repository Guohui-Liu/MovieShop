using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API
{
    [Route("api/[controller]")]//specify the url of
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]   // api/movies/toprevenue
        [Route("toprevenue")]
        public async Task<IActionResult> GetHighestGrossingMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("No Movies");
        }
    }
}
