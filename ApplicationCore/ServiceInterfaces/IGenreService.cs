﻿using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
   public interface IGenreService
    {
       Task<List<GenreResponseModel>> GetAllGenres();

        Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int Id);

    }
}
