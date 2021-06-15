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


        Task<List<MovieCardResponseModel>> GetPurchasesMovies(int id);

        Task<UserProfileResponseModel> GetUserDetails(int id);
        // delete
        // EditUser
        // Change Password
        // Purchase Movie
        // Favorite Movie
        // Add Review
        // Get All Purchased Movies
        // Get All Favorited Movies
        // Edit Review
        // Remove Favorite
        // Get User Details
        // 


    }
}
