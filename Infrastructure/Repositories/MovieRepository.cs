using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository //implement 10 methods, 8 methods in EfRepo
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Movie> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetHighestRevenueMovies()
        {
            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList(); //inheritence from constructor in EfRepos
            return movies;
        }

    //    public override Movie GetById(int id)
    //    {
    //        return base.GetById(id);
    //    }



    }
}
