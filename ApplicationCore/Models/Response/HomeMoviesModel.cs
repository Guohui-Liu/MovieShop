using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    class HomeMoviesModel
    {
        List<MovieCardResponseModel> GetTopRevenueMovies { get; set; }
        List<MovieDetailsResponseModel> GetMovieDetailsById { get; set; }
    }
}
