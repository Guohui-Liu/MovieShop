using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //readonly can only modified in constructor,don't want to change it in other method
        private readonly IMovieServices _movieService;

        //constructor injection
        //public HomeController()
        //{
        //    //_movieService = new MovieService();
        //}

        public HomeController(IMovieServices movieService)
        {
            _movieService = movieService;
        }

        //localhost/Home/Index
        public IActionResult Index()
        {
            //we nend to go to database and display revenue movies
            //thin controllers don't have too much location
            //MovieServices service = new MovieService();
            //var movies = service.GetTopRevenueMovies();

            var movies = _movieService.GetTopRevenueMovies();
            //send the data to the view so that the view can display top revenue movies
            //1,passing the data from controller to my view by using strongly typed model
            //2,ViewBag
            //3,ViewData
            //perfer first one, because 2, 3, used to send page title 
            ViewData["MyCustomerData"] = "some information";

            ViewBag.MovieCount = movies.Count;
            ViewBag.PageTitle = "Top 30";
            return View(movies);
            //return View("Privacy");
        }


        //localhost/Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }


        //public IActionResult TopMovies()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
