using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using ApplicationCore.Helpers;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        //private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository/*, IMapper mapper*/)
        {
            _movieRepository = movieRepository;
           // _mapper = mapper;
        }

        
        public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        {
            var movie = await _movieRepository.GetById(id);

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                RunTime = movie.RunTime,
                Price = movie.Price,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault()
            };

            movieDetails.Genres = new List<GenreResponseModel>();
            movieDetails.Casts = new List<CastResponseModel>();

            foreach (var cast in movie.MovieCast)
            {
                movieDetails.Casts.Add(new CastResponseModel { Id = cast.CastId, Name = cast.Cast.Name, ProfilePath = cast.Cast.ProfilePath, Character = cast.Character });
            }

            foreach (var genre in movie.MovieGenres)
            {
                movieDetails.Genres.Add(new GenreResponseModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            return movieDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighestRevenueMovies();

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterURL = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                    Title = movie.Title
                });
            }

            return movieCardList;
        }
        
    }

   
}

