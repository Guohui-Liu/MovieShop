using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
   public class MovieGenre
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public ICollection<Movie> Movie { get; set; }
        public ICollection<Genre> Genre { get; set; }
    }
}
