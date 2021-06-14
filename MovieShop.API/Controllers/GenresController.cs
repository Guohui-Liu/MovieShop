using ApplicationCore.ServiceInterfaces;
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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]   // api/movies/toprevenue
        [Route("")]
        public async Task<IActionResult> GetAllGenres()
        {
            var movies = await _genreService.GetAllGenres();

            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("No Movies");
        }
    }
}
