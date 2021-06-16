using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
        }
        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            //go to database and get the user info--row by email
            var user = await _userRepository.GetUserByEmail(email);
            if(user == null)
            {
                return null;
            }

            // get the password from UI and salt from above step from CreateHashPassword method
            var hashPassword = CreateHashedPassword(password, user.Salt);
            if (hashPassword == user.HashedPassword)
            {
                //user enter correct password
                var loginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return loginResponseModel;
            }
            return null;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            //check if this email is registed or not, the email does not exist in current database
            var dbUser = await _userRepository.GetUserByEmail(userRegisterRequestModel.Email);

            if (dbUser != null)
            {
                throw new Exception("User already exist");
            }

            var salt = CreateSalt();
            var hashedPassword = CreateHashedPassword(userRegisterRequestModel.Password, salt);

            // call the user repository to save the user Info

            var user = new User
            {
                FirstName = userRegisterRequestModel.FirstName,
                LastName = userRegisterRequestModel.LastName,
                Email = userRegisterRequestModel.Email,
                DateOfBirth = userRegisterRequestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.Add(user);

            // convert the returned user entity to UserRegisterResponseModel

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };

            return response;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);

        }

        private string CreateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<List<MovieCardResponseModel>> GetPurchasesMovies(int id)
        {
            var purchaseMovies = await _purchaseRepository.GetUserPurchases(id);
            var purchaseMovieList = new List<MovieCardResponseModel>();
            foreach (var purchase in purchaseMovies)
            {
                purchaseMovieList.Add(new MovieCardResponseModel
                {
                    Id = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterURL = purchase.Movie.PosterUrl,
                    ReleaseDate=purchase.Movie.ReleaseDate.GetValueOrDefault()
                }) ;            
            }
            return purchaseMovieList;


        }

        public async Task<UserProfileResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetById(id);
            var userProfileResponse = new UserProfileResponseModel {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber
            };
            return userProfileResponse;
        }

        //public async Task<UserProfileRequestModel> UpdateUserDetails(UserProfileRequestModel model)
        //{
        //    //var user = await _userRepository.GetById(UserProfileRequestModel.Id);

            //if (user == null)
            //{
            //    // return null
            //    return null;
            //}

            //user.Id = UserProfileRequestModel.Id;
            //user.FirstName = UserProfileRequestModel.FirstName;
            //user.LastName = UserProfileRequestModel.LastName;
            //user.Email = UserProfileRequestModel.Email;
            //user.PhoneNumber = UserProfileRequestModel.PhoneNumber;
            //user.LastLoginDateTime = UserProfileRequestModel.LastLoginDateTime;

            //await _userRepository.UpdateAsync(user);

            //var response = new UserProfileResponseModel
            //{
            //    Id = userProfileResponseModel.Id,
            //    FirstName = userProfileResponseModel.FirstName,
            //    LastName = userProfileResponseModel.LastName,
            //    Email = userProfileResponseModel.Email,
            //    PhoneNumber = userProfileResponseModel.PhoneNumber,
            //    LastLoginDateTime = userProfileResponseModel.LastLoginDateTime
            //};

            //return response;

        //}

        Task IUserService.UpdateUserDetails(UserProfileRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieCardResponseModel>> GetFavoriteMoviesById(int id)
        {
            var favoriteMovie = await _userRepository.GetById(id);
            var favoriteMovieList = new List<MovieCardResponseModel>();
            foreach (var Movie in favoriteMovieList)
            {
                favoriteMovieList.Add(new MovieCardResponseModel
                {
                 Id = Movie.Id ,
                 PosterURL = Movie.PosterURL,
                 ReleaseDate = Movie.ReleaseDate.Date,
                 Title = Movie.Title 
                });
            }
            return favoriteMovieList;
        }

        public Task<MovieCardResponseModel> AddFavoriteMovie(MovieCardResponseModel model)
        {
            throw new NotImplementedException();
        }
    }
}

//purchaseMovieList.Add(new MovieCardResponseModel
//{
//    Id = purchase.MovieId,
//    Title = purchase.Movie.Title,
//    PosterURL = purchase.Movie.PosterUrl,
//    ReleaseDate = purchase.Movie.ReleaseDate.GetValueOrDefault()
//});            
//            }
//            return purchaseMovieList;