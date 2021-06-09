using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class MovieCardResponseModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String PosterURL { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}
