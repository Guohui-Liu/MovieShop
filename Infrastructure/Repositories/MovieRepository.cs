using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }


        public override async Task<Movie> GetById(int id)
        {
           //get movie info from Movie Table
           // get genres by joining moviegenre, genre
            //// movie, moviecast and cast
            //// rating, Avg of movieid Review Table
            var movie = await _dbContext.Movies.Include(m => m.MovieGenres).ThenInclude(m => m.Genre).
               Include(m => m.MovieCast).ThenInclude(m => m.Cast).
               FirstOrDefaultAsync(m => m.Id == id);

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            //if (movieRating > 0) 
                movie.Rating = movieRating;

            return movie;
        }
        //public IEnumerable<Movie> GetMovieDetailsById(int id)
        //{
        //    var movie = _dbContext.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
        //    yield return movie;
        //}

        //public override async Task<Movie> GetByIdAsync(int id)
        //{
        //    get movie info from movie table
        //    get genres by joining moviegenre, genre
        //    movie, moviecast and cast
        //    rating, avg of movieid review table
        //    return base.GetById(id);
        //    var movie = await _dbContext.Movies.Include(m => m.MovieCast).ThenInclude(m => m.Cast)
        //       .Include(m => m.Genre)
        //       .FirstOrDefaultAsync(m => m.Id == id);
        //    //if (movie == null)
        //    //{
        //    //    throw new NotFoundException("Movie Not found");
        //    //}

        //    var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
        //        .AverageAsync(r => r == null ? 0 : r.Rating);
        //    if (movieRating > 0) movie.Rating = movieRating;

        //    return movie;
        //}

    }

}

