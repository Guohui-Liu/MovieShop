using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.Response;

namespace Infrastructure.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Movie, MovieDetailsResponseModel>();
        }
    }
}
