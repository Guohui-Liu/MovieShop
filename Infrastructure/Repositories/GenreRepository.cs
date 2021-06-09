using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
    }
}
