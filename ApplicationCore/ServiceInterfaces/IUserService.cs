using ApplicationCore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;

namespace ApplicationCore.ServiceInterfaces
{
   public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel);
        Task<UserLoginResponseModel> Login(string email, string password);

        // Get All Purchased Movies
        Task<List<MovieCardResponseModel>> GetPurchasesMovies(int id);

        // Get User Details
        Task<UserProfileResponseModel> GetUserDetails(int id);
        // EditUser
        Task UpdateUserDetails(UserProfileRequestModel model);

        // Get All Favorited Movies
        Task<List<MovieCardResponseModel>> GetFavoriteMoviesById(int id);

        // Favorite Movie
        Task<MovieCardResponseModel> AddFavoriteMovie(MovieCardResponseModel model);
        // delete
       
        // Change Password
        // Purchase Movie
       
        // Add Review

        
        // Edit Review
        // Remove Favorite

        // 


    }
}
