using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        //localhost/moveis/details/23
        //always have http Method attributes, by default if you don't have anything its HttpGet

        private readonly IMovieService _movieServices;
        public MoviesController(IMovieService service)
        {
            _movieServices = service;
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _movieServices.GetMovieDetailsById(id).GetAwaiter().GetResult();
            //Console.WriteLine(movie);
            return View(movie);
        }

        [HttpGet]
        public IActionResult TopRatedMovies()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopRevenue()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Genre(int genreId)
        {
            return View();
        }


    }
}
