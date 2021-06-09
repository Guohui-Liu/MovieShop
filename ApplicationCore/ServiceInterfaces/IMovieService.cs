using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        // method for getting 30 top revenue movies
        //display properties in models,not return a single movie
        List<MovieCardResponseModel> GetTopRevenueMovies();

        MovieDetailsResponseModel GetMovieDetailsById(int id);
    }
}
