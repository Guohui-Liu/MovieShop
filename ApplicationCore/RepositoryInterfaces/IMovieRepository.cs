using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie> //T is a class, can only pass in a class type,int is a struct
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();

        //Task<IEnumerable<Movie>> GetMovieDetailsById(int id);
    }
}


