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

        //public async Task<MovieDetailsResponseModel> GetMovieDetailsById(int id)
        //{

        //    var movieDetails = new MovieDetailsResponseModel
        //    {
        //        Id = 1,
        //        Title = "Avengers: Infinity War",
        //        PosterUrl = "",
        //        Budget = 1200000,
        //        Overview = "Test String"
        //    };

        //    return movieDetails;
        //}
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

            foreach (var genre in movie.Genre)
            {
                movieDetails.Genres.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name});
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
        //var movies = new List<MovieCardResponseModel>
        //{
        //    new MovieCardResponseModel {Id = 1, Title ="Avengers:Infinity War"},
        //    new MovieCardResponseModel {Id = 2, Title = "Avatar"},
        //    new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
        //    new MovieCardResponseModel {Id = 4, Title = "Titanic"},
        //    new MovieCardResponseModel {Id = 5, Title = "Inception"},
        //    new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
        //    new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
        //    new MovieCardResponseModel {Id = 8, Title = "Fight Club"},
        //    new MovieCardResponseModel
        //    {
        //        Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring"
        //    },
        //    new MovieCardResponseModel {Id = 10, Title = "The Dark Knight"},
        //    new MovieCardResponseModel {Id = 11, Title = "The Hunger Games"},
        //    new MovieCardResponseModel {Id = 12, Title = "Django Unchained"},
        //    new MovieCardResponseModel
        //    {
        //        Id = 13, Title = "The Lord of the Rings: The Return of the King"
        //    },
        //    new MovieCardResponseModel
        //        {Id = 14, Title = "Harry Potter and the Philosopher's Stone"},
        //    new MovieCardResponseModel {Id = 15, Title = "Iron Man"},
        //    new MovieCardResponseModel {Id = 16, Title = "Furious 7"}
        //};
        //return movies;
    }

   
}

